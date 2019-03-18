# 카테고리 기능 추가 및 URL 개선
- string 타입은 Nullable 타입이 아니므로 "?"을 통한 체크가 불가능하다.
- 카테고리 null 체크는 LINQ에서 해결할 수 있다.
```
        public ViewResult List(string Category, int? Page)
        {
            int PageNo = Page ?? 1;
            var viewData = repository.Products
                            .Where(p => Category == null || p.Category == Category)
                            .OrderBy(p => p.ProductID);

            ViewBag.Category = Category ?? null;

            return View(viewData.ToPagedList(PageNo, PageSize));
        }
```
- View에서 페이지 이동 시 카테고리가 유지되도록 설정한다.
```
<div class="btn-group pull-right">
    @Html.PagedListPager(Model, Page => Url.Action("List",
                                        new { Page, Category = ViewBag.Category }))
</div>
```

- WebUI 프로젝트에서 RouteConfig를 통해 URL 구성을 개선한다.
  - 현재 카테고리는 http://~/?Category=Soccer 식으로 구성되어 있다.
  - 이를 http://~/Soccer 혹은 http://~/Soccer/Page2 식으로 구성한다.
  - 순서가 어긋날 경우 제대로 라우팅이 되지 않는다.
  - 페이지 검색 ---> 카테고리 검색 ---> 카테고리, 페이지 검색 순
```
//IgnoreRoute

            routes.MapRoute(
                name: null,
                url: "",
                defaults: new { controller = "Product", action = "List",
                                Category = (string)null, Page = 1}
            );

            routes.MapRoute(
                name: null,
                url: "Page{Page}",
                defaults: new { controller = "Product", action = "List", Category = (string)null }
            );

            routes.MapRoute(
                name: null,
                url: "{Category}",
                defaults: new { controller = "Product", action = "List", Page = 1 }
             );

            routes.MapRoute(
                name: null,
                url: "{Category}/Page{Page}",
                defaults: new { controller = "Product", action = "List" }
            );

// Default
```

# 카테고리 네비게이션 메뉴 구성 (자식 액션 사용)
- 카테고리 목록을 여러 컨트롤러들에서 사용할 것이므로 독립적이고 재사용할 수 있는 형태로 구현할 필요가 있다.
- 자식 액션은 재사용 가능한 네비게이션 컨트롤 같은 구성요소들을 구현해야 할 때 매우 적합하다.
- 자식 액션은 특정 액션 메소드의 출력을 현재 뷰에 포함시켜주는 HTML.Action이라는 HTML 헬퍼 메소드에 의존해서 동작한다.
- WebUI 프로젝트에서 Empty 템플릿 형태로 Controller 추가
- Controller의 이름을 NavController, 액션 메소드를 Menu() 로 설정 한다면
- 공통적인 적용을 위해 Views/Shared/Layout.cshtml에 HTML 헬퍼 메소드를 적용한다.
  - Html.Action에서 첫 번째 인자는 액션 메소드의 이름이 들어간다.
  - Html.Action에서 두 번째 인자는 컨트롤러의 이름이 들어간다. (뒤의 Controller는 생략)
```
    <div class="row panel">
        <div id="categories" class="col-xs-3">
            @Html.Action("Menu","Nav")
        </div>

        <div class="col-xs-8">
            @RenderBody()
        </div>
    </div>
```
- Controller에서 사용할 repository를 생성자로 등록한다.
```
        private IProductRepository repository;

        public NavController(IProductRepository repo)
        {
            this.repository = repo;
        }
```
- 이후에 Menu 엑션 메소드는 PartialViewResult 형태를 리턴한다.
  - LINQ를 이용해 카테고리 이름들을 알아낸다.
```
        public PartialViewResult Menu()
        {
            IEnumerable<string> categories = repository.Products
                                                .Select(x => x.Category)
                                                .Distinct()
                                                .OrderBy(x => x);
            return PartialView(categories);
        }
```
- 해당 엑션 메소드의 뷰를 생성한다.
- Views/Nav 폴더에서 Menu.cshtml 생성
- controller에서 넘어온 string categories를 받기 위해 @model을 선언한다.
- Html.ActionLink를 사용해 카테고리가 없는 경우의 링크를 만들어 준다.
  - 첫 번째 인자 : 안에 들어갈 텍스트
  - 두 번째 인자 : 엑션 메소드 이름
  - 세 번째 인자 : 컨트롤러의 이름,
  - 네 번째 인자 : 라우트 벨류
  - 다섯번째 인자 : 적용할 html 속성 (클래스 등)
```
@model IEnumerable<string>

@Html.ActionLink("Home","List","Product",
    new
    {
        Category = (string)null,
        Page = 1
    },
    new { @class = "btn btn-block btn-default btn-lg" })

@foreach (var link in Model)
{
    @Html.ActionLink(link, "List", "Product",
        new
        {
            Category = link,
            Page = 1
        },
        new { @class = "btn btn-block btn-default btn-lg" +
                        (link == ViewBag.SelectedCategory ? " btn-primary" : "") })
}
```

# Form 전송 후 리다이렉션 (Html.BeginForm 사용)
- BeginForm 헬퍼
  - 첫 번째 인자 : 엑션 메소드명
  - 두 번째 인자 : 컨트롤러 이름 (Controller 생략)
- 히든 태그로 ProductID와 returnUrl 전송
```
    @using (Html.BeginForm("AddToCart", "Cart"))
    {
        <div class="pull-right">
            @Html.HiddenFor(x => x.ProductID)
            @Html.Hidden("returnUrl", Request.Url.PathAndQuery)
            <input type="submit" class="btn btn-success" value="Add to cart"/>
        </div>
    }
```
- Controller에서 받아서 사용
```
        public RedirectToRouteResult AddToCart(int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);

		...

            return RedirectToAction("Index", new { returnUrl });
        }
```

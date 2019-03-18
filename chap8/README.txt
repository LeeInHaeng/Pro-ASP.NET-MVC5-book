# ī�װ� ��� �߰� �� URL ����
- string Ÿ���� Nullable Ÿ���� �ƴϹǷ� "?"�� ���� üũ�� �Ұ����ϴ�.
- ī�װ� null üũ�� LINQ���� �ذ��� �� �ִ�.
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
- View���� ������ �̵� �� ī�װ��� �����ǵ��� �����Ѵ�.
```
<div class="btn-group pull-right">
    @Html.PagedListPager(Model, Page => Url.Action("List",
                                        new { Page, Category = ViewBag.Category }))
</div>
```

- WebUI ������Ʈ���� RouteConfig�� ���� URL ������ �����Ѵ�.
  - ���� ī�װ��� http://~/?Category=Soccer ������ �����Ǿ� �ִ�.
  - �̸� http://~/Soccer Ȥ�� http://~/Soccer/Page2 ������ �����Ѵ�.
  - ������ ��߳� ��� ����� ������� ���� �ʴ´�.
  - ������ �˻� ---> ī�װ� �˻� ---> ī�װ�, ������ �˻� ��
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

# ī�װ� �׺���̼� �޴� ���� (�ڽ� �׼� ���)
- ī�װ� ����� ���� ��Ʈ�ѷ��鿡�� ����� ���̹Ƿ� �������̰� ������ �� �ִ� ���·� ������ �ʿ䰡 �ִ�.
- �ڽ� �׼��� ���� ������ �׺���̼� ��Ʈ�� ���� ������ҵ��� �����ؾ� �� �� �ſ� �����ϴ�.
- �ڽ� �׼��� Ư�� �׼� �޼ҵ��� ����� ���� �信 ���Խ����ִ� HTML.Action�̶�� HTML ���� �޼ҵ忡 �����ؼ� �����Ѵ�.
- WebUI ������Ʈ���� Empty ���ø� ���·� Controller �߰�
- Controller�� �̸��� NavController, �׼� �޼ҵ带 Menu() �� ���� �Ѵٸ�
- �������� ������ ���� Views/Shared/Layout.cshtml�� HTML ���� �޼ҵ带 �����Ѵ�.
  - Html.Action���� ù ��° ���ڴ� �׼� �޼ҵ��� �̸��� ����.
  - Html.Action���� �� ��° ���ڴ� ��Ʈ�ѷ��� �̸��� ����. (���� Controller�� ����)
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
- Controller���� ����� repository�� �����ڷ� ����Ѵ�.
```
        private IProductRepository repository;

        public NavController(IProductRepository repo)
        {
            this.repository = repo;
        }
```
- ���Ŀ� Menu ���� �޼ҵ�� PartialViewResult ���¸� �����Ѵ�.
  - LINQ�� �̿��� ī�װ� �̸����� �˾Ƴ���.
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
- �ش� ���� �޼ҵ��� �並 �����Ѵ�.
- Views/Nav �������� Menu.cshtml ����
- controller���� �Ѿ�� string categories�� �ޱ� ���� @model�� �����Ѵ�.
- Html.ActionLink�� ����� ī�װ��� ���� ����� ��ũ�� ����� �ش�.
  - ù ��° ���� : �ȿ� �� �ؽ�Ʈ
  - �� ��° ���� : ���� �޼ҵ� �̸�
  - �� ��° ���� : ��Ʈ�ѷ��� �̸�,
  - �� ��° ���� : ���Ʈ ����
  - �ټ���° ���� : ������ html �Ӽ� (Ŭ���� ��)
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

# Form ���� �� �����̷��� (Html.BeginForm ���)
- BeginForm ����
  - ù ��° ���� : ���� �޼ҵ��
  - �� ��° ���� : ��Ʈ�ѷ� �̸� (Controller ����)
- ���� �±׷� ProductID�� returnUrl ����
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
- Controller���� �޾Ƽ� ���
```
        public RedirectToRouteResult AddToCart(int productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);

		...

            return RedirectToAction("Index", new { returnUrl });
        }
```

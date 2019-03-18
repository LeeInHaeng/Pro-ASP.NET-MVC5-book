# 반응형 웹 디자인 사용하기 (bootstrap 내에서)
- 반응형 페이지 헤더 만들기
```
<head>
	...

    <style>
        .navbar-right{
            float: right !important;
            margin-right: 15px; margin-left: 15px;
        }
    </style>
</head>
```
- 클래스의 이름이 visible- 혹은 hidden- 으로 시작한다.
- xs로 끝나는 클래스들은 768 픽셀 이하로 되면 동작한다.
- sm으로 끝나는 클래스들은 768 픽셀 ~ 992 픽셀 사이에서 동작한다.
- md로 끝나는 클래스들은 992픽셀 ~ 1200 픽셀 사이에서 동작한다.
- lg로 끝나는 클래스들은 1200 픽셀보다 큰 화면에서 동작한다.
```
    <div class="navbar navbar-inverse" role="navigation">
        <a class="navbar-brand" href="#">
            <span class="hidden-xs">SPORTS STORE</span>
            <div class="visible-xs">SPORTS</div>
            <div class="visible-xs">STORE</div>
        </a>
        @Html.Action("Summary", "Cart")
    </div>
```
- 해당 예제는 가로가 768 픽셀 이하가 되면 SPORTS STORE가 2줄로 변경 된다.
- 또한 헤더에서 선언해둔 스타일을 이용해 너비가 변경 되어도 같은 위치에 그려지도록 할 수 있다.
```
<div class="navbar-right hidden-xs">
    @Html.ActionLink("Checkout", "Index", "Cart",
        new { returnUrl = Request.Url.PathAndQuery },
        new { @class = "btn btn-default navbar-btn "})
</div>

<div class="navbar-right visible-xs">
    <a href=@Url.Action("Index", "Cart", new {returnUrl = Request.Url.PathAndQuery})
        class="btn btn-default navbar-btn">
        <span class="glyphicon glyphicon-shopping-cart"></span>
    </a>
</div>
```
- Razor에서 RenderBody는 오직 한 번만 호출할 수 있다.
- 이러한 이유에서 상황에 맞게 보여주거나 숨기는 방법은 사용할 수 없다.
- 하지만 Bootstrap에서 그리드 레이아웃 기능을 사용하면 너비 픽셀에 따라 적절하게 변경이 가능하다.
```
        <div class="col-xs-12 col-sm-8">
            @RenderBody()
        </div>
```
- 768 픽셀 이상일 때 8개의 열을 차지하며, 768 픽셀 이하일 때는 12개의 열을 차지한다.

# 컨트롤러가 뷰 선택할 때 도와주기
- 너비가 작을 때 보여줄 부분 뷰를 따로 작성한다.
- 레이아웃에서 너비 픽셀에 따라 부분 뷰를 선택한다.
  - 768 픽셀 이하일때 horizontalLayout을 true로 넘겨준다.
  - 그 이외에는 그냥 부분 뷰를 선택한다.
```
        <div class="visible-xs">
            @Html.Action("Menu", "Nav", new { horizontalLayout = true })
        </div>
        <div id="categories" class="col-sm-3 hidden-xs">
            @Html.Action("Menu","Nav")
        </div>
```
- 컨트롤러에서 뷰를 선택한다.
  - horizontalLayout의 디폴트는 false로 선택한다.
  - 뷰의 horizontalLayout에 따라 어떠한 부분 뷰를 사용할지 컨트롤러에서 선택한다.
```
        public PartialViewResult Menu(string category, bool horizontalLayout = false)
        {
		...

            string viewName = horizontalLayout ? "MenuHorizontal" : "Menu";
            return PartialView(viewName, categories);
        }
```

# 모바일 전용 컨텐츠 만들기 (jquery mobile 사용)
- 반응형 디자인은 다루기도 힘들고, 올바로 수정하기 위해 끝없는 테스트가 필요하다는 문제가 있다.
- 또한 전체적인 주의를 기울이지 않는다면 모든 장치에서 적당하게 타협된 응용프로그램을 만들게 될 가능성도 높다.
- 이러한 결과 훌륭한 특징을 살리지 못하고, 전체적으로 하향 평준화 되는 결과를 얻게 될 수도 있다.
- 그렇기 때문에 모바일 전용의 컨텐츠를 따로 만들어둘 필요가 있다.
- 단점은 mobile jquery를 따로 공부하고, 모바일 전용 뷰를 새로 만들어야 한다.
- 먼저 모바일 레이아웃을 만든다.
  - 새 항목 ---> 웹 ---> Razor ---> Layout Page를 선택하여 만든다.
  - 파일명 뒤에 .Mobile.cshtml로 끝나는 파일을 만들어야 한다.
```
<!DOCTYPE html>
<html>
    <head>
        <meta charset="utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <link rel="stylesheet" href="http://code.jquery.com/mobile/1.4.5/jquery.mobile-1.4.5.min.css" />
        <script src="http://code.jquery.com/jquery-1.11.1.min.js"></script>
        <script src="http://code.jquery.com/mobile/1.4.5/jquery.mobile-1.4.5.min.js"></script>
        <title>@ViewBag.Title</title>
    </head>
    <body>
        <div data-role="page" id="page1">
            <div data-theme="a" data-role="header" data-position="fixed">
                <h3>SportsStore</h3>
                @Html.Action("Menu","Nav")
            </div>
        </div>
        <div data-role="content">
            <ul data-role="listview" data-dividertheme="b" data-inset="false">
                @RenderBody()
            </ul>
        </div>
    </body>
</html>
```
- 모바일에서 사용할 뷰를 따로 만들기
  - .Mobile.cshtml로 끝나는 뷰를 작성한다.

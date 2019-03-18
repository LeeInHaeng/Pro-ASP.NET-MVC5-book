# ������ �� ������ ����ϱ� (bootstrap ������)
- ������ ������ ��� �����
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
- Ŭ������ �̸��� visible- Ȥ�� hidden- ���� �����Ѵ�.
- xs�� ������ Ŭ�������� 768 �ȼ� ���Ϸ� �Ǹ� �����Ѵ�.
- sm���� ������ Ŭ�������� 768 �ȼ� ~ 992 �ȼ� ���̿��� �����Ѵ�.
- md�� ������ Ŭ�������� 992�ȼ� ~ 1200 �ȼ� ���̿��� �����Ѵ�.
- lg�� ������ Ŭ�������� 1200 �ȼ����� ū ȭ�鿡�� �����Ѵ�.
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
- �ش� ������ ���ΰ� 768 �ȼ� ���ϰ� �Ǹ� SPORTS STORE�� 2�ٷ� ���� �ȴ�.
- ���� ������� �����ص� ��Ÿ���� �̿��� �ʺ� ���� �Ǿ ���� ��ġ�� �׷������� �� �� �ִ�.
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
- Razor���� RenderBody�� ���� �� ���� ȣ���� �� �ִ�.
- �̷��� �������� ��Ȳ�� �°� �����ְų� ����� ����� ����� �� ����.
- ������ Bootstrap���� �׸��� ���̾ƿ� ����� ����ϸ� �ʺ� �ȼ��� ���� �����ϰ� ������ �����ϴ�.
```
        <div class="col-xs-12 col-sm-8">
            @RenderBody()
        </div>
```
- 768 �ȼ� �̻��� �� 8���� ���� �����ϸ�, 768 �ȼ� ������ ���� 12���� ���� �����Ѵ�.

# ��Ʈ�ѷ��� �� ������ �� �����ֱ�
- �ʺ� ���� �� ������ �κ� �並 ���� �ۼ��Ѵ�.
- ���̾ƿ����� �ʺ� �ȼ��� ���� �κ� �並 �����Ѵ�.
  - 768 �ȼ� �����϶� horizontalLayout�� true�� �Ѱ��ش�.
  - �� �̿ܿ��� �׳� �κ� �並 �����Ѵ�.
```
        <div class="visible-xs">
            @Html.Action("Menu", "Nav", new { horizontalLayout = true })
        </div>
        <div id="categories" class="col-sm-3 hidden-xs">
            @Html.Action("Menu","Nav")
        </div>
```
- ��Ʈ�ѷ����� �並 �����Ѵ�.
  - horizontalLayout�� ����Ʈ�� false�� �����Ѵ�.
  - ���� horizontalLayout�� ���� ��� �κ� �並 ������� ��Ʈ�ѷ����� �����Ѵ�.
```
        public PartialViewResult Menu(string category, bool horizontalLayout = false)
        {
		...

            string viewName = horizontalLayout ? "MenuHorizontal" : "Menu";
            return PartialView(viewName, categories);
        }
```

# ����� ���� ������ ����� (jquery mobile ���)
- ������ �������� �ٷ�⵵ �����, �ùٷ� �����ϱ� ���� ������ �׽�Ʈ�� �ʿ��ϴٴ� ������ �ִ�.
- ���� ��ü���� ���Ǹ� ������� �ʴ´ٸ� ��� ��ġ���� �����ϰ� Ÿ���� �������α׷��� ����� �� ���ɼ��� ����.
- �̷��� ��� �Ǹ��� Ư¡�� �츮�� ���ϰ�, ��ü������ ���� ����ȭ �Ǵ� ����� ��� �� ���� �ִ�.
- �׷��� ������ ����� ������ �������� ���� ������ �ʿ䰡 �ִ�.
- ������ mobile jquery�� ���� �����ϰ�, ����� ���� �並 ���� ������ �Ѵ�.
- ���� ����� ���̾ƿ��� �����.
  - �� �׸� ---> �� ---> Razor ---> Layout Page�� �����Ͽ� �����.
  - ���ϸ� �ڿ� .Mobile.cshtml�� ������ ������ ������ �Ѵ�.
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
- ����Ͽ��� ����� �並 ���� �����
  - .Mobile.cshtml�� ������ �並 �ۼ��Ѵ�.

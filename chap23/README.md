# URL�� �����ϴ� ������ HTML ���� �޼ҵ�
- Url.Content("~/Content/Site.css")
  - �������α׷��� ������� URL�� �����Ѵ�.
  ```
  - ��� : "/Content/Site.css"
  ```
- Html.ActionLink("My Link", "Index", "Home")
  - ������ �׼� �� ��Ʈ�ѷ��� ���� ��ũ�� �����Ѵ�.
  ```
  - ��� : <a href="/">My Link</a>
  ```
- Url.Action("GetPeople", "People")
  - ������ �׼ǿ� ���� URL�� �����Ѵ�.
  ```
  - ��� : /People/GetPeople
  ```
- Url.RouteUrl(new {controller = "People", action="GetPeople"})
  - ���Ʈ �����͸� �����ؼ� URL�� �����Ѵ�.
  ```
  - ��� : /People/GetPeople
  ```
- Html.RouteLink("My Link", new {controller = "People", action="GetPeople"})
  - ���Ʈ �����͸� �����ؼ� ��ũ�� �����Ѵ�.
  ```
  - ��� : <a href="/People/GetPeople">My Link</a>
  ```
- Html.RouteLink("My Link", "FormRoute", new {controller = "People", action="GetPeople"})
  - ������ ���Ʈ�� ���� ��ũ�� �����Ѵ�.
  ```
  - ��� : <a href="/app/forms/People/GetPeople">My Link</a>
  ```

# Ajax �� �̿��� �κ������� �� �׸���
- �� �������� ���� ��ġ�� �ʰ� ��׶��忡�� ������ �����͸� ��û�ؼ� �������� ���� ��
- NuGet���� jQuery�� Microsoft.jQuery.Unobtrusive ��ġ
- Web.config ���Ͽ��� Ajax ����� Ȱ��ȭ ��Ų��.
  - appSettings �±� �ȿ���
  ```
  - <add key="UnobtrusiveJavaScriptEnabled" value="true" /> �� ����
  ```
- cshtml���� jQuery ���
```
    <script src="~/Scripts/jquery-3.3.1.js"></script>
    <script src="~/Scripts/jquery.unobtrusive-ajax.js"></script>
```
- �� �±� ���۽� �信�� table ��ü�� �ٽ� �׸��� ���� �ƴ� �񵿱��� ó���� ���� �κ��� ������Ʈ�� ���� �κ� �並 ���
- ��Ʈ�ѷ����� ��ü ���̺� ��ȸ�� �κ� ���̺� ��ȸ�� ������.
```
        public PartialViewResult GetPeopleData(string selectedRole = "All") {
            IEnumerable<Person> data = personData;
            if (selectedRole != "All") {
                Role selected = (Role)Enum.Parse(typeof(Role), selectedRole);
                data = personData.Where(p => p.Role == selected);
            }
            return PartialView(data);
        }

        public ActionResult GetPeople(string selectedRole = "All") {
            return View((object)selectedRole);
        }
```
- �κ� �信 ���� cshtml ���� ����
```
@using HelperMethods.Models
@model IEnumerable<Person>

@foreach (Person p in Model) {
    <tr>
        <td>@p.FirstName</td>
        <td>@p.LastName</td>
        <td>@p.Role</td>
    </tr>
}

```
- Ajax�� ����� cshtml���� Ajax ���� ���� �� �� ����
  - Html.BeginForm ���� �޼��带 Ajax.BeginForm ���� �޼���� ��ü�Ѵ�.
```
@{
    ViewBag.Title = "GetPeople";
    Layout = "/Views/Shared/_Layout.cshtml";
    AjaxOptions ajaxOpts = new AjaxOptions
    {
        UpdateTargetId = "tableBody"
    };
}

...

    <tbody id="tableBody">
        @Html.Action("GetPeopleData", new { selectedRole = Model })
    </tbody>
    
...

@using (Ajax.BeginForm("GetPeopleData", ajaxOpts)) {
    <div>
        @Html.DropDownList("selectedRole", new SelectList(
            new[] { "All" }.Concat(Enum.GetNames(typeof(Role)))))
        <button type="submit">Submit</button>
    </div>
}
```

# Ajax �ɼ� �����ϱ�
- Javascript�� ��Ȱ��ȭ ���װų�, Javascript�� �������� �ʴ� �� ���������� ���� �����ϵ��� �����ϱ� ���� ���
  - Ajax �ɼ� �� Url�� ����� �񵿱� ó���� ���� ��Ʈ�ѷ��� �����Ѵ�.
```
@{
    ViewBag.Title = "GetPeople";
    Layout = "/Views/Shared/_Layout.cshtml";
    AjaxOptions ajaxOpts = new AjaxOptions
    {
        UpdateTargetId = "tableBody",
        Url = Url.Action("GetPeopleData")
    };
}

...

@using (Ajax.BeginForm(ajaxOpts)) {
    ...
}
```
- Ajax ��û�� ó���ϴ� ���� ����ڿ��� �ǵ�� �����ϱ�
  - LoadingElementId�� LoadingElementDuration �ɼ� ���
  - �񵿱� ó�� ���� ����ڿ��� ������ html ��� ���� ---> display�� none���� ����
```
@{
    ViewBag.Title = "GetPeople";
    Layout = "/Views/Shared/_Layout.cshtml";
    AjaxOptions ajaxOpts = new AjaxOptions
    {
        UpdateTargetId = "tableBody",
        Url = Url.Action("GetPeopleData"),
        LoadingElementId = "loading",
        LoadingElementDuration = 1000
    };
}

...

<div id="loading" class="load" style="display:none">
    <p>Loading Data...</p>
</div>
```
- Ajax ��û ���� ����ڿ��� ������Ʈ ǥ��
  - Confirm �ɼ��� ���
```
@{
    ViewBag.Title = "GetPeople";
    Layout = "/Views/Shared/_Layout.cshtml";
    AjaxOptions ajaxOpts = new AjaxOptions
    {
        UpdateTargetId = "tableBody",
        Url = Url.Action("GetPeopleData"),
        LoadingElementId = "loading",
        LoadingElementDuration = 1000,
        Confirm = "Do you wish to request new data?"
    };
}
```
- Ajax ���� �ƴ� Ajax ��ũ ����
```
    @foreach (string role in Enum.GetNames(typeof(Role))) {
        <div class="ajaxLink">
            @Ajax.ActionLink(role, "GetPeople",
                new { selectedRole = role },
                new AjaxOptions {
                    UpdateTargetId = "tableBody",
                    Url = Url.Action("GetPeopleData", new { selectedRole = role })
                })
        </div>
    }
```
- Ajax �ݹ� ����ϱ�
  - OnBegin : ��û�� ���۵Ǳ� ���� ��� ȣ��ȴ�.
  - OnComplete : ��û�� �����ϸ� ȣ��ȴ�.
  - OnFailure : ��û�� �����ϸ� ȣ��ȴ�.
  - OnSuccess : ��û�� ���� �Ǵ� ���� ���ο� ���� ����, ��û�� �Ϸ�Ǹ� ȣ��ȴ�.
```
    AjaxOptions ajaxOpts = new AjaxOptions
    {
        UpdateTargetId = "tableBody",
        Url = Url.Action("GetPeopleData"),
        LoadingElementId = "loading",
        LoadingElementDuration = 1000,
        Confirm = "Do you wish to request new data?",
        OnSuccess = "MySuccess"
    };
    
<script type="text/javascript">
    function MySuccess(){
    
    }
...

```
# URL을 렌더하는 간단한 HTML 헬퍼 메소드
- Url.Content("~/Content/Site.css")
  - 응용프로그램에 상대적인 URL을 생성한다.
  ```
  - 결과 : "/Content/Site.css"
  ```
- Html.ActionLink("My Link", "Index", "Home")
  - 지정된 액션 및 컨트롤러에 대한 링크를 생성한다.
  ```
  - 결과 : <a href="/">My Link</a>
  ```
- Url.Action("GetPeople", "People")
  - 지정된 액션에 대한 URL을 생성한다.
  ```
  - 결과 : /People/GetPeople
  ```
- Url.RouteUrl(new {controller = "People", action="GetPeople"})
  - 라우트 데이터를 지정해서 URL을 생성한다.
  ```
  - 결과 : /People/GetPeople
  ```
- Html.RouteLink("My Link", new {controller = "People", action="GetPeople"})
  - 라우트 데이터를 지정해서 링크를 생성한다.
  ```
  - 결과 : <a href="/People/GetPeople">My Link</a>
  ```
- Html.RouteLink("My Link", "FormRoute", new {controller = "People", action="GetPeople"})
  - 지정된 라우트에 대한 링크를 생성한다.
  ```
  - 결과 : <a href="/app/forms/People/GetPeople">My Link</a>
  ```

# Ajax 를 이용해 부분적으로 뷰 그리기
- 웹 페이지를 새로 고치지 않고도 백그라운드에서 서버의 데이터를 요청해서 가져오기 위한 모델
- NuGet에서 jQuery와 Microsoft.jQuery.Unobtrusive 설치
- Web.config 파일에서 Ajax 기능을 활성화 시킨다.
  - appSettings 태그 안에서
  ```
  - <add key="UnobtrusiveJavaScriptEnabled" value="true" /> 로 지정
  ```
- cshtml에서 jQuery 사용
```
    <script src="~/Scripts/jquery-3.3.1.js"></script>
    <script src="~/Scripts/jquery.unobtrusive-ajax.js"></script>
```
- 폼 태그 전송시 뷰에서 table 전체를 다시 그리는 것이 아닌 비동기적 처리를 통한 부분적 업데이트를 위해 부분 뷰를 사용
- 컨트롤러에서 전체 테이블 조회와 부분 테이블 조회를 나눈다.
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
- 부분 뷰에 대한 cshtml 파일 생성
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
- Ajax를 사용할 cshtml에서 Ajax 관련 설정 및 폼 생성
  - Html.BeginForm 헬퍼 메서드를 Ajax.BeginForm 헬퍼 메서드로 대체한다.
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

# Ajax 옵션 설정하기
- Javascript를 비활성화 시켰거나, Javascript를 지원하지 않는 웹 브라우저에서 정상 동작하도록 설정하기 위한 방법
  - Ajax 옵션 중 Url을 사용해 비동기 처리를 위한 컨트롤러를 지정한다.
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
- Ajax 요청을 처리하는 동안 사용자에게 피드백 제공하기
  - LoadingElementId와 LoadingElementDuration 옵션 사용
  - 비동기 처리 동안 사용자에게 보여줄 html 요소 생성 ---> display는 none으로 설정
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
- Ajax 요청 전에 사용자에게 프롬프트 표시
  - Confirm 옵션을 사용
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
- Ajax 폼이 아닌 Ajax 링크 생성
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
- Ajax 콜백 사용하기
  - OnBegin : 요청이 전송되기 전에 즉시 호출된다.
  - OnComplete : 요청이 성공하면 호출된다.
  - OnFailure : 요청이 실패하면 호출된다.
  - OnSuccess : 요청의 성공 또는 실패 여부와 관계 없이, 요청이 완료되면 호출된다.
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
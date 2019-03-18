# Visual Studio 설치 및 초기 프로젝트
- https://visualstudio.microsoft.com/ko/vs/express/
- 맨 아래쪽에 Express 2015 for Web 클릭 ---> 2015 Download
- Visual Studio Express 2015 for Web 설치
- C# ---> 웹 ---> ASP.NET 웹 응용 프로그램
- 템플릿은 Empty로 선택 후, 핵심 참조는 MVC만 체크
- Visual Studio에서 Project ---> 속성 ---> 웹 ---> 특정 페이지 선택
  - 특정 URL을 적지 않는다면 기본적으로 /index 를 요청

# IIS 설치
- 자습서 : https://docs.microsoft.com/ko-kr/aspnet/web-forms/overview/deployment/visual-studio-web-deployment/deploying-to-iis
- Web Platform Installer : https://www.microsoft.com/web/downloads/platform.aspx
- IIS: .NET Extensibility 4.5
- Web Deploy 3.6 without bundled SQL support

# Controller에서 View로 데이터 전달 (ViewBag 사용)
```
// Controller
            int hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour < 12 ? "Good Morning" : "Good Afternoon";

// View
    <div> 
        @ViewBag.Greeting World (from the view)
    </div>
```
- View에서 간단한 링크 걸기
  - 첫 번째 매개변수 : 링크에 출력될 문자열
  - 두 번째 매개변수 : 사용자가 링크를 클릭했을 때 실행될 액션의 이름 (http://~/Home/RsvpForm)
  - Controller에서 RsvpForm 메소드 생성
```
@Html.ActionLink("RSVP Now", "RsvpForm")
```

# MVC 중 Model 작성
- 보통 '도메인'으로 알려져 있으며 응용프로그램의 주제를 정의하는 실제 세계의 개체들과 절차들, 그리고 규칙들을 나타낸다.
- 종종 '도메인 모델'이라고 부르기도 하며, C# 개체들과 이 개체들을 조작할 수 있는 메서드들로 구성된다.
- 잘 설계된 MVC는 잘 설계된 모델로부터 비롯되며, 잘 설계된 모델은 컨트롤러와 뷰를 추가하기 위한 중추이다.
```
        public string Phone { get; set; }
        public bool? WillAttend { get; set; }
```

# View에서 폼 작성하기 (BeginForm 사용)
- Strongly Typed View 생성
  - 뷰 추가 ---> Template는 Empty로 선택 ---> Model Class에서 작성한 모델 클래스 선택
  - 작성한 모델 클래스의 속성들에 대해 각각 적절한 HTML 입력 컨트롤들을 렌더할 수 있다.
  - @Model 을 이용하여 클래스의 값에 접근 가능
```
<h1>Thank you, @Model.Name!</h1>
@if (Model.WillAttend == true)
{
	@: 참가 감사
}
else
{
	@: 아쉽
}
```
- HTML의 form 요소를 생성하도록 설정
  - HTML 헬퍼 메소드 중 BeginForm 사용
  - 아무런 매개변수도 전달하지 않으면 현재 폼이 존재하는 HTML 문서와 동일한 URL로 요청하는 것으로 간주
```
        @using (Html.BeginForm())
        {
		... 폼 내용이 들어가는 위치 ...

		<input type="submit" value="Submit RSVP"/>
	}
```
- 폼 내용 안에서 HTML 헬퍼 메소드 중 TextBoxFor 사용
  - 해당 헬퍼 메소드는 type은 text로 지정하고, id와 name은 선택된 클래스 속성의 이름으로 설정한다.
  - 두 번째 인자로 어트리뷰트를 지정할 수 있는 선택적 object 매개변수를 지정할 수 있다.
  - 두 번째 인자로는 CSS로 추가할 클래스 명을 적는다.
```
<p>Your name : @Html.TextBoxFor(x => x.Name, new { @class = "form-control" })</p>
---> <input id="Phone" name="Phone" type="text" value="" class="form-control"/>
```
- 폼 내용 안에서 HTML 헬퍼 메소드 중 DropDownListFor 사용
```
                @Html.DropDownListFor(x => x.WillAttend, new[] {
                    new SelectListItem() { Text = "Yes, I'll be there",
                                            Value = bool.TrueString },
                    new SelectListItem() { Text = "No, I can't come",
                                            Value = bool.FalseString }
                    }, "Choose an option", new { @class = "form-control" })
```

# MVC 중 Controller 작성 (모델 바인딩)
- Post 메소드를 보면 '모델 바인딩'을 사용
- 모델 바인딩은 전달된 데이터를 분석하고 HTTP 요청에 포함된 키값의 쌍들을 이용해서 도메인 모델 형식의 속성들을 자동으로 채워준다.
- (유효성 검사 후 실패해도)뷰가 렌더될 때 입력했던 데이터들이 유지되어 다시 출력되는 이점이 있음
- 또한 View의 첫 번째 매개변수는 해당 View를 보여 주도록 한다.
  - Thanks ---> Thanks.cshtml, GuestResponse 개체를 해당 뷰에 전달
```
        [HttpGet]
        public ViewResult RsvpForm()
        {
            return View();
        }

        [HttpPost]
        public ViewResult RsvpForm(GuestResponse guestResponse)
        {
            return View("Thanks", guestResponse);
        }
```

# Model 클래스에 유효성 검사 추가하기
- Model에서 using System.ComponentModel.DataAnnotations; 사용
- Controller에서 ModelState.IsValid 로 검사
- View에서 HTML 헬퍼 메소드 중 ValidationSummary() 사용
```
// Model 클래스에서
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your emaill address")]
        [RegularExpression(".+\\@.+\\..+",
            ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

	// 숫자 부분 유효성 검사
	[Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal Price { get; set; }

// Controller 에서
        [HttpPost]
        public ViewResult RsvpForm(GuestResponse guestResponse)
        {
            if (ModelState.IsValid)
                return View("Thanks", guestResponse);
            else
                return View();
        }

// View 에서
        @using (Html.BeginForm())
        {
            @Html.ValidationSummary()
            <p>Your name : @Html.TextBoxFor(x => x.Name)</p>

	// 따로 메시지 보여주기 : @Html.ValidationMessage(태그의 name 속성)
	...

```
- 유효성 검사 실패 시 input 태그에 input-validation-error 라는 클래스가 자동으로 추가된다.
  - 이를 이용해 유효하지 않은 필드에 대해 강조 처리를 할 수 있다.
  - Content 폴더 생성 후 안에 정적 파일들을 넣는다 (.css 등)
```
// css 에서
.input-validation-error{
    border: 1px solid #f00;
    background-color: #fee;
}
.validation-summary-errors{
    font-weight: bold;
    color: #f00;
}
.validation-summary-valid{
    display:none;
}

// View 에서
<link rel="stylesheet" type="text/css" href="~/Content/Styles.css"/>
```

# NuGet을 이용해 필요 라이브러리 설치
- 어떠한 라이브러리를 다운로드 할 때 해당 라이브러리에 의존성을 갖는 다른 라이브러리들도 함께 다운된다.
- 도구 ---> Nuget 패키지 관리자 ---> 솔루션용 NuGet 패키지 관리 에서 필요한 라이브러리 검색 후 설치
- Visual Studio의 버전에 맞는 라이브러리 버전을 찾아서 다운로드 하도록 주의

# WebMail 헬퍼 메서드를 통해 자동 전자메일 전송 예제 작성
```
<body>
    @{ 
        try
        {
            WebMail.SmtpServer = "smpt.example.com";
            WebMail.SmtpPort = 587;
            WebMail.EnableSsl = true;
            WebMail.UserName = "mySmtpUsername";
            WebMail.Password = "mySmtpPassword";
            WebMail.From = "rsvp@example.com";
            WebMail.Send("party-host@example.com", "RSVP Notification",
                Model.Name + " is " + ((Model.WillAttend ?? false) ? "" : "not") + "attending");
        }
        catch (Exception)
        {
            @:<b>Sorry - we couldn't send the email to confirm your RSVP.</b>
        }
    }
```

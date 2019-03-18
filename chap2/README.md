# Visual Studio ��ġ �� �ʱ� ������Ʈ
- https://visualstudio.microsoft.com/ko/vs/express/
- �� �Ʒ��ʿ� Express 2015 for Web Ŭ�� ---> 2015 Download
- Visual Studio Express 2015 for Web ��ġ
- C# ---> �� ---> ASP.NET �� ���� ���α׷�
- ���ø��� Empty�� ���� ��, �ٽ� ������ MVC�� üũ
- Visual Studio���� Project ---> �Ӽ� ---> �� ---> Ư�� ������ ����
  - Ư�� URL�� ���� �ʴ´ٸ� �⺻������ /index �� ��û

# IIS ��ġ
- �ڽ��� : https://docs.microsoft.com/ko-kr/aspnet/web-forms/overview/deployment/visual-studio-web-deployment/deploying-to-iis
- Web Platform Installer : https://www.microsoft.com/web/downloads/platform.aspx
- IIS: .NET Extensibility 4.5
- Web Deploy 3.6 without bundled SQL support

# Controller���� View�� ������ ���� (ViewBag ���)
```
// Controller
            int hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour < 12 ? "Good Morning" : "Good Afternoon";

// View
    <div> 
        @ViewBag.Greeting World (from the view)
    </div>
```
- View���� ������ ��ũ �ɱ�
  - ù ��° �Ű����� : ��ũ�� ��µ� ���ڿ�
  - �� ��° �Ű����� : ����ڰ� ��ũ�� Ŭ������ �� ����� �׼��� �̸� (http://~/Home/RsvpForm)
  - Controller���� RsvpForm �޼ҵ� ����
```
@Html.ActionLink("RSVP Now", "RsvpForm")
```

# MVC �� Model �ۼ�
- ���� '������'���� �˷��� ������ �������α׷��� ������ �����ϴ� ���� ������ ��ü��� ������, �׸��� ��Ģ���� ��Ÿ����.
- ���� '������ ��'�̶�� �θ��⵵ �ϸ�, C# ��ü��� �� ��ü���� ������ �� �ִ� �޼����� �����ȴ�.
- �� ����� MVC�� �� ����� �𵨷κ��� ��ԵǸ�, �� ����� ���� ��Ʈ�ѷ��� �並 �߰��ϱ� ���� �����̴�.
```
        public string Phone { get; set; }
        public bool? WillAttend { get; set; }
```

# View���� �� �ۼ��ϱ� (BeginForm ���)
- Strongly Typed View ����
  - �� �߰� ---> Template�� Empty�� ���� ---> Model Class���� �ۼ��� �� Ŭ���� ����
  - �ۼ��� �� Ŭ������ �Ӽ��鿡 ���� ���� ������ HTML �Է� ��Ʈ�ѵ��� ������ �� �ִ�.
  - @Model �� �̿��Ͽ� Ŭ������ ���� ���� ����
```
<h1>Thank you, @Model.Name!</h1>
@if (Model.WillAttend == true)
{
	@: ���� ����
}
else
{
	@: �ƽ�
}
```
- HTML�� form ��Ҹ� �����ϵ��� ����
  - HTML ���� �޼ҵ� �� BeginForm ���
  - �ƹ��� �Ű������� �������� ������ ���� ���� �����ϴ� HTML ������ ������ URL�� ��û�ϴ� ������ ����
```
        @using (Html.BeginForm())
        {
		... �� ������ ���� ��ġ ...

		<input type="submit" value="Submit RSVP"/>
	}
```
- �� ���� �ȿ��� HTML ���� �޼ҵ� �� TextBoxFor ���
  - �ش� ���� �޼ҵ�� type�� text�� �����ϰ�, id�� name�� ���õ� Ŭ���� �Ӽ��� �̸����� �����Ѵ�.
  - �� ��° ���ڷ� ��Ʈ����Ʈ�� ������ �� �ִ� ������ object �Ű������� ������ �� �ִ�.
  - �� ��° ���ڷδ� CSS�� �߰��� Ŭ���� ���� ���´�.
```
<p>Your name : @Html.TextBoxFor(x => x.Name, new { @class = "form-control" })</p>
---> <input id="Phone" name="Phone" type="text" value="" class="form-control"/>
```
- �� ���� �ȿ��� HTML ���� �޼ҵ� �� DropDownListFor ���
```
                @Html.DropDownListFor(x => x.WillAttend, new[] {
                    new SelectListItem() { Text = "Yes, I'll be there",
                                            Value = bool.TrueString },
                    new SelectListItem() { Text = "No, I can't come",
                                            Value = bool.FalseString }
                    }, "Choose an option", new { @class = "form-control" })
```

# MVC �� Controller �ۼ� (�� ���ε�)
- Post �޼ҵ带 ���� '�� ���ε�'�� ���
- �� ���ε��� ���޵� �����͸� �м��ϰ� HTTP ��û�� ���Ե� Ű���� �ֵ��� �̿��ؼ� ������ �� ������ �Ӽ����� �ڵ����� ä���ش�.
- (��ȿ�� �˻� �� �����ص�)�䰡 ������ �� �Է��ߴ� �����͵��� �����Ǿ� �ٽ� ��µǴ� ������ ����
- ���� View�� ù ��° �Ű������� �ش� View�� ���� �ֵ��� �Ѵ�.
  - Thanks ---> Thanks.cshtml, GuestResponse ��ü�� �ش� �信 ����
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

# Model Ŭ������ ��ȿ�� �˻� �߰��ϱ�
- Model���� using System.ComponentModel.DataAnnotations; ���
- Controller���� ModelState.IsValid �� �˻�
- View���� HTML ���� �޼ҵ� �� ValidationSummary() ���
```
// Model Ŭ��������
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your emaill address")]
        [RegularExpression(".+\\@.+\\..+",
            ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }

	// ���� �κ� ��ȿ�� �˻�
	[Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal Price { get; set; }

// Controller ����
        [HttpPost]
        public ViewResult RsvpForm(GuestResponse guestResponse)
        {
            if (ModelState.IsValid)
                return View("Thanks", guestResponse);
            else
                return View();
        }

// View ����
        @using (Html.BeginForm())
        {
            @Html.ValidationSummary()
            <p>Your name : @Html.TextBoxFor(x => x.Name)</p>

	// ���� �޽��� �����ֱ� : @Html.ValidationMessage(�±��� name �Ӽ�)
	...

```
- ��ȿ�� �˻� ���� �� input �±׿� input-validation-error ��� Ŭ������ �ڵ����� �߰��ȴ�.
  - �̸� �̿��� ��ȿ���� ���� �ʵ忡 ���� ���� ó���� �� �� �ִ�.
  - Content ���� ���� �� �ȿ� ���� ���ϵ��� �ִ´� (.css ��)
```
// css ����
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

// View ����
<link rel="stylesheet" type="text/css" href="~/Content/Styles.css"/>
```

# NuGet�� �̿��� �ʿ� ���̺귯�� ��ġ
- ��� ���̺귯���� �ٿ�ε� �� �� �ش� ���̺귯���� �������� ���� �ٸ� ���̺귯���鵵 �Բ� �ٿ�ȴ�.
- ���� ---> Nuget ��Ű�� ������ ---> �ַ�ǿ� NuGet ��Ű�� ���� ���� �ʿ��� ���̺귯�� �˻� �� ��ġ
- Visual Studio�� ������ �´� ���̺귯�� ������ ã�Ƽ� �ٿ�ε� �ϵ��� ����

# WebMail ���� �޼��带 ���� �ڵ� ���ڸ��� ���� ���� �ۼ�
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

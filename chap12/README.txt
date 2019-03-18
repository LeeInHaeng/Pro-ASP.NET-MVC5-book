# 인증을 위해 폼 인증 방식 사용
- WebUI 프로젝트의 Web.config 파일에 추가
- system.web 태그 내에 추가한다.
  - loginUrl 은 사용자가 인증을 필요로 할 때, 사용자 요청을 돌려보내 처리할 주소
  - timeout은 성공적으로 로그인 후, 인증을 유지할 수 있는 시간 (단위 : 분)
  - credentials 태그 : 간단한 예제를 위해 로컬에서 로그인 처리 (DB를 통한 인증 X)
```
    <authentication mode="Forms">
      <forms loginUrl="~/Account/Login" timeout="2880">
        <credentials passwordFormat="Clear">
          <user name="admin" password="secret"/>
        </credentials>
      </forms>
    </authentication>
```
- Controller에 Authorize 필터 적용
```
    [Authorize]
    public class AdminController : Controller
	...
```
- 인증을 위한 인터페이스 작성
```
    public interface IAuthProvider
    {
        bool Authenticate(string username, string password);
    }
```
- 해당 인터페이스 구현
```
using System.Web.Security;

    public class FormsAuthProvider : IAuthProvider
    {
        public bool Authenticate(string username, string password)
        {
            bool result = FormsAuthentication.Authenticate(username, password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }
            return result;
        }
    }
```
- Ninject의 AddBinding에서 바인딩 처리
```
kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
```
- 인증 관련 Model을 만들고, Controller에서 해당 인터페이스를 통해 인증 후 View 에서 처리
- 위의 방법은 인증을 위한 좋은 방법은 아니기 때문에 이를 참고해서 다른 방법을 강구
  - https://github.com/Apress/pro-asp.net-mvc-5 에서 Additional content 참고

# 이미지 업로드 기능 추가
- 기존의 데이터베이스에 이미지 관련 컬럼 추가
```
ALTER TABLE [dbo].[Products]
	ADD [ImageData] VARBINARY(MAX)	NULL,
		[ImageMimeType] VARCHAR(50) NULL;
```
- Product 모델에 해당 타입들 추가
  - DB에서 VARBINARY는 byte[] 타입, VARCHAR은 string 타입
```
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
```
- View에서 이미지 업로드를 위한 UI와 BeginForm 작성
  - BeginForm 매개변수 : 액션 메소드, 컨트롤러, 메소드 명, 이미지 전송을 위한 속성
  - ImageData와 ImageMimeType은 input 태그가 생성되지 않도록 조건 처리
  - 이미지 업로드 시 Product 컨트롤러의 GetImage 액션 메소드를 수행하도록 한다.
  - 해당 액션 메소드는 이미지 태그 삽입을 위해 Url.Action 사용
```
    @using (Html.BeginForm("Edit","Admin",FormMethod.Post,
        new { enctype = "multipart/form-data" }))
    {

	...

	// foreach 내에서
                switch (property.PropertyName)
                {
                    case "ProductID":
                    case "ImageData":
                    case "ImageMimeType":
			break;
		    default:
			원래 동작
		}

        <div class="form-group">
            <div style="position:relative; ">
                <label>Image</label>
                <a class="btn" href="javascript:;">
                    Choose File...
                    <input type="file" name="Image" size="40"
                           style="position:absolute; z-index:2; top:0; 
                                left:0; filter:alpha(opacity=0); opacity:0; 
                                background-color:transparent; color:transparent;"
                           onchange='$("#upload-file-info").html($(this).val());' />
                </a>
                <span class="label label-info" id="upload-file-info"></span>
            </div>
            @if(Model.ImageData == null)
            {
                <div class="form-control-static">No Image</div>
            }
            else
            {
                <img class="img-thumbnail" width="150" height="150"
                     src="@Url.Action("GetImage","Product",
                         new {Model.ProductID})" />
            }
        </div>
```
- Controller에서 이미지 처리
  - form 태그에서 넘어온 이미지에 대한 정보를 매개 변수로 HttpPostedFileBase 객체로 받는다.
  - 이미지가 존재하는 경우 Product 객체에 이미지에 대한 정보들을 담는다.
```
        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if(image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
		repository.SaveProduct(product);
	...
```
- Repository 동작 또한 Product의 Image 관련 속성들을 통해 DB에 업데이트 한다.
- 이미지 업로드시 위에서 호출한 GetImage 액션 메소드를 구현한다.
  - FileContentResult를 반환 타입으로 한다.
```
        public FileContentResult GetImage(int productId)
        {
            Product prod = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            if(prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
```
- 상품을 보여줄 View에서도 이미지 태그 삽입
  - Url.Action을 이용해 이미지 태그가 삽입되도록 동작
```
    @if(Model.ImageData != null)
    {
        <div class="pull-left" style="margin-right: 10px">
            <img class="img-thumbnail" width="75" height="75"
                 src="@Url.Action("GetImage", "Product",
                     new {Model.ProductID})" />
        </div>
    }
```
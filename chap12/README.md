# ������ ���� �� ���� ��� ���
- WebUI ������Ʈ�� Web.config ���Ͽ� �߰�
- system.web �±� ���� �߰��Ѵ�.
  - loginUrl �� ����ڰ� ������ �ʿ�� �� ��, ����� ��û�� �������� ó���� �ּ�
  - timeout�� ���������� �α��� ��, ������ ������ �� �ִ� �ð� (���� : ��)
  - credentials �±� : ������ ������ ���� ���ÿ��� �α��� ó�� (DB�� ���� ���� X)
```
    <authentication mode="Forms">
      <forms loginUrl="~/Account/Login" timeout="2880">
        <credentials passwordFormat="Clear">
          <user name="admin" password="secret"/>
        </credentials>
      </forms>
    </authentication>
```
- Controller�� Authorize ���� ����
```
    [Authorize]
    public class AdminController : Controller
	...
```
- ������ ���� �������̽� �ۼ�
```
    public interface IAuthProvider
    {
        bool Authenticate(string username, string password);
    }
```
- �ش� �������̽� ����
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
- Ninject�� AddBinding���� ���ε� ó��
```
kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
```
- ���� ���� Model�� �����, Controller���� �ش� �������̽��� ���� ���� �� View ���� ó��
- ���� ����� ������ ���� ���� ����� �ƴϱ� ������ �̸� �����ؼ� �ٸ� ����� ����
  - https://github.com/Apress/pro-asp.net-mvc-5 ���� Additional content ����

# �̹��� ���ε� ��� �߰�
- ������ �����ͺ��̽��� �̹��� ���� �÷� �߰�
```
ALTER TABLE [dbo].[Products]
	ADD [ImageData] VARBINARY(MAX)	NULL,
		[ImageMimeType] VARCHAR(50) NULL;
```
- Product �𵨿� �ش� Ÿ�Ե� �߰�
  - DB���� VARBINARY�� byte[] Ÿ��, VARCHAR�� string Ÿ��
```
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
```
- View���� �̹��� ���ε带 ���� UI�� BeginForm �ۼ�
  - BeginForm �Ű����� : �׼� �޼ҵ�, ��Ʈ�ѷ�, �޼ҵ� ��, �̹��� ������ ���� �Ӽ�
  - ImageData�� ImageMimeType�� input �±װ� �������� �ʵ��� ���� ó��
  - �̹��� ���ε� �� Product ��Ʈ�ѷ��� GetImage �׼� �޼ҵ带 �����ϵ��� �Ѵ�.
  - �ش� �׼� �޼ҵ�� �̹��� �±� ������ ���� Url.Action ���
```
    @using (Html.BeginForm("Edit","Admin",FormMethod.Post,
        new { enctype = "multipart/form-data" }))
    {

	...

	// foreach ������
                switch (property.PropertyName)
                {
                    case "ProductID":
                    case "ImageData":
                    case "ImageMimeType":
			break;
		    default:
			���� ����
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
- Controller���� �̹��� ó��
  - form �±׿��� �Ѿ�� �̹����� ���� ������ �Ű� ������ HttpPostedFileBase ��ü�� �޴´�.
  - �̹����� �����ϴ� ��� Product ��ü�� �̹����� ���� �������� ��´�.
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
- Repository ���� ���� Product�� Image ���� �Ӽ����� ���� DB�� ������Ʈ �Ѵ�.
- �̹��� ���ε�� ������ ȣ���� GetImage �׼� �޼ҵ带 �����Ѵ�.
  - FileContentResult�� ��ȯ Ÿ������ �Ѵ�.
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
- ��ǰ�� ������ View������ �̹��� �±� ����
  - Url.Action�� �̿��� �̹��� �±װ� ���Եǵ��� ����
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
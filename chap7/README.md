# ������Ʈ ����
- ���� ��Ÿ ������Ʈ ���� ---> Visual Studio �ַ�� ---> �� �ַ���� ����
- ������Ʈ ��Ŭ�� ---> �߰� ---> �� ������Ʈ ---> Ŭ���� ���̺귯��
  - SportsStore.Domain ������Ʈ�� �������丮�� ���� �����͸� �����ϱ� ���� ������Ʈ
  - �ڵ����� ������� Class1.cs�� ����
- ������Ʈ ��Ŭ�� ---> �߰� ---> �� ������Ʈ ---> ASP.NET �� ���� ���α׷�
  - SportsStore.WebUI ������Ʈ�� ��Ʈ�ѷ��� �並 ��� ������ ����� �������̽��� ����
  - Empty ���ø��� MVC�� üũ
  - ����� ���Ǹ� ���� �ش� ������Ʈ�� ��Ŭ�� �� ���� ������Ʈ�� ����
- ������Ʈ ��Ŭ�� ---> �߰� ---> �� ������Ʈ ---> ���� �׽�Ʈ ������Ʈ
  - SportsStore.UnitTests ������Ʈ�� �ٸ� �� ������Ʈ�� ���� ���� �׽�Ʈ�� �ۼ�
- WebUI�� �ʿ��� ���̺귯��
  - Ninject, Ninject.Web.Common.WebHost, Ninject.MVC5, Moq
- Domain�� �ʿ��� ���̺귯��
  - Microsoft.AspNet.Mvc
- UnitTests�� �ʿ��� ���̺귯��
  - Ninject, Ninject.Web.Common.WebHost, Ninject.MVC5, Moq, Microsoft.AspNet.Mvc
- WebUI ���� �ַ�� ������ �� ����� ���� �߰�
  - ������Ʈ�� ���� ��Ŭ�� ---> ���� �߰� ---> ������Ʈ �ǿ��� Domain üũ
- Domain ���� �ַ�� ������ �� ����� ���� �߰�
  - ������Ʈ�� ���� ��Ŭ�� ---> ���� �߰� ---> System.ComponentModel.DataAnnotations �߰�
- UnitTests ���� �ַ�� ������ �� ����� ���� �߰�
  - ������Ʈ�� ���� ��Ŭ�� ---> ���� �߰� ---> ������Ʈ �ǿ��� Domain�ϰ� WebUI üũ
  - ������Ʈ�� ���� ��Ŭ�� ---> ���� �߰� ---> System.Web, Microsoft.CSharp �߰�
- WebUI���� DI �����̳� ����� ���� Infrastructure ������ �����ϰ� DependencyResolver Ŭ���� ���� ��, NinjectWebCommon���� ���
```
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {

        }
    }
```
```
        private static void RegisterServices(IKernel kernel)
        {
            System.Web.Mvc.DependencyResolver.SetResolver(
                new SportsStore.WebUI.Infrastructure.NinjectDependencyResolver(kernel));
        }  
```

# ������Ʈ �ۼ� ����(1) : ������ �� �ۼ�
- Domain ������Ʈ�� Entities ���� ���� �� �����ο� ����� Ŭ���� ���� (Ŭ���� �� ���� �ʵ嵵 public Ÿ��)
- �߻� �������丮 �����ϱ� ---> �������丮 ����
  - Abstract ���� �߰� �� IŬ������Repository �������̽��� ����
  - �ش� �������̽��� �����ϴ� Ŭ������ �����Ͱ� ��� ������, �������̽��� ������ Ŭ������ �� �����͸� ��� �����ϴ��� ���� ���� ���ϴ��� Product ��ü���� ���� �� �ִ�.
```
using SportsStore.Domain.Entities;

    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
    }
```
- ���Ŀ� �ʿ��� ������ �Ǹ� �𵨰� �������丮�� ����� �߰�

# ������Ʈ �ۼ� ����(2) : WebUI���� ��Ʈ�ѷ� �ۼ�
- Domain���� �������̽��� ������ Repository�� ����Ѵ�.
- ��Ʈ�ѷ��� �����ڿ��� �����ϵ��� �Ѵ�.
- ViewResult���� �Ѱ��ִ� ���� repository�� Products �޼ҵ� ����� �Ѱ� �ֵ��� �Ѵ�.
```
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

    public class ProductController : Controller
    {
        private IProductRepository repository;

        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }

        public ViewResult List()
        {
            return View(repository.Products);
        }
    }
```

# ������Ʈ �ۼ� ����(3) : WebUI���� View �ۼ�
- View �ۼ� �� Template�� Empty�� �����ϰ�, Ŭ������ Domain ������Ʈ�� Product�� �����ϵ��� �Ѵ�.
- �̶� Use a layout page�� üũ�� �ϸ� ������Ʈ���� �ڵ����� ViewStart�� Shared/Layout.cshtml�� ����� �ش�.
- Layout�� body�� RenderBody�� ����Ѵ�.
```
<body>
    <script src="~/Scripts/jquery-1.10.2.min.js"></script>
    <script src="~/Scripts/bootstrap.min.js"></script>

    <div>
        @RenderBody()
    </div>
</body>
```
- ���� ���� View ������ ������ model�� ����� �ִ� �κ��� Controller���� �Ѱ��ִ� Ÿ�Կ� �°� �������ش�.
```
// ������ ���� �ִ� �κ� ---> @model SportsStore.Domain.Entities.Product

@using SportsStore.Domain.Entities
@model IEnumerable<Product>
```
- foreach�� ���� ������ �� �ִ�.
```
@foreach(var p in Model)
{
    <div>
        <h3>@p.Name</h3>
        @p.Description
        <h4>@p.Price.ToString("c")</h4>
    </div>
}
```
- �̷��� ������ ���� View�� �⺻ ���Ʈ�� ������ �� �ִ�.
  - RouteConfig.cs���� defaults �κ��� controller���� controller �̸�, action���� View �̸��� ���´�.
```
defaults: new { controller = "Product", action = "List", id = UrlParameter.Optional }
```

# ������Ʈ �ۼ� ����(4) : DB �����ϱ�
- ���� ---> SQL Server ��ü Ž���� ���� (localdb) �̸� Ȯ��
- ���� ---> ���� Ž���� ---> �����ͺ��̽��� ���� ������ Ŭ��
- ������ �ҽ��� Microsoft SQL Server�� ���� �� Continue
- Server name�� ó���� Ȯ���ߴ� (localdb)\ProjectsV13 ���� ���������ν� LocalDB ����� ����Ѵٴ� �ǵ��� �˷��ش�.
- ������ Windows ����, �����ͺ��̽� �̸��� SportsStore�� �Է� �� Ȯ��
- ���̺� ---> �� ���� ���� ���� Model���� �����ߴ� �Ӽ��鿡 ���� ���̺� ����
```
CREATE TABLE Products
(
	[ProductID] INT NOT NULL PRIMARY KEY IDENTITY,
	[NAME] NVARCHAR(100) NOT NULL,
	[Description] NVARCHAR(500) NOT NULL,
	[Category] NVARCHAR(50) NOT NULL,
	[Price] DECIMAL(16,2) NOT NULL
)
```

# ORM ��� (Entity Framework ���)
- Domain ������Ʈ�� WebUI ������Ʈ�� EntityFramework(EF) ���̺귯�� ��ġ
- Domain ������Ʈ���� Concrete ������ ����� EFDbContext Ŭ���� ������ �߰�
  - �ش� Ŭ������ EntityFramework(EF)�� Code-First ����� ����ϱ� ���� DbContext�� ��� �޴´�.
  - DbSet ������ ���׸� Ÿ���� �� Ÿ���� �����ϰ�, ������ �̸��� �۾��� ���̺��� �����ش�.
  - �� Products ���̺��� ���� ǥ���ϴµ� EF�� Product��� �� ������ ����ؾ� ���� �ǹ��Ѵ�.
```
using SportsStore.Domain.Entities;
using System.Data.Entity;

    public class EFDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
```
- Domain ������Ʈ���� Concrete ���� �ȿ� EF�� ����� Repository Ŭ������ �ۼ��Ѵ�.
  - �ش� �������丮�� IProductRepository �������̽��� �����ϵ��� �ۼ��Ѵ�.
  - ���� EFDbContext�� �ν��Ͻ��� ����� �����ͺ��̽��κ��� �����͸� ��ȸ�Ѵ�.
```
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

    public class EFProductRepository : IProductRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Product> Products
        {
            get
            {
                return context.Products;
            }
        }
    }
```

- WebUI ������Ʈ�� Web.config ���Ͽ��� EF�� �����ͺ��̽��� ã�� �� �ֵ��� ������ �˷� �־�� �Ѵ�.
  - appSettings �±� �����ٰ� ������ �ش�.
  - name�� Model ������Ʈ���� ������� Ŭ���� ���� �����ش�.
  - Data Source�� DB ������ �ʿ��ߴ� Server name�� �����ش�.
  - Initial Catalog�� �����ͺ��̽� �̸��� �����ش�.
```
  <connectionStrings>
    <add name="EFDbContext" connectionString="Data Source=(localdb)\ProjectsV13; 
         Initial Catalog=SportsStore; Integrated Security=True"
         providerName="System.Data.SqlClient"/>
  </connectionStrings>
```

- WebUI ������Ʈ�� DependencyReslover Ŭ�������� �ռ� �ۼ��� EFRepository�� ���ε� �Ѵ�.
```
using SportsStore.Domain.Concrete;

        private void AddBindings()
        {
            kernel.Bind<IProductRepository>().To<EFProductRepository>();
        }
```

# ORM ��� (Dapper, ���� ���ν���, ���� SQL ����)
- Domain ������Ʈ�� WebUI ������Ʈ�� Dapper ���̺귯�� ��ġ
- Web.config���� DB Connection ����
```
  <connectionStrings>
    <add name="ConnectionString" connectionString="Data Source=(localdb)\ProjectsV13; 
         Initial Catalog=SportsStore; Integrated Security=True"
         providerName="System.Data.SqlClient"/>
  </connectionStrings>
```
- ���� �߰� ---> System.Configuration
- DapperRepository Ŭ���� �ۼ�
  - �ش� �������丮�� IProductRepository �������̽��� �����ϵ��� �ۼ��Ѵ�.
- ���� SQL�� ����ϴ� ���
```
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

    public class DapperRepository : IProductRepository
    {
        private IDbConnection db = new SqlConnection(
          ConfigurationManager.ConnectionStrings[
              "ConnectionString"].ConnectionString);

        public IEnumerable<Product> Products
        {
            get
            {
                string sql = "SELECT * FROM Products";
                return this.db.Query<Product>(sql);
            }
        }
    }
```
- ���� ���ν����� ����ϴ� ���
```
// ���� ���̺��� ���� ���ν��� ���
CREATE Procedure [dbo].[GetProducts]    
as    
begin    
   select * from Products  
End

...

        public IEnumerable<Product> Products
        {
            get
            {
                return this.db.Query<Product>("GetProducts"); // ��ȣ �ȿ� ���ν��� �̸� ���
            }
        }
```
- ���� ���ν����� ����ϴ� ���2
```
CREATE PROCEDURE GetProducts2
(
	@category varchar(30),
	@price decimal		
)
AS
begin
select * from Products where Category = @category and Price>@price
end

...

public IEnumerable<Product> Products
        {
            get
            {
                return this.db.Query<Product>("GetProducts2",
                                                new { category = "Chess",
                                                        price = 30},
                                                commandType: CommandType.StoredProcedure);
            }
        }
```
- WebUI ������Ʈ�� DependencyReslover Ŭ�������� �ռ� �ۼ��� DapperRepository�� ���ε� �Ѵ�.
```
        private void AddBindings()
        {
            kernel.Bind<IProductRepository>().To<DapperRepository>();
        }
```

# ����¡ ó���ϱ� (PagedList ���)
- ���� : https://t2t2tt.tistory.com/28
- WebUI ������Ʈ���� PagedList.Mvc ���̺귯�� ��ġ
- Controller���� PagedList�� �̿��Ͽ� �޼ҵ带 �ۼ��Ѵ�.
- int? Page : ����Ʈ �������� �ҷ��ö� null �� ����ϴ� int Page �� �޾ƿ´�.
  - Page �� ���� ȣ��Ǵ� ������ ��ȣ�� ����Ǿ��ִ�.
- viewData : �������丮�� �̿��� �����͸� �����´�
- int PageNo = Page ?? 1 : ���� ������ ������ ���ٸ� 1�������� �����ϰ�, �ƴϸ� ������ ������ ��´�.
- int PageSize = 4; : �� �������� ������ �������� ����
- �ش� �������� �������� View�� �����͸� ����
```
        public ViewResult List(int? Page)
        {
            var viewData = repository.Products;
            int PageNo = Page ?? 1;

            return View(viewData.ToPagedList(PageNo, PageSize));
        }
```
- View���� ������ ����Ʈ �� ��ü ������ �׷��ֱ�
```
@using SportsStore.Domain.Entities
@using PagedList.Mvc
@model PagedList.IPagedList<Product>

<link href="~/Content/PagedList.css" rel="stylesheet" type="text/css" />

...

@Html.PagedListPager(Model, Page => Url.Action("List", new { Page }))
```

# URL �����ϱ� (RouteConfig)
- ������ Ŭ�� �� "/?Page=2" ����� �ƴ� "/Page2" ����� �ǵ��� ����
- WebUI ������Ʈ�� App_Start ������ RouteConfig.cs ���� ���� �߰�
  - ������ routes.Maproute�� �����ϴ� ���� �ƴ϶� �̾ �߰�
```
            routes.MapRoute(
                name: null,
                url: "Page{Page}",
                defaults: new { controller = "Product", action = "List"}
            );
```

# �κ� �� �����ϱ�
- �κ� ��� �� ��ü�� ������ ���� ���� �信�� ���� �� �� �ִ� ������ �ִ�.
- ���� �ش� �並 ���� ���� ������ �ʿ䰡 �ִٸ� �� ������� �ߺ��� ���� �� �ִ�.
- WebUI ������Ʈ�� Views/Shared �������� �߰� ---> ��
- �� �̸� ����, ���ø��� Empty��, Ŭ������ Product, Create as a partial view�� üũǥ��
- �ش� �κ� �並 �����ϰ� �ٹδ�.
- �κ� �並 ����Ѵ�.
  - Html.Partial���� ù ��° �Ű� ������ �κ� ���� �̸��� �����ش�.
  - �� ��° �Ű������� �κ� �信�� ����� �����͸� �����ش�.
```
@foreach(var p in Model)
{
    @Html.Partial("ProductSummary", p)
}
```
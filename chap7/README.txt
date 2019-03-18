# 프로젝트 분할
- 먼저 기타 프로젝트 형식 ---> Visual Studio 솔루션 ---> 빈 솔루션을 생성
- 프로젝트 우클릭 ---> 추가 ---> 새 프로젝트 ---> 클래스 라이브러리
  - SportsStore.Domain 프로젝트로 레파지토리를 통해 데이터를 저장하기 위한 프로젝트
  - 자동으로 만들어준 Class1.cs는 삭제
- 프로젝트 우클릭 ---> 추가 ---> 새 프로젝트 ---> ASP.NET 웹 응용 프로그램
  - SportsStore.WebUI 프로젝트로 컨트롤러와 뷰를 담고 있으며 사용자 인터페이스로 동작
  - Empty 템플릿에 MVC만 체크
  - 디버깅 편의를 위해 해당 프로젝트를 우클릭 후 시작 프로젝트로 설정
- 프로젝트 우클릭 ---> 추가 ---> 새 프로젝트 ---> 단위 테스트 프로젝트
  - SportsStore.UnitTests 프로젝트로 다른 두 프로젝트에 대한 단위 테스트를 작성
- WebUI에 필요한 라이브러리
  - Ninject, Ninject.Web.Common.WebHost, Ninject.MVC5, Moq
- Domain에 필요한 라이브러리
  - Microsoft.AspNet.Mvc
- UnitTests에 필요한 라이브러리
  - Ninject, Ninject.Web.Common.WebHost, Ninject.MVC5, Moq, Microsoft.AspNet.Mvc
- WebUI 에서 솔루션 의존성 및 어셈블리 참조 추가
  - 프로젝트의 참조 우클릭 ---> 참조 추가 ---> 프로젝트 탭에서 Domain 체크
- Domain 에서 솔루션 의존성 및 어셈블리 참조 추가
  - 프로젝트의 참조 우클릭 ---> 참조 추가 ---> System.ComponentModel.DataAnnotations 추가
- UnitTests 에서 솔루션 의존성 및 어셈블리 참조 추가
  - 프로젝트의 참조 우클릭 ---> 참조 추가 ---> 프로젝트 탭에서 Domain하고 WebUI 체크
  - 프로젝트의 참조 우클릭 ---> 참조 추가 ---> System.Web, Microsoft.CSharp 추가
- WebUI에서 DI 컨테이너 사용을 위해 Infrastructure 폴더를 생성하고 DependencyResolver 클래스 생성 후, NinjectWebCommon에서 등록
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

# 프로젝트 작성 순서(1) : 도메인 모델 작성
- Domain 프로젝트에 Entities 폴더 생성 후 도메인에 사용할 클래스 생성 (클래스 및 하위 필드도 public 타입)
- 추상 레파지토리 생성하기 ---> 레파지토리 패턴
  - Abstract 폴더 추가 후 I클래스명Repository 인터페이스를 생성
  - 해당 인터페이스에 의존하는 클래스는 데이터가 어디서 오는지, 인터페이스를 구현할 클래스가 그 데이터를 어떻게 전달하는지 전혀 알지 못하더라도 Product 객체들을 얻을 수 있다.
```
using SportsStore.Domain.Entities;

    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
    }
```
- 이후에 필요한 시점이 되면 모델과 레파지토리의 기능을 추가

# 프로젝트 작성 순서(2) : WebUI에서 컨트롤러 작성
- Domain에서 인터페이스로 생성한 Repository를 사용한다.
- 컨트롤러의 생성자에서 셋팅하도록 한다.
- ViewResult에서 넘겨주는 것은 repository의 Products 메소드 결과를 넘겨 주도록 한다.
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

# 프로젝트 작성 순서(3) : WebUI에서 View 작성
- View 작성 시 Template는 Empty로 선택하고, 클래스는 Domain 프로젝트의 Product를 선택하도록 한다.
- 이때 Use a layout page에 체크를 하면 프로젝트에서 자동으로 ViewStart와 Shared/Layout.cshtml을 만들어 준다.
- Layout의 body는 RenderBody를 사용한다.
```
<body>
    <script src="~/Scripts/jquery-1.10.2.min.js"></script>
    <script src="~/Scripts/bootstrap.min.js"></script>

    <div>
        @RenderBody()
    </div>
</body>
```
- 또한 만든 View 파일은 기존의 model로 선언되 있는 부분을 Controller에서 넘겨주는 타입에 맞게 변경해준다.
```
// 기존에 적혀 있던 부분 ---> @model SportsStore.Domain.Entities.Product

@using SportsStore.Domain.Entities
@model IEnumerable<Product>
```
- foreach를 통해 접근할 수 있다.
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
- 이러한 식으로 만든 View를 기본 라우트로 설정할 수 있다.
  - RouteConfig.cs에서 defaults 부분의 controller에는 controller 이름, action에는 View 이름을 적는다.
```
defaults: new { controller = "Product", action = "List", id = UrlParameter.Optional }
```

# 프로젝트 작성 순서(4) : DB 연동하기
- 보기 ---> SQL Server 개체 탐색기 에서 (localdb) 이름 확인
- 보기 ---> 서버 탐색기 ---> 데이터베이스에 연결 아이콘 클릭
- 데이터 소스를 Microsoft SQL Server로 선택 후 Continue
- Server name은 처음에 확인했던 (localdb)\ProjectsV13 으로 지정함으로써 LocalDB 기능을 사용한다는 의도를 알려준다.
- 인증은 Windows 인증, 데이터베이스 이름은 SportsStore를 입력 후 확인
- 테이블 ---> 새 쿼리 에서 앞의 Model에서 정의했던 속성들에 대해 테이블 생성
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

# ORM 사용 (Entity Framework 사용)
- Domain 프로젝트와 WebUI 프로젝트에 EntityFramework(EF) 라이브러리 설치
- Domain 프로젝트에서 Concrete 폴더를 만들고 EFDbContext 클래스 파일을 추가
  - 해당 클래스는 EntityFramework(EF)의 Code-First 기능을 사용하기 위해 DbContext를 상속 받는다.
  - DbSet 이후의 제네릭 타입은 모델 타입을 지정하고, 이후의 이름은 작업할 테이블을 적어준다.
  - 즉 Products 테이블에서 행을 표현하는데 EF가 Product라는 모델 형식을 사용해야 함을 의미한다.
```
using SportsStore.Domain.Entities;
using System.Data.Entity;

    public class EFDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
```
- Domain 프로젝트에서 Concrete 폴더 안에 EF로 사용할 Repository 클래스를 작성한다.
  - 해당 레파지토리는 IProductRepository 인터페이스를 구현하도록 작성한다.
  - 또한 EFDbContext의 인스턴스를 사용해 데이터베이스로부터 데이터를 조회한다.
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

- WebUI 프로젝트의 Web.config 파일에서 EF가 데이터베이스를 찾을 수 있도록 정보를 알려 주어야 한다.
  - appSettings 태그 위에다가 선언해 준다.
  - name은 Model 프로젝트에서 만들었던 클래스 명을 적어준다.
  - Data Source는 DB 연동에 필요했던 Server name을 적어준다.
  - Initial Catalog는 데이터베이스 이름을 적어준다.
```
  <connectionStrings>
    <add name="EFDbContext" connectionString="Data Source=(localdb)\ProjectsV13; 
         Initial Catalog=SportsStore; Integrated Security=True"
         providerName="System.Data.SqlClient"/>
  </connectionStrings>
```

- WebUI 프로젝트의 DependencyReslover 클래스에서 앞서 작성한 EFRepository를 바인딩 한다.
```
using SportsStore.Domain.Concrete;

        private void AddBindings()
        {
            kernel.Bind<IProductRepository>().To<EFProductRepository>();
        }
```

# ORM 사용 (Dapper, 저장 프로시저, 순수 SQL 쿼리)
- Domain 프로젝트와 WebUI 프로젝트에 Dapper 라이브러리 설치
- Web.config에서 DB Connection 설정
```
  <connectionStrings>
    <add name="ConnectionString" connectionString="Data Source=(localdb)\ProjectsV13; 
         Initial Catalog=SportsStore; Integrated Security=True"
         providerName="System.Data.SqlClient"/>
  </connectionStrings>
```
- 참조 추가 ---> System.Configuration
- DapperRepository 클래스 작성
  - 해당 레파지토리는 IProductRepository 인터페이스를 구현하도록 작성한다.
- 순수 SQL을 사용하는 경우
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
- 저장 프로시저를 사용하는 경우
```
// 만든 테이블에서 저장 프로시저 등록
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
                return this.db.Query<Product>("GetProducts"); // 괄호 안에 프로시저 이름 사용
            }
        }
```
- 저장 프로시저를 사용하는 경우2
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
- WebUI 프로젝트의 DependencyReslover 클래스에서 앞서 작성한 DapperRepository를 바인딩 한다.
```
        private void AddBindings()
        {
            kernel.Bind<IProductRepository>().To<DapperRepository>();
        }
```

# 페이징 처리하기 (PagedList 사용)
- 설명 : https://t2t2tt.tistory.com/28
- WebUI 프로젝트에서 PagedList.Mvc 라이브러리 설치
- Controller에서 PagedList를 이용하여 메소드를 작성한다.
- int? Page : 리스트 페이지가 불러올때 null 을 허용하는 int Page 를 받아온다.
  - Page 는 현재 호출되는 페이지 번호가 저장되어있다.
- viewData : 레파지토리를 이용해 데이터를 가져온다
- int PageNo = Page ?? 1 : 현재 페이지 정보가 없다면 1페이지로 간주하고, 아니면 페이지 정보를 담는다.
- int PageSize = 4; : 한 페이지에 보여줄 데이터의 숫자
- 해당 정보들을 바탕으로 View에 데이터를 전달
```
        public ViewResult List(int? Page)
        {
            var viewData = repository.Products;
            int PageNo = Page ?? 1;

            return View(viewData.ToPagedList(PageNo, PageSize));
        }
```
- View에서 페이지 리스트 및 전체 페이지 그려주기
```
@using SportsStore.Domain.Entities
@using PagedList.Mvc
@model PagedList.IPagedList<Product>

<link href="~/Content/PagedList.css" rel="stylesheet" type="text/css" />

...

@Html.PagedListPager(Model, Page => Url.Action("List", new { Page }))
```

# URL 개선하기 (RouteConfig)
- 페이지 클릭 시 "/?Page=2" 방식이 아닌 "/Page2" 방식이 되도록 설정
- WebUI 프로젝트의 App_Start 폴더에 RouteConfig.cs 에서 내용 추가
  - 기존의 routes.Maproute를 삭제하는 것이 아니라 이어서 추가
```
            routes.MapRoute(
                name: null,
                url: "Page{Page}",
                defaults: new { controller = "Product", action = "List"}
            );
```

# 부분 뷰 생성하기
- 부분 뷰는 그 자체의 파일을 갖고 여러 뷰에서 재사용 할 수 있는 장점이 있다.
- 만약 해당 뷰를 여러 곳에 렌더할 필요가 있다면 이 방법으로 중복을 줄일 수 있다.
- WebUI 프로젝트의 Views/Shared 폴더에서 추가 ---> 뷰
- 뷰 이름 지정, 템플릿은 Empty로, 클래스는 Product, Create as a partial view에 체크표시
- 해당 부분 뷰를 적절하게 꾸민다.
- 부분 뷰를 사용한다.
  - Html.Partial에서 첫 번째 매개 변수는 부분 뷰의 이름을 적어준다.
  - 두 번째 매개변수는 부분 뷰에서 사용할 데이터를 적어준다.
```
@foreach(var p in Model)
{
    @Html.Partial("ProductSummary", p)
}
```
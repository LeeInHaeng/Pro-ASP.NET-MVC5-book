# 가변 길이의 라우트 정의
- id 옵션에 UrlParameter.Optional 을 사용하면 해당 세그먼트가 선택적 세그먼트임을 정의한다.
- catchall 변수 앞에 ```*``` 붙이는 것으로 뒤에 가변 길이를 갖는 라우트를 정의할 수 있다.
```
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{*catchall}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
```
- 맵핑되는 형태는 다음과 같다
  - / : controller = Home, action = Index
  - /Customer : controller = Customer, action = Index
  - /Customer/List : controller = Customer, action = List
  - /Customer/List/All : controller = Customer, action = List, id = All
  - /Customer/List/All/Delete : controller = Customer, action = List, id = All, catchall = Delete
  - /Customer/List/All/Delete/Perm : controller = Customer, action = List, id = All, catchall = Delete/Perm

# 라우트에 제약조건 설정
- URL 세그먼트가 지정한 값들과만 매치되도록 정규표현식을 이용해 제한할 수 있다.
```
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}/{*catchall}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new { controller = "^H.", action = "^Index$ | ^About$"}
            );
```
- HTTP 메소드를 사용한 제약조건
```
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}/{*catchall}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new { controller = "^H.", action = "^Index$ | ^About$",
                        httpMethod = new HttpMethodConstraint("GET","POST") }
            );
```
- 형식 및 값 제약조건
  - AlphaRouteConstraint : 대소문자에 관계 없이 알파벳 문자와 매치
  - BoolRouteConstraint : Bool로 파싱할 수 있는 값과 매치
  - DateTimeRouteConstraint : DateTime으로 파싱할 수 있는 값과 매치
  - DecimalRouteConstraint : Decimal로 파싱할 수 있는 값과 매치
  - DoubleRouteConstraint : Double로 파싱할 수 있는 값과 매치
  - FloatRouteConstraint : Float로 파싱할 수 있는 값과 매치
  - IntRouteConstraint : Int로 파싱할 수 있는 값과 매치
  - LengthRouteConstraint(len 혹은 min, max) : 값의 길이에 해당할 경우에만 매치
  - LongRouteConstraint : Long으로 파싱할 수 있는 값과 매치
  - MaxRouteConstraint(val) : val보다 작은 int 값과 매치
  - MaxLengthRouteConstraint(len) : len의 길이보다 작은 문자열과 매치
  - MinRouteConstraint(val) : val보다 큰 int 값과 매치
  - MinLengthRouteConstraint(len) : len의 길이보다 큰 문자열과 매치
  - RangeRouteConstraint(min, max) : min과 max 사이에 오는 int 값과 매치
```
using System.Web.Mvc.Routing.Constraints;

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}/{*catchall}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new { controller = "^H.", action = "^Index$ | ^About$",
                        httpMethod = new HttpMethodConstraint("GET","POST"),
                        id = new RangeRouteConstraint(10,20) }
            );
```
- CompoundRouteConstraint를 이용해 여러 제약조건을 동시에 사용 가능
  - id 값에 최소 6글자의 알파벳 문자로만 구성된 문자열 값들과만 매치된다.
```
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}/{*catchall}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new
                {
                    controller = "^H.",
                    action = "^Index$ | ^About$",
                    httpMethod = new HttpMethodConstraint("GET", "POST"),
                    id = new CompoundRouteConstraint(new IRouteConstraint[] {
                            new AlphaRouteConstraint(),
                            new MinLengthRouteConstraint(6) })
                }
            );
```

# 어트리뷰트 라우팅 활성화 및 적용하기
- MVC5 부터 추가된 라우팅 기능이다.
- 해당 기능을 사용하기 위해 RouteConfig.cs 파일에 해당 구문을 추가한다.
```
routes.MapMvcAttributeRoutes();
```
- 이후의 라우팅을 위한 작업들은 각각의 Controller의 액션 메소드에서 이루어진다.
```
    public class CustomerController : Controller
    {
        // GET: Customer
        [Route("Test")]
        public ActionResult Index()
```
- 세그먼트 변수 이용 및 제약조건 설정
```
        [Route("Users/Add/{user}/{id:int}")]
        public ActionResult Create(string user, int id)
```
- 제약조건 결합하기
```
        [Route("Users/Add/{user}/{password:alpha:length(6)}")]
        public ActionResult ChangePass(string user, string password)
```
- 라우트 접두어 사용
```
    [RoutePrefix("Users")]
    public class CustomerController : Controller
    {
        [Route("~/Test")]
        public ActionResult Index()

        [Route("Add/{user}/{id:int}")]
        public ActionResult Create(string user, int id)
```

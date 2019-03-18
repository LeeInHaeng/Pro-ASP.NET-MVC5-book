# ���� ������ ���Ʈ ����
- id �ɼǿ� UrlParameter.Optional �� ����ϸ� �ش� ���׸�Ʈ�� ������ ���׸�Ʈ���� �����Ѵ�.
- catchall ���� �տ� ```*``` ���̴� ������ �ڿ� ���� ���̸� ���� ���Ʈ�� ������ �� �ִ�.
```
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{*catchall}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
```
- ���εǴ� ���´� ������ ����
  - / : controller = Home, action = Index
  - /Customer : controller = Customer, action = Index
  - /Customer/List : controller = Customer, action = List
  - /Customer/List/All : controller = Customer, action = List, id = All
  - /Customer/List/All/Delete : controller = Customer, action = List, id = All, catchall = Delete
  - /Customer/List/All/Delete/Perm : controller = Customer, action = List, id = All, catchall = Delete/Perm

# ���Ʈ�� �������� ����
- URL ���׸�Ʈ�� ������ ������� ��ġ�ǵ��� ����ǥ������ �̿��� ������ �� �ִ�.
```
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}/{*catchall}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new { controller = "^H.", action = "^Index$ | ^About$"}
            );
```
- HTTP �޼ҵ带 ����� ��������
```
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}/{*catchall}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new { controller = "^H.", action = "^Index$ | ^About$",
                        httpMethod = new HttpMethodConstraint("GET","POST") }
            );
```
- ���� �� �� ��������
  - AlphaRouteConstraint : ��ҹ��ڿ� ���� ���� ���ĺ� ���ڿ� ��ġ
  - BoolRouteConstraint : Bool�� �Ľ��� �� �ִ� ���� ��ġ
  - DateTimeRouteConstraint : DateTime���� �Ľ��� �� �ִ� ���� ��ġ
  - DecimalRouteConstraint : Decimal�� �Ľ��� �� �ִ� ���� ��ġ
  - DoubleRouteConstraint : Double�� �Ľ��� �� �ִ� ���� ��ġ
  - FloatRouteConstraint : Float�� �Ľ��� �� �ִ� ���� ��ġ
  - IntRouteConstraint : Int�� �Ľ��� �� �ִ� ���� ��ġ
  - LengthRouteConstraint(len Ȥ�� min, max) : ���� ���̿� �ش��� ��쿡�� ��ġ
  - LongRouteConstraint : Long���� �Ľ��� �� �ִ� ���� ��ġ
  - MaxRouteConstraint(val) : val���� ���� int ���� ��ġ
  - MaxLengthRouteConstraint(len) : len�� ���̺��� ���� ���ڿ��� ��ġ
  - MinRouteConstraint(val) : val���� ū int ���� ��ġ
  - MinLengthRouteConstraint(len) : len�� ���̺��� ū ���ڿ��� ��ġ
  - RangeRouteConstraint(min, max) : min�� max ���̿� ���� int ���� ��ġ
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
- CompoundRouteConstraint�� �̿��� ���� ���������� ���ÿ� ��� ����
  - id ���� �ּ� 6������ ���ĺ� ���ڷθ� ������ ���ڿ� ������� ��ġ�ȴ�.
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

# ��Ʈ����Ʈ ����� Ȱ��ȭ �� �����ϱ�
- MVC5 ���� �߰��� ����� ����̴�.
- �ش� ����� ����ϱ� ���� RouteConfig.cs ���Ͽ� �ش� ������ �߰��Ѵ�.
```
routes.MapMvcAttributeRoutes();
```
- ������ ������� ���� �۾����� ������ Controller�� �׼� �޼ҵ忡�� �̷������.
```
    public class CustomerController : Controller
    {
        // GET: Customer
        [Route("Test")]
        public ActionResult Index()
```
- ���׸�Ʈ ���� �̿� �� �������� ����
```
        [Route("Users/Add/{user}/{id:int}")]
        public ActionResult Create(string user, int id)
```
- �������� �����ϱ�
```
        [Route("Users/Add/{user}/{password:alpha:length(6)}")]
        public ActionResult ChangePass(string user, string password)
```
- ���Ʈ ���ξ� ���
```
    [RoutePrefix("Users")]
    public class CustomerController : Controller
    {
        [Route("~/Test")]
        public ActionResult Index()

        [Route("Add/{user}/{id:int}")]
        public ActionResult Create(string user, int id)
```

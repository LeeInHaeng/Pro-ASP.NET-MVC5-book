# 의존성 주입 사용 (Ninject 사용, 생성자 주입)
- NuGet에서 Ninject, Ninject.Web.Common.WebHost, Ninject.MVC5 설치
- 의존성 해결자(Dependency Resolver)를 작성한다.
  - Infrastructure 폴더를 생성한 후 의존성 해결자 클래스를 작성한다.
  - Ninject를 사용하기 위해 using Ninject 와 해당 프로젝트의 Models, 그리고 System.Web.Mvc 를 선언한다.
  - IDependencyResolver 인터페이스를 상속 받는다.
  - 새로운 객체들을 생성해주는 역할을 담당하는 객체인 Ninject 커널의 객체를 생성한다.
  - AddBindings 메소드를 통해 어떠한 인터페이스에 어떠한 구현 객체가 맵핑되는지를 선언한다.
```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using EssentialTools.Models;
using System.Web.Mvc;

namespace EssentialTools.Infrastructure
{
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
            kernel.Bind<IValueCalculator>().To<LinqValueCalculator>();
        }
    }
}
```
- AddBindings 메소드에서 자주 사용하는 것들
  - 매개변수를 지정하려면 To 뒤에 WithPropertyValue를 사용한다.
  - 생성자를 지정하려면 To 뒤에 WithConstructorArgument를 사용한다.
  - To 뒤에 범위 지정 메소드를 지정할 수도 있다.
```
kernel.Bind<IValueCalculator>().To<LinqValueCalculator>().WithPropertyValue("DiscountSize",50M);
kernel.Bind<IValueCalculator>().To<LinqValueCalculator>().WithConstructorArgument("discountParam",50M);

// 가장 디폴트로 의존성을 해결할 때마다 매번 새로운 객체를 생성
kernel.Bind<IValueCalculator>().To<LinqValueCalculator>().InTransientScope();

// 응용프로그램 전체에 공유되는 단일 인스턴스를 생성
kernel.Bind<IValueCalculator>().To<LinqValueCalculator>().InSingletonScope();

// 단일 인스턴스를 생성해서 단일 스레드에서 요청된 모든 객체들의 의존성을 해결한다.
kernel.Bind<IValueCalculator>().To<LinqValueCalculator>().InThreadScope();

// 단일 인스턴스를 생성해서 단일 HTTP 요청에서 요청된 모든 객체들의 의존성을 해결한다.
// 사용하기 위해서는 using Ninject.Web.Common; 가 별도로 필요
kernel.Bind<IValueCalculator>().To<LinqValueCalculator>().InRequestScope();
```
- 의존성 해결자를 등록한다.
  - Ninject를 설치하면 App_Start 폴더에 NinjectWebCommon.cs라는 파일이 생성된다.
  - 해당 파일에서 RegisterServices 메소드를 찾은 후 위에서 작성한 의존성 해결자를 등록한다.
  - using Ninject.Web.Common.WebHost; 추가
```
        private static void RegisterServices(IKernel kernel)
        {
            System.Web.Mvc.DependencyResolver.SetResolver(
                new EssentialTools.Infrastructure.NinjectDependencyResolver(kernel));
        }   
``` 
- 시나리오는 다음과 같다.
- ShoppingCart 클래스에서 LinqValueCalculator 클래스의 메소드를 사용해야 한다고 가정한다.
  - 두 클래스의 강결합을 없애기 위해 IValueCalculator라는 인터페이스를 둔다.
```
    public class ShoppingCart
    {
        private IValueCalculator calc;

        public ShoppingCart(IValueCalculator calcParam)
        {
            calc = calcParam;
        }

        public IEnumerable<Product> Products { get; set; }

        public decimal CalculateProductTotal()
        {
            return calc.ValueProducts(Products);
        }
    }
```
- 컨트롤러에서 사용한다.
  - 사용할 인터페이스를 선언한다.
  - 생성자를 통해 선언한 인터페이스를 등록한다.
```
    public class HomeController : Controller
    {
        private IValueCalculator calc;
        private Product[] products =
        {
            new Product { Name = "Kayak", Category = "Watersports", Price = 275M},
            new Product { Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
            new Product { Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
            new Product { Name = "Corner flag", Category = "Soccer", Price = 34.95M}
        };

        public HomeController(IValueCalculator calcParam)
        {
            calc = calcParam;
        }

        // GET: Home
        public ActionResult Index()
        {
            ShoppingCart cart = new ShoppingCart(calc) { Products = products };

            decimal totalValue = cart.CalculateProductTotal();

            return View(totalValue);
        }
    }
```

# Unit Test Project를 이용해 단위 테스트 수행

# Mock 객체와 Moq를 사용해 단위 테스트 수행하기

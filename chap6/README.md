# ������ ���� ��� (Ninject ���, ������ ����)
- NuGet���� Ninject, Ninject.Web.Common.WebHost, Ninject.MVC5 ��ġ
- ������ �ذ���(Dependency Resolver)�� �ۼ��Ѵ�.
  - Infrastructure ������ ������ �� ������ �ذ��� Ŭ������ �ۼ��Ѵ�.
  - Ninject�� ����ϱ� ���� using Ninject �� �ش� ������Ʈ�� Models, �׸��� System.Web.Mvc �� �����Ѵ�.
  - IDependencyResolver �������̽��� ��� �޴´�.
  - ���ο� ��ü���� �������ִ� ������ ����ϴ� ��ü�� Ninject Ŀ���� ��ü�� �����Ѵ�.
  - AddBindings �޼ҵ带 ���� ��� �������̽��� ��� ���� ��ü�� ���εǴ����� �����Ѵ�.
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
- AddBindings �޼ҵ忡�� ���� ����ϴ� �͵�
  - �Ű������� �����Ϸ��� To �ڿ� WithPropertyValue�� ����Ѵ�.
  - �����ڸ� �����Ϸ��� To �ڿ� WithConstructorArgument�� ����Ѵ�.
  - To �ڿ� ���� ���� �޼ҵ带 ������ ���� �ִ�.
```
kernel.Bind<IValueCalculator>().To<LinqValueCalculator>().WithPropertyValue("DiscountSize",50M);
kernel.Bind<IValueCalculator>().To<LinqValueCalculator>().WithConstructorArgument("discountParam",50M);

// ���� ����Ʈ�� �������� �ذ��� ������ �Ź� ���ο� ��ü�� ����
kernel.Bind<IValueCalculator>().To<LinqValueCalculator>().InTransientScope();

// �������α׷� ��ü�� �����Ǵ� ���� �ν��Ͻ��� ����
kernel.Bind<IValueCalculator>().To<LinqValueCalculator>().InSingletonScope();

// ���� �ν��Ͻ��� �����ؼ� ���� �����忡�� ��û�� ��� ��ü���� �������� �ذ��Ѵ�.
kernel.Bind<IValueCalculator>().To<LinqValueCalculator>().InThreadScope();

// ���� �ν��Ͻ��� �����ؼ� ���� HTTP ��û���� ��û�� ��� ��ü���� �������� �ذ��Ѵ�.
// ����ϱ� ���ؼ��� using Ninject.Web.Common; �� ������ �ʿ�
kernel.Bind<IValueCalculator>().To<LinqValueCalculator>().InRequestScope();
```
- ������ �ذ��ڸ� ����Ѵ�.
  - Ninject�� ��ġ�ϸ� App_Start ������ NinjectWebCommon.cs��� ������ �����ȴ�.
  - �ش� ���Ͽ��� RegisterServices �޼ҵ带 ã�� �� ������ �ۼ��� ������ �ذ��ڸ� ����Ѵ�.
  - using Ninject.Web.Common.WebHost; �߰�
```
        private static void RegisterServices(IKernel kernel)
        {
            System.Web.Mvc.DependencyResolver.SetResolver(
                new EssentialTools.Infrastructure.NinjectDependencyResolver(kernel));
        }   
``` 
- �ó������� ������ ����.
- ShoppingCart Ŭ�������� LinqValueCalculator Ŭ������ �޼ҵ带 ����ؾ� �Ѵٰ� �����Ѵ�.
  - �� Ŭ������ �������� ���ֱ� ���� IValueCalculator��� �������̽��� �д�.
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
- ��Ʈ�ѷ����� ����Ѵ�.
  - ����� �������̽��� �����Ѵ�.
  - �����ڸ� ���� ������ �������̽��� ����Ѵ�.
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

# Unit Test Project�� �̿��� ���� �׽�Ʈ ����

# Mock ��ü�� Moq�� ����� ���� �׽�Ʈ �����ϱ�

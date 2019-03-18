using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguageFeatures.Models;
using System.Text;

namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public string Index()
        {
            return "Navigate to a URL to show an example";
        }

        public ViewResult AutoProperty()
        {
            Product myProduct = new Product();

            myProduct.Name = "Kayak";

            string productName = myProduct.Name;

            return View("Result", (Object)String.Format("Product name : {0}", productName));
        }

        public ViewResult CreateProduct()
        {
            var myProduct = new
            {
                ProductID = 100,
                Name = "Kayak",
                Description = "A boat for one person",
                Price = 275M,
                Category = "Watersports"
            };

            return View("Result", (object)String.Format("Category : {0}", myProduct.Category));
        }

        public ViewResult FindProducts()
        {
            Product[] products =
            {
                new Product {Name="Kayak", Category="Watersports", Price=275M },
                new Product {Name="Lifejacket", Category="Watersports", Price=48.95M },
                new Product {Name="Soccer ball", Category="Soccer", Price=19.50M },
                new Product {Name="Corner flag", Category="Soccer", Price=34.95M }
            };

            var foundProducts = products.OrderByDescending(e => e.Price)
                                    .Take(3)
                                    .Select(e => new { e.Name, e.Price });

            var foundProducts2 = from match in products
                                 orderby match.Price descending
                                 select new { match.Name, match.Price };

            StringBuilder result = new StringBuilder();
            foreach (var fp in foundProducts)
                result.AppendFormat("Price : {0}", fp.Price);

            return View("Result", (object)result.ToString());
        }
    }
}
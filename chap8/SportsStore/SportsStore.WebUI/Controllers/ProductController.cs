using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 2;

        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }

        public ViewResult List(string Category, int? Page)
        {
            int PageNo = Page ?? 1;
            var viewData = repository.Products
                            .Where(p => Category == null || p.Category == Category)
                            .OrderBy(p => p.ProductID);

            ViewBag.Category = Category ?? null;

            return View(viewData.ToPagedList(PageNo, PageSize));
        }
    }
}
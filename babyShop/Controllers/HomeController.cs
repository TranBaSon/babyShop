using babyShop.Data;
using babyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace babyShop.Controllers
{
    public class HomeController : Controller
    {
        private MyDbContext db = new MyDbContext();
        public ActionResult Index()
        {
            var products = db.Products;
            return View(products.ToList());
        }

        public ActionResult showByCategory(string category)
        {
            var products = db.Products.Where(x => x.Category.Name.Equals(category)).ToList();
            return View("ShowProducts", products);
        }

        public ActionResult Search(string searchKey)
        {
            var products = db.Products.Where(x => x.Name.Contains(searchKey)).ToList();
            return View("ShowProducts", products);
        }
        
        public ActionResult ShowProducts(string searchKey)
        {
            var products = db.Products;
            return View("ShowProducts", products);
        }

       

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
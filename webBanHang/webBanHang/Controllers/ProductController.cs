using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBanHang.Models;

namespace webBanHang.Controllers
{
    public class ProductController : Controller
    {
        WebBanHangNongSan db = new WebBanHangNongSan();

        // GET: Product
        public ActionResult Index()
        {
            var products = db.Products.Select(p => p);

            return View(products.ToList());
        }

        public ActionResult Detail()
        {
            return View();
        }
    }
}
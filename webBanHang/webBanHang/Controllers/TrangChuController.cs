using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBanHang.Models;

namespace webBanHang.Controllers
{
    public class TrangChuController : Controller
    {
        private WebBanHangNongSan db = new WebBanHangNongSan();
        // GET: TrangChu
        public ActionResult TrangChu()
        {
            var product = db.Products.Select(p => p);
            return View(product.ToList());
        }
        public ActionResult Content()
        {
            return View();
        }
    }
}
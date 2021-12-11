using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBanHang.Models;


namespace webBanHang.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private WebBanHangNongSan db = new WebBanHangNongSan();
        // GET: Admin/Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string userName, string passWord)
        {
            string Name = userName;
            string Pass = passWord;
            var acc = db.Admins.SingleOrDefault(x => x.UserName == Name && x.PassWord == Pass);
            if(acc != null)
            {
                Session["Name"] = Name;
               return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }  
        }
    }
}
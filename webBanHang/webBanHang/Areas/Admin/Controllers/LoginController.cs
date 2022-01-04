using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBanHang.Models;
using webBanHang.Controllers;


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
            if(ModelState.IsValid)
            {
                string Name = userName;
                string Pass = DangNhapController.Instance.LayMaMD5(passWord);
                var acc = db.Admins.SingleOrDefault(x => x.UserName == Name && x.PassWord == Pass);
                if (acc != null)
                {
                    Session["Name"] = Name;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không đúng!!!");
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult LogOut()
        {
            Session["Name"] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBanHang.Models;

namespace webBanHang.Controllers
{
    public class DangNhapController : Controller
    {
        WebBanHangNongSan db = new WebBanHangNongSan();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var email = collection["email"];
            var matKhau = collection["matKhau"];
            if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi1"] = "Email không được để trống";
            }
            if (String.IsNullOrEmpty(matKhau))
            {
                ViewData["Loi2"] = "Mật khẩu không được để trống";
            }
            else
            {
                Customer cus = db.Customers.SingleOrDefault(n => n.Email == email && n.PassWord == matKhau);
                if (cus != null)
                {
                    
                    Session["Customer"] = cus;
                    ViewBag.ThongBao = "Bạn đã đăng nhập thành công";
                    return RedirectToAction("TrangChu", "TrangChu");
                }
                else
                {
                    ViewBag.ThongBao = "Sai email hoặc mật khẩu";
                }
            }
            return View();
            
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
    }
}
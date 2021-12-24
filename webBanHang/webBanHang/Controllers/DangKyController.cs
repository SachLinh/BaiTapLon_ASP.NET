using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBanHang.Models;

namespace webBanHang.Controllers
{
    public class DangKyController : Controller
    {
        WebBanHangNongSan db = new WebBanHangNongSan();

        // GET: DangKy
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, Customer cus)
        {
            var hoten = collection["hoten"];
            var email = collection["email"];
            var sdt = collection["sdt"];
            var matKhau = collection["matKhau"];
            var matKhau2 = collection["matKhau2"];
            var diaChi = collection["diaChi"];
            

            if(String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên không được để trống";
            }  
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi2"] = "Email không được để trống";
            }
            else if (String.IsNullOrEmpty(matKhau))
            {
                ViewData["Loi3"] = "Mật khẩu không được để trống";
            }
            else if (String.IsNullOrEmpty(matKhau2))
            {
                ViewData["Loi4"] = "Mật khẩu nhập lại không được để trống";
            }
            else if (String.IsNullOrEmpty(diaChi))
            {
                ViewData["Loi5"] = "Địa chỉ không được để trống";
            }
            else if (String.IsNullOrEmpty(sdt))
            {
                ViewData["Loi7"] = "Số điện thoại không được để trống";
            }
            else if (matKhau != matKhau2)
            {
                ViewData["Loi6"] = "Mật khẩu nhập lại không trùng khớp";
            }
            else
            {
                cus.Username = hoten;
                cus.Email = email;
                cus.Phone = sdt;
                cus.PassWord = matKhau;
                cus.Address = diaChi;
                cus.RoleID = null;
                db.Customers.Add(cus);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    
                }
                return RedirectToAction("DangNhap", "DangNhap");
            }
            return this.DangKy();
        }
    }
}
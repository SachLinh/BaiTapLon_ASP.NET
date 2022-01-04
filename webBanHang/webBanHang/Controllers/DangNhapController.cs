using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBanHang.Models;
using System.Security.Cryptography;
using System.Text;

namespace webBanHang.Controllers
{
    public class DangNhapController : Controller
    {
        #region Tạo Instance
        private static DangNhapController instance;

        public static DangNhapController Instance
        {
            get
            {
                if (instance == null)
                    instance = new DangNhapController();
                return instance;
            }
        }

        public DangNhapController()
        {
        }
        #endregion

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
            var matKhau = LayMaMD5(collection["matKhau"].ToString());
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

        public string LayMaMD5(string matKhau)
        {
            MD5 md5Hash = MD5.Create();

            byte[] data = md5Hash.ComputeHash(ASCIIEncoding.ASCII.GetBytes(matKhau));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
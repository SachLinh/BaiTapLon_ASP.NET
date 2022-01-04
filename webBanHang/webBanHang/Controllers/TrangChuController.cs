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
            //Sản phẩm bán chạy
            var sanPhamBanChay = from hd in db.HoaDons
                                 join cthd in db.ChiTietHoaDons on hd.MaHD equals cthd.MaHD
                                 join sp in db.Products on cthd.ProID equals sp.ProID
                                 join c in db.Catalogies on sp.ID equals c.ID
                                 group sp by new
                                 {
                                     sp.ProID,
                                     sp.ProName,
                                     sp.ProImage,
                                     sp.Price,
                                     c.CataName,
                                     sp.Quantity,
                                     sp.ProDescription,
                                     sp.DateOfImport
                                 }
                           into kq
                                 orderby kq.Count() descending
                                 select new SPNoiBat()
                                 {
                                     ProID = kq.Key.ProID,
                                     ProName = kq.Key.ProName,
                                     ProImage = kq.Key.ProImage,
                                     Quantity = kq.Key.Quantity,
                                     Price = kq.Key.Price,
                                     CataName = kq.Key.CataName,
                                     DateOfImport = kq.Key.DateOfImport,
                                     soluongMua = kq.Count()
                                 };
            ViewBag.SanPhamBanChay = sanPhamBanChay.Take(3).ToList();


            //Danh sách sản phẩm
            var product = db.Products.Select(p => p);
            return View(product.ToList());
        }
        public ActionResult Content()
        {
            return View();
        }

        //var sanPhamBanChay = from hd in db.HoaDons
        //                     join cthd in db.ChiTietHoaDons on hd.MaHD equals cthd.MaHD
        //                     join sp in db.Products on cthd.ProID equals sp.ProID
        //                     group sp by new
        //                     {
        //                         sp.ProID,
        //                         sp.ProName,
        //                         sp.ProImage,
        //                         sp.Price,
        //                         sp.ID,
        //                         sp.Quantity,
        //                         sp.ProDescription,
        //                         sp.DateOfImport
        //                     }
        //                   into kq
        //                     orderby kq.Count() descending
        //                     select new SPNoiBat()
        //                     {
        //                         ProID = kq.Key.ProID,
        //                         ProName = kq.Key.ProName,
        //                         ProImage = kq.Key.ProImage,
        //                         Quantity = kq.Key.Quantity,
        //                         Price = kq.Key.Price,
        //                         ID = kq.Key.ID,
        //                         DateOfImport = kq.Key.DateOfImport,
        //                         soluongMua = kq.Count()
        //                     };
        //var s = sanPhamBanChay.Take(2);
    }
}
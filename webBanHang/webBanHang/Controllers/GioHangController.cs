using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBanHang.Models;

namespace webBanHang.Controllers
{
    public class GioHangController : Controller
    {
        WebBanHangNongSan db = new WebBanHangNongSan();
        public List<gioHang> LayGioHang()
        {
            List<gioHang> listGioHang = Session["GioHang"] as List<gioHang>;
            if (listGioHang == null)
            {
                listGioHang = new List<gioHang>();
                Session["GioHang"] = listGioHang;
            }
            return listGioHang;
        }
        public ActionResult ThemGioHang(string sMaSP, string strURL)
        {
            List<gioHang> listGioHang = LayGioHang();
            gioHang sanPham = listGioHang.Find(n => n.sMaSP == sMaSP);
            if (sanPham == null)
            {
                sanPham = new gioHang(sMaSP);
                listGioHang.Add(sanPham);
                return Redirect(strURL);
            }
            else
            {
                sanPham.iSoLuong++;
                return Redirect(strURL);
            }
        }
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<gioHang> listGioHang = Session["GioHang"] as List<gioHang>;
            if (listGioHang != null)
            {
                iTongSoLuong = listGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double iTongTien = 0;
            List<gioHang> listGioHang = Session["GioHang"] as List<gioHang>;
            if (listGioHang != null)
            {
                iTongTien = listGioHang.Sum(n => n.dThanhTien);
            }
            return iTongTien;
        }

        // GET: GioHang
        public ActionResult GioHang()
        {
            List<gioHang> listGioHang = LayGioHang();
            if (listGioHang.Count == 0)
            {
                return RedirectToAction("TrangChu", "TrangChu");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(listGioHang);
        }
        public ActionResult XoaGioHang(string sMaSP)
        {
            List<gioHang> listGioHang = LayGioHang();
            gioHang sanPham = listGioHang.SingleOrDefault(n => n.sMaSP == sMaSP);
            if (sanPham != null)
            {
                listGioHang.RemoveAll(n => n.sMaSP == sMaSP);
                return RedirectToAction("GioHang");
            }
            if (listGioHang.Count == 0)
            {
                return RedirectToAction("TrangChu", "TrangChu");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapNhat(string sMaSP, FormCollection f)
        {
            List<gioHang> listGioHang = LayGioHang();
            gioHang sanPham = listGioHang.SingleOrDefault(n => n.sMaSP == sMaSP);
            if (sanPham != null)
            {
                sanPham.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaTatCaGioHang()
        {
            List<gioHang> listGioHang = LayGioHang();
            listGioHang.Clear();
            return RedirectToAction("GioHang");
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["Customer"] == null || Session["Customer"].ToString() == null)
            {
                return RedirectToAction("DangNhap", "DangNhap");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("TrangChu", "TrangChu");
            }
            List<gioHang> listGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(listGioHang);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            // them don hang
            HoaDon hd = new HoaDon();
            Customer cus = (Customer)Session["Customer"];
            List<gioHang> gh = LayGioHang();
            hd.Phone = cus.Phone;
            hd.ngayDayHang = DateTime.Now;
            if (collection["MaKM"] != null)
                hd.MaKhuyenMai = collection["MaKM"];
            else
                hd.MaKhuyenMai = "";
            hd.NoiNhanHang = collection["NoiNhanHang"];
            hd.ThanhToan = false;
            hd.GiaoHang = false;
            db.HoaDons.Add(hd);
            db.SaveChanges();
            // chi tiet
            foreach (var item in gh)
            {
                ChiTietHoaDon cthd = new ChiTietHoaDon();
                cthd.MaHD = hd.MaHD;
                cthd.ProID = item.sMaSP;
                cthd.SoLuongMua = item.iSoLuong;
                cthd.Price = item.fDonGia;
                db.ChiTietHoaDons.Add(cthd);
            }
            db.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XacNhanDonHang", "GioHang");

        }
        public ActionResult XacNhanDonHang()
        {
            return View();
        }
    }
}
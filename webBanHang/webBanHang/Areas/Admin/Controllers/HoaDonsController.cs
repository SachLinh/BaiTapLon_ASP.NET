using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using webBanHang.Models;
using PagedList;

namespace webBanHang.Areas.Admin.Controllers
{
    public class HoaDonsController : Controller
    {
        private WebBanHangNongSan db = new WebBanHangNongSan();

        // GET: Admin/HoaDons
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;


            ViewBag.SapTheoID = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.SapTheoTen = sortOrder == "ten" ? "ten_desc" : "ten";
            ViewBag.SapTheoNgay = sortOrder == "ngay" ? "ngay_desc" : "ngay";

            if(searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var hoaDons = db.HoaDons.Select(h => h);

            if(!string.IsNullOrEmpty(searchString))
            {
                hoaDons = hoaDons.Where(h => h.Customer.Username.Contains(searchString));
            }

            switch(sortOrder)
            {
                case "ten_desc":
                    hoaDons = hoaDons.OrderByDescending(h => h.Customer.Username);
                    break;
                case "ngay_desc":
                    hoaDons = hoaDons.OrderByDescending(h => h.ngayDayHang);
                    break;
                case "ten":
                    hoaDons = hoaDons.OrderBy(h => h.Customer.Username);
                    break;
                case "ngay":
                    hoaDons = hoaDons.OrderBy(h => h.ngayDayHang);
                    break;
                case "id_desc":
                    hoaDons = hoaDons.OrderByDescending(h => h.MaHD);
                    break;
                default:               
                    hoaDons = hoaDons.OrderBy(h => h.MaHD);
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(hoaDons.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/HoaDons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // GET: Admin/HoaDons/Create
        public ActionResult Create()
        {
            ViewBag.Phone = new SelectList(db.Customers, "Phone", "Username");
            ViewBag.MaKhuyenMai = new SelectList(db.KhuyenMais, "MaKhuyenMai", "MaKhuyenMai");
            return View();
        }

        // POST: Admin/HoaDons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHD,Phone,ngayDayHang,MaKhuyenMai,NoiNhanHang,ThanhToan,GiaoHang")] HoaDon hoaDon)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.HoaDons.Add(hoaDon);
                    db.SaveChanges();
                    
                }return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Loi nhap du lieu !!! " + ex.Message;
                ViewBag.Phone = new SelectList(db.Customers, "Phone", "Username", hoaDon.Phone);
                ViewBag.MaKhuyenMai = new SelectList(db.KhuyenMais, "MaKhuyenMai", "MaKhuyenMai", hoaDon.MaKhuyenMai);
                return View(hoaDon);
            }
        }

        // GET: Admin/HoaDons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            ViewBag.Phone = new SelectList(db.Customers, "Phone", "Username", hoaDon.Phone);
            ViewBag.MaKhuyenMai = new SelectList(db.KhuyenMais, "MaKhuyenMai", "MaKhuyenMai", hoaDon.MaKhuyenMai);
            return View(hoaDon);
        }

        // POST: Admin/HoaDons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHD,Phone,ngayDayHang,MaKhuyenMai,NoiNhanHang,ThanhToan,GiaoHang")] HoaDon hoaDon)
        {
            
            
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(hoaDon).State = EntityState.Modified;
                    db.SaveChanges();
                   
                } return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Loi nhap du lieu !!! " + ex.Message;
                ViewBag.Phone = new SelectList(db.Customers, "Phone", "Username", hoaDon.Phone);
                ViewBag.MaKhuyenMai = new SelectList(db.KhuyenMais, "MaKhuyenMai", "MaKhuyenMai", hoaDon.MaKhuyenMai);
                return View(hoaDon);
            }
        }

        // GET: Admin/HoaDons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // POST: Admin/HoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HoaDon hoaDon = db.HoaDons.Find(id);
            try
            {
                db.HoaDons.Remove(hoaDon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Khong xoa duoc ban ghi nay !!! " + ex.Message;
                return View("Delete", hoaDon);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

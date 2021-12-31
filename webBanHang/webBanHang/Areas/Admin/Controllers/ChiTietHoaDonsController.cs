using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using webBanHang.Models;

namespace webBanHang.Areas.Admin.Controllers
{
    public class ChiTietHoaDonsController : Controller
    {
        private WebBanHangNongSan db = new WebBanHangNongSan();

        // GET: Admin/ChiTietHoaDons
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.SapXepHD = string.IsNullOrEmpty(sortOrder) ? "HD_desc" : "";
            ViewBag.SapXepPro = sortOrder == "Pro" ? "Pro_desc" : "Pro";
            var chiTietHoaDons = db.ChiTietHoaDons.Select(c => c);

            if(!string.IsNullOrEmpty(searchString))
            {
                int ss = int.Parse(searchString);
                chiTietHoaDons = chiTietHoaDons.Where(c => c.MaHD == ss);
            }

            switch(sortOrder)
            {
                case "HD_desc":
                    chiTietHoaDons = chiTietHoaDons.OrderByDescending(c => c.HoaDon.MaHD);
                    break;

                case "Pro_desc":
                    chiTietHoaDons = chiTietHoaDons.OrderByDescending(c => c.ProID);
                    break;

                case "Pro":
                    chiTietHoaDons = chiTietHoaDons.OrderBy(c => c.HoaDon.MaHD);
                    break;

                default:
                    chiTietHoaDons = chiTietHoaDons.OrderBy(c => c.HoaDon.MaHD);
                    break;
            }
            return View(chiTietHoaDons.ToList());
        }

        // GET: Admin/ChiTietHoaDons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDon chiTietHoaDon = db.ChiTietHoaDons.Find(id);
            if (chiTietHoaDon == null)
            {
                return HttpNotFound();
            }
            return View(chiTietHoaDon);
        }

        // GET: Admin/ChiTietHoaDons/Create
        public ActionResult Create()
        {
            ViewBag.MaHD = new SelectList(db.HoaDons, "MaHD", "Phone");
            ViewBag.ProID = new SelectList(db.Products, "ProID", "ID");
            return View();
        }

        // POST: Admin/ChiTietHoaDons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHD,ProID,SoLuongMua,Price")] ChiTietHoaDon chiTietHoaDon)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietHoaDons.Add(chiTietHoaDon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHD = new SelectList(db.HoaDons, "MaHD", "Phone", chiTietHoaDon.MaHD);
            ViewBag.ProID = new SelectList(db.Products, "ProID", "ID", chiTietHoaDon.ProID);
            return View(chiTietHoaDon);
        }

        // GET: Admin/ChiTietHoaDons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDon chiTietHoaDon = db.ChiTietHoaDons.Find(id);
            if (chiTietHoaDon == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHD = new SelectList(db.HoaDons, "MaHD", "Phone", chiTietHoaDon.MaHD);
            ViewBag.ProID = new SelectList(db.Products, "ProID", "ID", chiTietHoaDon.ProID);
            return View(chiTietHoaDon);
        }

        // POST: Admin/ChiTietHoaDons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHD,ProID,SoLuongMua,Price")] ChiTietHoaDon chiTietHoaDon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietHoaDon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHD = new SelectList(db.HoaDons, "MaHD", "Phone", chiTietHoaDon.MaHD);
            ViewBag.ProID = new SelectList(db.Products, "ProID", "ID", chiTietHoaDon.ProID);
            return View(chiTietHoaDon);
        }

        // GET: Admin/ChiTietHoaDons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietHoaDon chiTietHoaDon = db.ChiTietHoaDons.Find(id);
            if (chiTietHoaDon == null)
            {
                return HttpNotFound();
            }
            return View(chiTietHoaDon);
        }

        // POST: Admin/ChiTietHoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietHoaDon chiTietHoaDon = db.ChiTietHoaDons.Find(id);
            db.ChiTietHoaDons.Remove(chiTietHoaDon);
            db.SaveChanges();
            return RedirectToAction("Index");
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

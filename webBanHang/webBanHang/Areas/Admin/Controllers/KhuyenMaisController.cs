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
    public class KhuyenMaisController : Controller
    {
        private WebBanHangNongSan db = new WebBanHangNongSan();

        // GET: Admin/KhuyenMais
        public ActionResult Index(string searchString, string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SapTheoMa = string.IsNullOrEmpty(sortOrder) ? "ma_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var khuyenMai = db.KhuyenMais.Select(k => k);
            if (!string.IsNullOrEmpty(searchString))
            {
                khuyenMai = khuyenMai.Where(k => k.MaKhuyenMai.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "ma_desc":
                    khuyenMai = khuyenMai.OrderByDescending(k => k.MaKhuyenMai);
                    break;
                default:
                    khuyenMai = khuyenMai.OrderBy(k => k.MaKhuyenMai);
                    break;
            }
            int pageSize = 3;
            int pageNumer = (page ?? 1);
            return View(khuyenMai.ToPagedList(pageNumer, pageSize));
        }

        // GET: Admin/KhuyenMais/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhuyenMai khuyenMai = db.KhuyenMais.Find(id);
            if (khuyenMai == null)
            {
                return HttpNotFound();
            }
            return View(khuyenMai);
        }

        // GET: Admin/KhuyenMais/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/KhuyenMais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKhuyenMai,GiaTri")] KhuyenMai khuyenMai)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.KhuyenMais.Add(khuyenMai);
                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Loi nhap du lieu !!! " + ex.Message;
                return View(khuyenMai);
            }

            
        }

        // GET: Admin/KhuyenMais/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhuyenMai khuyenMai = db.KhuyenMais.Find(id);
            if (khuyenMai == null)
            {
                return HttpNotFound();
            }
            return View(khuyenMai);
        }

        // POST: Admin/KhuyenMais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKhuyenMai,GiaTri")] KhuyenMai khuyenMai)
        {
           
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(khuyenMai).State = EntityState.Modified;
                    db.SaveChanges();
                   
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Loi nhap du lieu !!! " + ex.Message;
                return View(khuyenMai);
            }
        }

        // GET: Admin/KhuyenMais/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhuyenMai khuyenMai = db.KhuyenMais.Find(id);
            if (khuyenMai == null)
            {
                return HttpNotFound();
            }
            return View(khuyenMai);
        }

        // POST: Admin/KhuyenMais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            KhuyenMai khuyenMai = db.KhuyenMais.Find(id);

           
            try
            {
                db.KhuyenMais.Remove(khuyenMai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Loi ko xoa duoc du lieu!!!" + ex.Message;
                return View("Delete", khuyenMai);
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

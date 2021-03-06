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
    public class CatalogiesController : Controller
    {
        private WebBanHangNongSan db = new WebBanHangNongSan();

        // GET: Admin/Catalogies
        public ActionResult Index(string searchString, string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SapTheoTen = string.IsNullOrEmpty(sortOrder) ? "ten_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var cata = db.Catalogies.Select(c => c);
            if (!string.IsNullOrEmpty(searchString))
            {
                cata = cata.Where(c => c.CataName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "ten_desc":
                    cata = cata.OrderByDescending(c => c.CataName);
                    break;
                default:
                    cata = cata.OrderBy(c => c.CataName);
                    break;
            }
            int pageSize = 3;
            int pageNumer = (page ?? 1);
            return View(cata.ToPagedList(pageNumer, pageSize));
        }

        // GET: Admin/Catalogies/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalogy catalogy = db.Catalogies.Find(id);
            if (catalogy == null)
            {
                return HttpNotFound();
            }
            return View(catalogy);
        }

        // GET: Admin/Catalogies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Catalogies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CataName")] Catalogy catalogy)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Catalogies.Add(catalogy);
                    db.SaveChanges();
                    
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Lỗi nhập sai dữ liệu!! " + ex.Message;
                return View(catalogy);
            }
            
            
        }

        // GET: Admin/Catalogies/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalogy catalogy = db.Catalogies.Find(id);
            if (catalogy == null)
            {
                return HttpNotFound();
            }
            return View(catalogy);
        }

        // POST: Admin/Catalogies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CataName")] Catalogy catalogy)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(catalogy).State = EntityState.Modified;
                    db.SaveChanges();
                   
                } return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu!!!" + ex.Message;
                return View(catalogy);
            }
        }

        // GET: Admin/Catalogies/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalogy catalogy = db.Catalogies.Find(id);
            if (catalogy == null)
            {
                return HttpNotFound();
            }
            return View(catalogy);
        }

        // POST: Admin/Catalogies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Catalogy catalogy = db.Catalogies.Find(id);
            try
            {
                db.Catalogies.Remove(catalogy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Không xóa được dữ liệu " + ex.Message;
                return View("Delete", catalogy);
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

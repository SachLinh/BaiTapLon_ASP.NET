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
    public class ProductsController : Controller
    {
        private WebBanHangNongSan db = new WebBanHangNongSan();

        // GET: Admin/Products
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SapTheoTen = string.IsNullOrEmpty(sortOrder) ? "ten_desc" : "";
            ViewBag.SapTheoID = sortOrder == "ID" ? "ID_desc" : "ID";
            ViewBag.SapTheoGia = sortOrder == "gia" ? "gia_desc" : "gia";
            ViewBag.SapTheoLoaiSP = sortOrder == "cata" ? "cata_desc" : "cata";
            ViewBag.SapTheoSoLuong = sortOrder == "sl" ? "sl_desc" : "sl";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var products = db.Products.Select(p => p);
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.ProName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "ten_desc":
                    products = products.OrderByDescending(p => p.ProName);
                    break;
                case "gia_desc":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                case "cata_desc":
                    products = products.OrderByDescending(p => p.Catalogy.CataName);
                    break;
                case "ID_desc":
                    products = products.OrderByDescending(p => p.ProID);
                    break;
                case "sl_desc":
                    products = products.OrderByDescending(p => p.Quantity);
                    break;
                case "gia":
                    products = products.OrderBy(p => p.Price);
                    break;
                case "ID":
                    products = products.OrderBy(p => p.ProID);
                    break;
                case "cata":
                    products = products.OrderBy(p => p.Catalogy.CataName);
                    break;
                case "sl":
                    products = products.OrderBy(p => p.Quantity);
                    break;
                default:
                    products = products.OrderBy(p => p.ProName);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.Catalogies, "ID", "CataName");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProID,ID,ProName,Quantity,Price,ProImage,DateOfImport,ProDescription")] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    product.ProImage = "";
                    var f = Request.Files["ImageFile"];
                    if (f != null & f.ContentLength > 0)
                    {
                        string FileName = System.IO.Path.GetFileName(f.FileName);
                        string UpLoadFile = Server.MapPath("~/wwwroot/images/" + FileName);
                        f.SaveAs(UpLoadFile);
                        product.ProImage = FileName;
                    }
                    db.Products.Add(product);
                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập dữ liệu!!!" + ex.Message;
                ViewBag.ID = new SelectList(db.Catalogies, "ID", "CataName", product.ID);
                return View(product);
            }

        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.Catalogies, "ID", "CataName", product.ID);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProID,ID,ProName,Quantity,Price,ProImage,DateOfImport,ProDescription")] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    product.ProImage = "";
                    var f = Request.Files["ImageFile"];

                    if (f != null & f.ContentLength > 0)
                    {
                        string FileName = System.IO.Path.GetFileName(f.FileName);
                        string UpLoadFile = Server.MapPath("~/wwwroot/images/" + FileName);
                        f.SaveAs(UpLoadFile);
                        product.ProImage = FileName;
                    }

                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi sửa dữ liệu!!! " + ex.Message;
                ViewBag.ID = new SelectList(db.Catalogies, "ID", "CataName", product.ID);
                return View(product);
            }

        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Product product = db.Products.Find(id);
            try
            {
                db.Products.Remove(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Không xóa được dữ liệu!!! " + ex.Message;
                return View("Delete", product);
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

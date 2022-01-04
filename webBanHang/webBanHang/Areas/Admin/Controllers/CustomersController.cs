using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using webBanHang.Models;
using webBanHang.Controllers;

using PagedList;

namespace webBanHang.Areas.Admin.Controllers
{
    public class CustomersController : Controller
    {
        private WebBanHangNongSan db = new WebBanHangNongSan();

        // GET: Admin/Customers
        public ActionResult Index(string searchString, string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SapTheoTen = string.IsNullOrEmpty(sortOrder) ? "ten_desc" : "";
            if(searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var customers = db.Customers.Select(c => c);
            if(!string.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(c => c.Username.Contains(searchString));
            }
            switch(sortOrder)
            {
                case "ten_desc":
                    customers = customers.OrderByDescending(c => c.Username);
                    break;
                default:
                    customers = customers.OrderBy(c => c.Username);
                    break;
            }
            int pageSize = 3;
            int pageNumer = (page ?? 1);
            return View(customers.ToPagedList(pageNumer, pageSize));
        }

        // GET: Admin/Customers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Admin/Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Phone,Username,PassWord,Email,Address,RoleID")] Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var pass = DangNhapController.Instance.LayMaMD5(Request["PassWord"]);
                    customer.PassWord = pass;
                    db.Customers.Add(customer);
                    db.SaveChanges();
                   
                }
                return RedirectToAction("Index");
            }
             catch(Exception ex)
            {
                ViewBag.Error = "Lỗi nhập sai dữ liệu!! " + ex.Message;
                return View(customer);
            } 

            
        }

        // GET: Admin/Customers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Admin/Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Phone,Username,PassWord,Email,Address,RoleID")] Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(customer).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi nhập sai dữ liệu!! " + ex.Message;
                return View(customer);
            }
        }

        // GET: Admin/Customers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Admin/Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Customer customer = db.Customers.Find(id);
            try
            {
                db.Customers.Remove(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = "Khong xoa duoc dữ liệu!! " + ex.Message;
                return View("Delete",customer);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webBanHang.Models;
using PagedList;

namespace webBanHang.Controllers
{
    public class SanPhamController : Controller
    {
        WebBanHangNongSan db = new WebBanHangNongSan();

        // GET: SanPham
        public ActionResult Index(int? page, string cataId)
        {
            ViewBag.CurrentCategory = cataId;

            //Danh muc san pham
            var category = db.Catalogies.Select(c => c);
            ViewBag.Category = category.ToList();

            //Danh sach san pham
            var products = db.Products.Select(p => p);
            if(cataId != null)
            {
                products = db.Products.Where(p => p.Catalogy.ID == cataId);
            }
            ViewBag.ProductCount = products.Count();

            //Danh sach san pham moi
            var newProducts = db.Products.OrderBy(p => p.DateOfImport).Take(3).ToList();
            ViewBag.NewProducts = newProducts.ToList();

            //Phan trang
            products = products.OrderBy(p => p.ID);
            int pageSize = 12;
            int pageNumber = (page ?? 1);

            return View(products.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Detail(string id)
        {
            var product = db.Products.Find(id);

            //Danh muc san pham
            var category = db.Catalogies.Select(c => c);
            ViewBag.Category = category.ToList();

            //San pham lien quan
            ViewBag.RelatedProducts = db.Products.Where(p => p.Catalogy.ID == product.Catalogy.ID && p.ProID != id).Take(4).ToList();

            return View(product);
        }

    }
}
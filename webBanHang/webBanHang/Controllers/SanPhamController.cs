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
        public ActionResult Index(int? page, string cataId, string minPrice, string maxPrice, string currentMinPrice, string currentMaxPrice, string searchString, string currentFilter)
        {
            //Viewbag lưu danh mục hiện tại
            ViewBag.CurrentCategory = cataId;

            //Danh muc san pham
            var category = db.Catalogies.Select(c => c);
            ViewBag.Category = category.ToList();

            //Tìm theo tên
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            //Trả về page 1 nếu có lọc theo giá
            if (minPrice != null && maxPrice != null)
            {
                page = 1;
            }
            else
            {
                minPrice = currentMinPrice;
                maxPrice = currentMaxPrice;
            }
            //Tạo viewbag để lưu gia trị của khoảng giá
            ViewBag.CurrentMinPrice = minPrice;
            ViewBag.CurrentMaxPrice = maxPrice;

            //Danh sach san pham
            var products = db.Products.Select(p => p);
            if(cataId != null)
            {
                products = db.Products.Where(p => p.Catalogy.ID == cataId);
            }

            //Tìm sản phẩm theo tên
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.ProName .Contains(searchString));
            }

            //Tìm sản phẩm theo khoảng giá
            if (!String.IsNullOrEmpty(minPrice) && !String.IsNullOrEmpty(maxPrice))
            {
                decimal _minPrice = Convert.ToDecimal(minPrice);
                decimal _maxPrice = Convert.ToDecimal(maxPrice);
                products = products.Where(p => p.Price >= _minPrice && p.Price <= _maxPrice);
            }

            //Danh sach san pham moi
            var newProducts = db.Products.OrderByDescending(p => p.DateOfImport).Take(3).ToList();
            ViewBag.NewProducts = newProducts.ToList();

            //Phan trang
            products = products.OrderBy(p => p.ProID);
            int pageSize = 8;
            int pageNumber = (page ?? 1);

            ViewBag.ProductCount = products.Count();

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
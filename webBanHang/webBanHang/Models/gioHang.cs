using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webBanHang.Models
{
    public class gioHang
    {
        WebBanHangNongSan db = new WebBanHangNongSan();
        public string sMaSP { get; set; }
        public string sTenSP { get; set; }
        public string sHinhAnh { get; set; }
        public int fDonGia { get; set; }
        public int iSoLuong { get; set; }
        public Double dThanhTien
        {
            get { return fDonGia * iSoLuong; }
        }
        public gioHang(string ProID)
        {
            sMaSP = ProID;
            Product product = db.Products.Single(n => n.ProID == sMaSP);
            sTenSP = product.ProName;
            sHinhAnh = product.ProImage;
            fDonGia = int.Parse(product.Price.ToString());
            iSoLuong = 1;
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace webBanHang.Models
{
    public partial class WebBanHangNongSan : DbContext
    {
        public WebBanHangNongSan()
            : base("name=WebBanHangNongSan")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}

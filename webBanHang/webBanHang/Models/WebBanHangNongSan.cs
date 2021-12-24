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
        public virtual DbSet<Catalogy> Catalogies { get; set; }
        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<KhuyenMai> KhuyenMais { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Catalogy>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Catalogy)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Customer>()
                .Property(e => e.RoleID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.HoaDons)
                .WithOptional(e => e.Customer)
                .WillCascadeOnDelete();

            modelBuilder.Entity<KhuyenMai>()
                .HasMany(e => e.HoaDons)
                .WithOptional(e => e.KhuyenMai)
                .WillCascadeOnDelete();
        }
    }
}

namespace webBanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }

        [Key]
        [StringLength(10)]
        public string ProID { get; set; }

        [StringLength(10)]
        public string ID { get; set; }

        [StringLength(30)]
        public string ProName { get; set; }

        public int? Quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:0,##0}")]
        public int? Price { get; set; }

        [StringLength(100)]
        public string ProImage { get; set; }

        [StringLength(30)]
        public string DateOfImport { get; set; }

        [StringLength(100)]
        public string ProDescription { get; set; }

        public virtual Catalogy Catalogy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}

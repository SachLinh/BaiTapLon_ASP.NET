namespace webBanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietHoaDon")]
    public partial class ChiTietHoaDon
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaHD { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string ProID { get; set; }

        public int? SoLuongMua { get; set; }

        [DisplayFormat(DataFormatString = "{0:0,##0}")]
        public int? Price { get; set; }

        [DisplayFormat(DataFormatString = "{0:0,##0}")]
        public int? Total { get { return Price * SoLuongMua; } }

        public virtual Product Product { get; set; }

        public virtual HoaDon HoaDon { get; set; }
    }
}

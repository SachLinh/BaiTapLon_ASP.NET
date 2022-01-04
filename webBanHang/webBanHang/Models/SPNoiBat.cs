using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webBanHang.Models
{
    public class SPNoiBat
    {
        public string ProID { get; set; }
        public string ProName { get; set; }
        [DisplayFormat(DataFormatString = "{0:0,##0}")]
        public int? Price { get; set; }
        public string ProImage { get; set; }
        public string CataName { get; set; }
        public int? soluongMua { get; set; }
        public int? Quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateOfImport { get; set; }

    }
}
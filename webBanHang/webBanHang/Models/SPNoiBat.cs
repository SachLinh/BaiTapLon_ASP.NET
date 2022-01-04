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
        public string ID { get; set; }
        public int? soluongMua { get; set; }
        public int? Quantity { get; set; }

        public DateTime? DateOfImport { get; set; }
    }
}
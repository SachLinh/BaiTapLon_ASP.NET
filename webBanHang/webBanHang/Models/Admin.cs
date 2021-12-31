namespace webBanHang.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admin")]
    public partial class Admin
    {
        [Key]
        [Required(ErrorMessage = "Username không được để trống")]
        [StringLength(10)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password không được để trống")]
        [StringLength(20)]
        public string PassWord { get; set; }
    }
}

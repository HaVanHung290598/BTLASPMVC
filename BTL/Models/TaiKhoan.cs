namespace BTL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int maTaiKhoan { get; set; }

        public int maLoaiTK { get; set; }

        [Required]
        [StringLength(20)]
        public string tenDangNhap { get; set; }

        [Required]
        [StringLength(20)]
        public string password { get; set; }

        [Required]
        [StringLength(20)]
        public string loaiTaiKhoan { get; set; }

        public virtual loaiTaiKhoan loaiTaiKhoan1 { get; set; }
    }
}

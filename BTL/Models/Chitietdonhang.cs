namespace BTL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Chitietdonhang")]
    public partial class Chitietdonhang
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int maDonHang { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int maSanPham { get; set; }

        public int soLuong { get; set; }

        [Column(TypeName = "money")]
        public decimal tongTien { get; set; }

        [Required]
        [StringLength(30)]
        public string kichThuoc { get; set; }

        public virtual Donhang Donhang { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}

namespace BTL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int maSanPham { get; set; }

        [Required]
        [StringLength(100)]
        public string tenSanPham { get; set; }

        [Column(TypeName = "money")]
        public decimal gia { get; set; }

        [Column(TypeName = "ntext")]
        public string moTa { get; set; }

        [Required]
        [StringLength(100)]
        public string anh { get; set; }

        [Required]
        [StringLength(100)]
        public string chatLieu { get; set; }

        [Required]
        [StringLength(100)]
        public string kieuDang { get; set; }

        [Required]
        [StringLength(100)]
        public string thietKe { get; set; }

        [Required]
        [StringLength(100)]
        public string thuongHieu { get; set; }

        [Required]
        [StringLength(100)]
        public string mauSac { get; set; }

        [Required]
        [StringLength(30)]
        public string kichThuoc { get; set; }

        public int maDanhMuc { get; set; }
    }
}

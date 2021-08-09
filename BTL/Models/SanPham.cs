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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            Chitietdonhangs = new HashSet<Chitietdonhang>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int maSanPham { get; set; }

        [Required]
        [StringLength(100)]
        public string tenSanPham { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,###}")]
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

        public int soLuongCo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Chitietdonhang> Chitietdonhangs { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }
    }
}

namespace BTL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Donhang")]
    public partial class Donhang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Donhang()
        {
            Chitietdonhangs = new HashSet<Chitietdonhang>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int maDonHang { get; set; }

        [Required]
        [StringLength(100)]
        public string hoTen { get; set; }

        [Required]
        [StringLength(100)]
        public string diaChi { get; set; }

        [Required]
        [StringLength(20)]
        public string soDienThoai { get; set; }

        [Required]
        [StringLength(30)]
        public string email { get; set; }

        [Column(TypeName = "ntext")]
        public string chuThich { get; set; }

        [Required]
        [StringLength(100)]
        public string phuongThucVanChuyen { get; set; }

        [Column(TypeName = "money")]
        public decimal tongTien { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int maTaiKhoan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Chitietdonhang> Chitietdonhangs { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}

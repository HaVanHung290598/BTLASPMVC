namespace BTL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DanhMuc")]
    public partial class DanhMuc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int maDanhMuc { get; set; }

        [Required]
        [StringLength(100)]
        public string tenDanhMuc { get; set; }
    }
}

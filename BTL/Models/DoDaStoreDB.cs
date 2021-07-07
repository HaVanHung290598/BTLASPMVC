using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BTL.Models
{
    public partial class DoDaStoreDB : DbContext
    {
        public DoDaStoreDB()
            : base("name=DoDaStoreDB")
        {
        }

        public virtual DbSet<Chitietdonhang> Chitietdonhangs { get; set; }
        public virtual DbSet<DanhMuc> DanhMucs { get; set; }
        public virtual DbSet<Donhang> Donhangs { get; set; }
        public virtual DbSet<loaiTaiKhoan> loaiTaiKhoans { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chitietdonhang>()
                .Property(e => e.tongTien)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Chitietdonhang>()
                .Property(e => e.kichThuoc)
                .IsUnicode(false);

            modelBuilder.Entity<Donhang>()
                .Property(e => e.soDienThoai)
                .IsUnicode(false);

            modelBuilder.Entity<Donhang>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<loaiTaiKhoan>()
                .Property(e => e.level)
                .IsUnicode(false);

            modelBuilder.Entity<loaiTaiKhoan>()
                .HasMany(e => e.TaiKhoans)
                .WithRequired(e => e.loaiTaiKhoan1)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.gia)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.kichThuoc)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.tenDangNhap)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.loaiTaiKhoan)
                .IsUnicode(false);
        }
    }
}

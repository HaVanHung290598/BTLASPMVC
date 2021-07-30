using BTL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL.Controllers
{
    public class HomeController : Controller
    {
        QLDoDa db = new QLDoDa();
        public ActionResult Index()
        {
            List<SanPham> sanPhams = new List<SanPham>();
             sanPhams = db.SanPhams.Select(s => s).ToList();
            return View(sanPhams);
        }
        public ActionResult SanPham(string id)
        {
            List<SanPham> sanPhams = new List<SanPham>();
            if (id == null)
            {
                sanPhams = db.SanPhams.Select(s => s).ToList();
            }
            else
            {
                sanPhams = db.SanPhams.Where(s => s.maDanhMuc.ToString().Equals(id)).Select(s => s).ToList();
            }
            return View(sanPhams);
        }
        public ActionResult SanPhamChiTiet(string id)
        {
            List<SanPham> sanPhams = new List<SanPham>();
            if (id == null)
            {
                sanPhams = db.SanPhams.Select(s => s).ToList();
            }
            else
            {
                sanPhams = db.SanPhams.Where(s => s.maSanPham.ToString().Equals(id)).Select(s => s).ToList();
            }
            return View(sanPhams);
        }
        public ActionResult LienHe()
        {

            return View();
        }

        public ActionResult TinTuc()
        {
            return View();
        }

        public ActionResult TaiKhoan()
        {
            return View();
        }

        public ActionResult DangNhap()
        {
            return View();
        }

        public ActionResult GioHang()
        {
            List<SanPham> sanPhams = new List<SanPham>();
            sanPhams = db.SanPhams.Select(s => s).ToList();
            return View(sanPhams);
        }
        public PartialViewResult _Menu()
        {
            var danhMuc = db.DanhMucs.Select(n => n);
            return PartialView(danhMuc);
        }
        public PartialViewResult _Aside()
        {
            var sanPhams = db.SanPhams.OrderByDescending(sp => sp.gia).Select(sp => sp).Take(4);
            return PartialView(sanPhams);
        }

        public ActionResult GioiThieu()
        {
            return View();
        }
    }
}
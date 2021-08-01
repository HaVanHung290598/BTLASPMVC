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
            if(Session["maTaiKhoan"] != null)
            {
                List<SanPham> sanPhams = new List<SanPham>();
                sanPhams = db.SanPhams.Select(s => s).ToList();
                return View(sanPhams);
            }
            else
            {
                return RedirectToAction("DangNhap");
            }

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(string tenDangNhap, string password)
        {
            if (ModelState.IsValid)
            {
                var user = db.TaiKhoans.Where(u => u.tenDangNhap.Equals(tenDangNhap) && u.password.Equals(password)).ToList();
                if(user.Count() > 0)
                {
                    Session["Email"] = user.FirstOrDefault().tenDangNhap;
                    Session["maTaiKhoan"] = user.FirstOrDefault().maTaiKhoan;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Đăng nhập không thành công";
                }
            }
            return View();
        }

        public ActionResult GioHang()
        {
            List<Donhang> donhangs = new List<Donhang>();
            donhangs = db.Donhangs.Select(s => s).ToList();
            return View(donhangs);
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

        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("DangNhap");
        }
    }
}
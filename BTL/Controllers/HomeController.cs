using BTL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using PagedList;
using System.IO;
using System.Data;

namespace BTL.Controllers
{
    public class HomeController : Controller
    {
        QLDoDa db = new QLDoDa();
        public ActionResult Index()
        {
            if (Session["maTaiKhoan"] != null)
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
        public ActionResult SanPham(string id, string sortOder, string search, string currentFilter, int? page)
        {
            if (id == null)
            {
                if (search != null)
                {
                    page = 1;  //Trang  đầu  tiên
                }
                else
                {
                    search = currentFilter;
                }
                ViewBag.CurrentFilter = search;
                ViewBag.CurrentSort = sortOder;//Biến  lấy  yêu  cầu  sắp  xếp  hiện  tại
                ViewBag.SapTheoTen = String.IsNullOrEmpty(sortOder) ? "name_desc" : "";
                ViewBag.SapTheoGia = sortOder == "Gia" ? "gia_desc" : "Gia";
                var sanPhams = db.SanPhams.Select(s => s);
                if (!String.IsNullOrEmpty(search))
                {
                    sanPhams = sanPhams.Where(p => p.tenSanPham.Contains(search));
                }
                switch (sortOder)
                {
                    case "name_desc":
                        sanPhams = sanPhams.OrderByDescending(s => s.tenSanPham);
                        break;
                    case "Gia":
                        sanPhams = sanPhams.OrderBy(s => s.gia);
                        break;
                    case "gia_desc":
                        sanPhams = sanPhams.OrderByDescending(s => s.gia);
                        break;
                    default:
                        sanPhams = sanPhams.OrderBy(s => s.tenSanPham);
                        break;

                }
                int pageSize = 8;  //Kích  thước  trang
                int pageNumber = (page ?? 1);
                return View(sanPhams.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                if (search != null)
                {
                    page = 1;  //Trang  đầu  tiên
                }
                else
                {
                    search = currentFilter;
                }
                ViewBag.CurrentFilter = search;
                ViewBag.CurrentSort = sortOder;//Biến  lấy  yêu  cầu  sắp  xếp  hiện  tại
                ViewBag.SapTheoTen = String.IsNullOrEmpty(sortOder) ? "name_desc" : "";
                ViewBag.SapTheoGia = sortOder == "Gia" ? "gia_desc" : "Gia";
                var sanPhams = db.SanPhams.Where(s => s.maDanhMuc.ToString().Equals(id)).Select(s => s);
                if (!String.IsNullOrEmpty(search))
                {
                    sanPhams = sanPhams.Where(p => p.tenSanPham.Contains(search));
                }
                switch (sortOder)
                {
                    case "name_desc":
                        sanPhams = sanPhams.OrderByDescending(s => s.tenSanPham);
                        break;
                    case "Gia":
                        sanPhams = sanPhams.OrderBy(s => s.gia);
                        break;
                    case "gia_desc":
                        sanPhams = sanPhams.OrderByDescending(s => s.gia);
                        break;
                    default:
                        sanPhams = sanPhams.OrderBy(s => s.tenSanPham);
                        break;

                }
                int pageSize = 8;  //Kích  thước  trang
                int pageNumber = (page ?? 1);
                return View(sanPhams.ToPagedList(pageNumber, pageSize));
            }
        }
        public ActionResult SanPhamChiTiet(string id)
        {
            Session["id"] = id;
            
            List<SanPham> sanPhams = new List<SanPham>();
            
            sanPhams = db.SanPhams.Where(s => s.maSanPham.ToString().Equals(id)).Select(s => s).ToList();
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
                if (user.Count() > 0)
                {
                    Session["Email"] = user.FirstOrDefault().tenDangNhap;
                    Session["maTaiKhoan"] = user.FirstOrDefault().maTaiKhoan;
                    Session["loaiTaiKhoan"] = user.FirstOrDefault().loaiTaiKhoan;
                    Session["mail"] = user.FirstOrDefault().email;
                    Session["HoTen"] = user.FirstOrDefault().hoTen;
                    Session["Anh"] = user.FirstOrDefault().anh;
                    Session["DiaChi"] = user.FirstOrDefault().diaChi;
                    Session["sdt"] = user.FirstOrDefault().sdt;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Sai tên đăng nhập hoặc mật khẩu";
                }
            }
            return View();
        }

        public ActionResult register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult register([Bind(Include = "maTaiKhoan,maLoaiTK, tenDangNhap, password, email, hoTen, anh, diaChi, sdt")] TaiKhoan taiKhoan)
        {
            taiKhoan.maTaiKhoan = db.TaiKhoans.ToList().Last().maTaiKhoan + 1;
            taiKhoan.maLoaiTK = 2;
            taiKhoan.anh = "";
            var f = Request.Files["ImageFile"];

            if (f != null && f.ContentLength > 0)
            {
                string FileName = System.IO.Path.GetFileName(f.FileName);
                string UploadPath = Path.Combine(Server.MapPath("~/wwwroot/Images/"), FileName);
                f.SaveAs(UploadPath);
                taiKhoan.anh = FileName;
            }
            List<TaiKhoan> taiKhoans = new List<TaiKhoan>();
            string acc = taiKhoan.tenDangNhap.ToString();
            taiKhoans = db.TaiKhoans.Where(s => s.tenDangNhap.ToString().Equals(acc)).Select(s =>s).ToList();
            if(taiKhoans.Count == 0)
            {
                db.TaiKhoans.Add(taiKhoan);
                db.SaveChanges();
                TempData["Message"] = "Đăng Ký Thành Công";
                return RedirectToAction("DangNhap");
            }
            else
            {
                ViewBag.errDky = "Tên Đăng Nhập Đã tồn tại";
                return View();
            }
        }

        public ActionResult DonHang()
        {
            
            return View();
        }

        public ActionResult GioHang(int? maSanPham, int? soLuong, double? gia)
        {
            if (Session["maTaiKhoan"] == null)
            {
                ViewBag.notice = "Bạn hãy đăng nhập để xem giỏ hàng";
                ViewBag.gioHang = Session["gioHang"];
                return View();
            }
            else if (maSanPham == null && Session["gioHang"] == null)
            {
                ViewBag.noItem = "Chưa có sản phẩm nào trong giỏ hàng";
                return View();
            }
            else { 
                List<SanPham> a = new List<SanPham>();
                var sp = db.SanPhams.Find(maSanPham);
                if(Session["gioHang"] == null)
                {
                    sp.soLuongCo = (int)soLuong;
                    sp.gia = (decimal)gia * (int)soLuong;
                    a.Add(sp);
                    Session["gioHang"] = a;
                }
                else
                {
                    a = (List<SanPham>)Session["gioHang"];
                    bool m = false;
                    foreach(var item in a)
                    {
                        if (item.maSanPham == maSanPham)
                        {
                            m = true;
                            break;
                        }
                    }
                    if (m == true)
                    {
                        foreach (var item in a)
                        {
                            if (item.maSanPham == maSanPham)
                            {
                                sp.soLuongCo = item.soLuongCo + (int)soLuong;
                                sp.gia = item.gia + ((decimal)gia*(int)soLuong);
                                Session["bientam"] = item;
                            }
                        }
                        a.Remove((SanPham)Session["bientam"]);
                        a.Add(sp);
                        Session["gioHang"] = a;
                    }
                    else if (soLuong == null || gia == null)
                    {
                        Session["gioHang"] = a;
                    }
                    else
                    {
                        sp.soLuongCo = (int)soLuong;
                        sp.gia = (decimal)gia * (int)soLuong;
                        a.Add(sp);
                        Session["gioHang"] = a;
                    }
                }
                ViewBag.gioHang = Session["gioHang"];
                return View();
            }
        }

        public ActionResult DeleteGioHang(int? maSanPham)
        {
            List<SanPham> a = new List<SanPham>();
            a = (List<SanPham>)Session["gioHang"];
            foreach (var item in a)
            {
                if (item.maSanPham == maSanPham)
                {
                    Session["bientam"] = item;
                }
            }
            a.Remove((SanPham)Session["bientam"]);
            Session["gioHang"] = a;
            return RedirectToAction("GioHang");
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
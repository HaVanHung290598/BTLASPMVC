using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using BTL.Models;
using System.IO;

namespace BTL.Areas.Admin.Controllers
{
    public class ProductManagerController : Controller
    {
        private QLDoDa db = new QLDoDa();

        // GET: Admin/ProductManager
        public ActionResult ProductManager()
        {
            ViewBag.Categorys = db.DanhMucs.ToList();
            return View(db.SanPhams.ToList());
        }

        // GET: /Admin/ProductManager/Details
        public JsonResult Details(int? id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            DanhMuc danhMuc = db.DanhMucs.Find(sanPham.maDanhMuc);
            return Json(new
            {
                sanPham = sanPham,
                danhMuc = danhMuc,
            }, JsonRequestBehavior.AllowGet);
        }

        // POST: Admin/ProductManager/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maSanPham,tenSanPham,gia,moTa,anh,chatLieu,kieuDang,thietKe,thuongHieu,mauSac,kichThuoc,maDanhMuc")] SanPham sanPham)
        {
            sanPham.maSanPham = db.SanPhams.ToList().Count() + 1;
            sanPham.anh = "";
            var f = Request.Files["ImageFile"];
            if (f != null && f.ContentLength > 0)
            {
                string FileName = System.IO.Path.GetFileName(f.FileName);
                string UploadPath = Path.Combine(Server.MapPath("~/wwwroot/asset/images/products"), FileName);
                f.SaveAs(UploadPath);
                sanPham.anh = FileName;
            }
            db.SanPhams.Add(sanPham);
            db.SaveChanges();
            return RedirectToAction("ProductManager");
        }

        // POST: Admin/ProductManager/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maSanPham,tenSanPham,gia,moTa,anh,chatLieu,kieuDang,thietKe,thuongHieu,mauSac,kichThuoc,maDanhMuc")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {

                var f = Request.Files["ImageFile"];
                if (f != null && f.ContentLength > 0)
                {
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    string UploadPath = Path.Combine(Server.MapPath("~/wwwroot/asset/images/products"), FileName);
                    f.SaveAs(UploadPath);
                    sanPham.anh = FileName;
                }
                try
                {
                    db.Entry(sanPham).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ProductManager");
                }
                catch (Exception exp)
                {
                    ViewBag.error = exp.Message;
                    return RedirectToAction("ProductManager");
                }
                
            }
            return RedirectToAction("ProductManager");
        }
    }
}
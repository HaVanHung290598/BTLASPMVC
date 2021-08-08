using BTL.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL.Areas.Admin.Controllers
{
    public class AccountManagerController : Controller
    {
        private QLDoDa db = new QLDoDa();

        // GET: Admin/AccountManager
        public ActionResult AccountManager(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SortWithName = String.IsNullOrEmpty(sortOrder) ?
                ""
                : sortOrder == "name_desc" ?
                    "name_desc"
                    : sortOrder == "name" ?
                        "name"
                        : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            // Mặc đinh lấy all tài khoản và lấy từ mới => cũ
            var accounts = db.TaiKhoans.Select(s => s).OrderByDescending(s => s.maTaiKhoan);

            if (!String.IsNullOrEmpty(searchString))
            {
                accounts = (IOrderedQueryable<TaiKhoan>)accounts.Where(s => s.hoTen.Contains(searchString) || s.tenDangNhap.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    accounts = accounts.OrderByDescending(s => s.hoTen);
                    break;
                case "name":
                    accounts = accounts.OrderBy(s => s.hoTen);
                    break;
                default:
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            
            ViewBag.Roles = db.loaiTaiKhoans.ToList();
            return View(accounts.ToPagedList(pageNumber, pageSize));
        }

        // POST: Admin/TaiKhoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "hoTen,tenDangNhap,password,anh,email,sdt,diaChi,maLoaiTK")] TaiKhoan taiKhoan)
        {
            taiKhoan.maTaiKhoan = db.TaiKhoans.ToList().Last().maTaiKhoan + 1;
            taiKhoan.anh = "";
            var f = Request.Files["ImageFile"];
            if (f != null && f.ContentLength > 0)
            {
                string FileName = System.IO.Path.GetFileName(f.FileName);
                string UploadPath = Path.Combine(Server.MapPath("~/wwwroot/asset/images/account"), FileName);
                f.SaveAs(UploadPath);
                taiKhoan.anh = FileName;
            }
            db.TaiKhoans.Add(taiKhoan);
            db.SaveChanges();
            return RedirectToAction("AccountManager");
        }

        // POST: Admin/TaiKhoans/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            db.TaiKhoans.Remove(taiKhoan);
            db.SaveChanges();
            return RedirectToAction("AccountManager");
        }

        // GET: Admin/TaiKhoans/Edit/5
        public ActionResult Edit(int? id)
        {
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            ViewBag.maLoaiTK = new SelectList(db.loaiTaiKhoans, "maLoaiTK", "level", taiKhoan.maLoaiTK);
            return View(taiKhoan);
        }

        // POST: Admin/TaiKhoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maTaiKhoan,hoTen, tenDangNhap, password, anh, email, sdt, diaChi, maLoaiTK")] TaiKhoan taiKhoan)
        {
            taiKhoan.anh = "";
            var f = Request.Files["ImageFile"];
            if (f != null && f.ContentLength > 0)
            {
                string FileName = System.IO.Path.GetFileName(f.FileName);
                string UploadPath = Path.Combine(Server.MapPath("~/wwwroot/asset/images/account"), FileName);
                f.SaveAs(UploadPath);
                taiKhoan.anh = FileName;
            }
            db.Entry(taiKhoan).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("AccountManager");
        }
    }
}
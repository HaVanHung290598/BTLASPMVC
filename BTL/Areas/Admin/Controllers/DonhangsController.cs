using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BTL.Models;
using PagedList;

namespace BTL.Areas.Admin.Controllers
{
    public class DonhangsController : Controller
    {
        private QLDoDa db = new QLDoDa();

        // GET: Admin/Donhangs
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page, string filterStatus, string currentFilterWithStatus)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SortWithName = sortOrder;

            if (searchString != null || filterStatus != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
                filterStatus = currentFilterWithStatus;
            }
            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentFilterWithStatus = filterStatus;
            ViewBag.filterStatus = filterStatus;

            // Mặc đinh lấy all tài khoản và lấy từ mới => cũ
            var invoices = db.Donhangs.Select(s => s).OrderByDescending(s => s.maDonHang);

            if (!String.IsNullOrEmpty(searchString))
            {
                invoices = (IOrderedQueryable<Donhang>)invoices.Where(s => s.maDonHang.ToString() == searchString);
            }

            if (!String.IsNullOrEmpty(filterStatus))
            {
                invoices = (IOrderedQueryable<Donhang>)invoices.Where(s => s.trangThai == filterStatus);
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(invoices.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Donhangs/Details/5
        public ActionResult Details(int? maDonHang, int? maTaiKhoan)
        {
            Donhang donhang = db.Donhangs.Find(maDonHang, maTaiKhoan);
            var chiTietDonHangs = db.Chitietdonhangs.Select(s => s).Where(s => s.maDonHang == maDonHang);
            TaiKhoan taiKhoan = db.TaiKhoans.Find(maTaiKhoan);
            ViewBag.chiTietDonHangs = chiTietDonHangs;
            ViewBag.taiKhoan = taiKhoan;
            return View(donhang);
        }

        // GET: Admin/Donhangs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Donhangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maDonHang,hoTen,diaChi,soDienThoai,email,chuThich")] Donhang donhang)
        {
            if (ModelState.IsValid)
            {
                db.Donhangs.Add(donhang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(donhang);
        }

        // GET: Admin/Donhangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donhang donhang = db.Donhangs.Find(id);
            if (donhang == null)
            {
                return HttpNotFound();
            }
            return View(donhang);
        }

        [HttpPost]
        public ActionResult Edit(string statusValue, int maDonHang, int maTaiKhoan)
        {
            Donhang donHang = db.Donhangs.Find(maDonHang, maTaiKhoan);
            donHang.trangThai = statusValue;
            db.Entry(donHang).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin/Donhangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donhang donhang = db.Donhangs.Find(id);
            if (donhang == null)
            {
                return HttpNotFound();
            }
            return View(donhang);
        }

        // POST: Admin/Donhangs/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(int maDonHang, int maTaiKhoan)
        {
            var chiTietDonHangs = db.Chitietdonhangs.Select(s => s).Where(s => s.maDonHang == maDonHang);
            foreach(Chitietdonhang item in chiTietDonHangs)
            {
                db.Chitietdonhangs.Remove(item);
            }
            Donhang donhang = db.Donhangs.Find(maDonHang, maTaiKhoan);
            db.Donhangs.Remove(donhang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

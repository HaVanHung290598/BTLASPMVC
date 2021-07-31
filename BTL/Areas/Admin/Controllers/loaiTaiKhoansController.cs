using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BTL.Models;

namespace BTL.Areas.Admin.Controllers
{
    public class loaiTaiKhoansController : Controller
    {
        private QLDoDa db = new QLDoDa();

        // GET: Admin/loaiTaiKhoans
        public ActionResult Index()
        {
            return View(db.loaiTaiKhoans.ToList());
        }

        // GET: Admin/loaiTaiKhoans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loaiTaiKhoan loaiTaiKhoan = db.loaiTaiKhoans.Find(id);
            if (loaiTaiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(loaiTaiKhoan);
        }

        // GET: Admin/loaiTaiKhoans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/loaiTaiKhoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maLoaiTK,level")] loaiTaiKhoan loaiTaiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.loaiTaiKhoans.Add(loaiTaiKhoan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiTaiKhoan);
        }

        // GET: Admin/loaiTaiKhoans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loaiTaiKhoan loaiTaiKhoan = db.loaiTaiKhoans.Find(id);
            if (loaiTaiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(loaiTaiKhoan);
        }

        // POST: Admin/loaiTaiKhoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maLoaiTK,level")] loaiTaiKhoan loaiTaiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiTaiKhoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiTaiKhoan);
        }

        // GET: Admin/loaiTaiKhoans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            loaiTaiKhoan loaiTaiKhoan = db.loaiTaiKhoans.Find(id);
            if (loaiTaiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(loaiTaiKhoan);
        }

        // POST: Admin/loaiTaiKhoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            loaiTaiKhoan loaiTaiKhoan = db.loaiTaiKhoans.Find(id);
            db.loaiTaiKhoans.Remove(loaiTaiKhoan);
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

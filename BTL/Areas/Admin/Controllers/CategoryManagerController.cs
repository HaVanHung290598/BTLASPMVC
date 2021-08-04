using BTL.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL.Areas.Admin.Controllers
{
    public class CategoryManagerController : Controller
    {
        private QLDoDa db = new QLDoDa();

        // GET: Admin/CategoryManager
        public ActionResult CategoryManager(string sortOrder, string searchString, string currentFilter, int? page)
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

            var categorys = db.DanhMucs.Select(s => s).OrderByDescending(s => s.maDanhMuc);

            if (!String.IsNullOrEmpty(searchString))
            {
                categorys = (IOrderedQueryable<DanhMuc>)categorys.Where(s => s.tenDanhMuc.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    categorys = categorys.OrderByDescending(s => s.tenDanhMuc);
                    break;
                case "name":
                    categorys = categorys.OrderBy(s => s.tenDanhMuc);
                    break;
                default:
                    break;
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);

            return View(categorys.ToPagedList(pageNumber, pageSize));
        }

        // POST: Admin/DanhMucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maDanhMuc,tenDanhMuc")] DanhMuc danhMuc)
        {
            danhMuc.maDanhMuc = db.DanhMucs.ToList().Last().maDanhMuc + 1;
            db.DanhMucs.Add(danhMuc);
            db.SaveChanges();
            return RedirectToAction("CategoryManager");
        }

        // POST: Admin/DanhMucs/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            DanhMuc danhMuc = db.DanhMucs.Find(id);
            db.DanhMucs.Remove(danhMuc);
            db.SaveChanges();
            return RedirectToAction("CategoryManager");
        }

        // POST: Admin/DanhMucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult Edit([Bind(Include = "maDanhMuc,tenDanhMuc")] DanhMuc danhMuc)
        {
            db.Entry(danhMuc).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new
            {danhMuc = danhMuc}, JsonRequestBehavior.AllowGet);
        }
    }
}
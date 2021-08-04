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
using PagedList;

namespace BTL.Areas.Admin.Controllers
{
    public class ProductManagerController : Controller
    {
        private QLDoDa db = new QLDoDa();

        // GET: Admin/ProductManager
        public ActionResult ProductManager(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SortWithName = String.IsNullOrEmpty(sortOrder) ?
                "" 
                : sortOrder == "name_desc" ?
                    "name_desc" 
                    : sortOrder == "name" ?
                        "name"
                        : "";
            ViewBag.SortWithPrice = String.IsNullOrEmpty(sortOrder) ?
                "" 
                : sortOrder == "price_desc" ?
                    "price_desc"
                    : sortOrder == "price" ?
                        "price"
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

            // Mặc đinh lấy all sản phẩm và lấy từ mới => cũ
            var products = db.SanPhams.Select(s => s).OrderByDescending(s => s.maSanPham);

            if (!String.IsNullOrEmpty(searchString))
            {
                products = (IOrderedQueryable<SanPham>)products.Where(s => s.tenSanPham.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(s => s.tenSanPham);
                    break;
                case "name":
                    products = products.OrderBy(s => s.tenSanPham);
                    break;
                case "price":
                    products = products.OrderBy(s => s.gia);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(s => s.gia);
                    break;
                default:
                    break;
            }

            int pageSize = 12;
            int pageNumber = (page ?? 1);

            ViewBag.Categorys = db.DanhMucs.ToList();
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        // GET: /Admin/ProductManager/Details
        [HttpGet]
        public JsonResult Details(int? id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            return Json(new { sanPham = sanPham }, JsonRequestBehavior.AllowGet);
        }

        // POST: Admin/ProductManager/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maSanPham,tenSanPham,gia,moTa,anh,chatLieu,kieuDang,thietKe,thuongHieu,mauSac,kichThuoc,maDanhMuc")] SanPham sanPham)
        {
            sanPham.maSanPham = db.SanPhams.ToList().Last().maSanPham + 1;
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

        /*// POST: Hangs/Delete/5
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            SanPham product = db.SanPhams.Find(id);
            db.SanPhams.Remove(product);
            db.SaveChanges();
            return RedirectToAction("ProductManager");
        }*/
        /*[HttpDelete]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            SanPham product = db.SanPhams.Find(id);
            db.SanPhams.Remove(product);
            db.SaveChanges();
            return RedirectToAction("ProductManager");
        }*/

        // POST: Admin/SanPhams/Delete/5
        /* [AllowAnonymous]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            SanPham product = db.SanPhams.Find(id);
            db.SanPhams.Remove(product);
            db.SaveChanges();
            return RedirectToAction("ProductManager", "ProductManager");
        }*/

        // GET: Admin/SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("ProductManager");
        }
    }
}
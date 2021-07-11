using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using System.Net;
using BTL.Models;

namespace BTL.Areas.Admin.Controllers
{
    public class ProductManagerController : Controller
    {
        private DoDaStoreDB db = new DoDaStoreDB();

        // GET: Admin/ProductManager
        public ActionResult ProductManager()
        {
            return View();
        }

        // GET: Admin/Details
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
    }
}
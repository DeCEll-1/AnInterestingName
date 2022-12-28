using AnInterestingWebSiteName.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnInterestingWebSiteName.Areas.Admin.Controllers
{
    public class KategoriController : Controller
    {
        AnInterestingWebSiteName_Model db = new AnInterestingWebSiteName_Model();
        // GET: Admin/Kategori
        public ActionResult Index()
        {
            return View(db.Kategoris.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(Kategori model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.message = "Kategori Eklendi";
                db.Kategoris.Add(model);
                db.SaveChanges();
            }
            else
            {
                ViewBag.message = "Kategori Eklenemedi";
            }

            return View();
        }

    }
}
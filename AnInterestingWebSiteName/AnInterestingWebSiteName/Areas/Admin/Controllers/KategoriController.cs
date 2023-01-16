using AnInterestingWebSiteName.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            List<SelectListItem> kategoriler = new List<SelectListItem>();
            kategoriler.Add(new SelectListItem { Text = "Üst Kategori", Value = "0", Selected = true });
            foreach (Kategori item in db.Kategoris.ToList())
            {
                kategoriler.Add(new SelectListItem { Text = item.Ad, Value = item.ID.ToString(), Selected = false });
            }

            ViewBag.UstKategoriler = kategoriler;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Kategori model)
        {
            #region list
            List<SelectListItem> kategoriler = new List<SelectListItem>();
            kategoriler.Add(new SelectListItem { Text = "Üst Kategori", Value = "0", Selected = true });
            foreach (Kategori item in db.Kategoris.ToList())
            {
                kategoriler.Add(new SelectListItem { Text = item.Ad, Value = item.ID.ToString(), Selected = false });
            }

            ViewBag.UstKategoriler = kategoriler;
            #endregion
            if (ModelState.IsValid)
            {

                if (model.UstKategori_ID==0)
                {
                    model.UstKategori_ID = null;
                }
                else
                {
                    model.UstKategori = db.Kategoris.Find(model.UstKategori_ID);
                }


                db.Kategoris.Add(model);
                db.SaveChanges();
                ViewBag.message = "Kategori Eklendi";
            }
            else
            {
                ViewBag.message = "Kategori Düzenlemede Bir Sıkıntı Oluştu Lütfen Tekrar Deneyiniz";
            }


            return View();
        }
        [HttpGet]
        public ActionResult Edit(int? ID)
        {
            if (ID == null || db.Kategoris.Find(ID) == null)
            {
                ViewBag.message = "Yanlış Bir ID Girdiniz";
                return RedirectToAction("Index");
            }

            return View(db.Kategoris.Find(ID));
        }

        [HttpPost]
        public ActionResult Edit(Kategori model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (model.UstKategori_ID==0)
                    {
                        model.UstKategori_ID = null;
                    }



                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.message = "Kategori Düzenlendi";
                }
                catch 
                {
                    ViewBag.message = "Kategori Düzenlemede Bir Sıkıntı Oluştu Lütfen Tekrar Deneyiniz";

                }
            }
            else
            {
                ViewBag.message = "Kategori Düzenlemede Bir Sıkıntı Oluştu Lütfen Tekrar Deneyiniz";
            }

            return View(model);
        }

        public ActionResult Delete(int? ID)
        {
            if (ID == null|| db.Kategoris.Find(ID)==null)
            {
                ViewBag.message = "Yanlış Bir ID Girdiniz";
            }
            else
            {
                db.Kategoris.Remove(db.Kategoris.Find(ID));
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
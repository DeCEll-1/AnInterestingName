using AnInterestingWebSiteName.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnInterestingWebSiteName.Areas.Admin.Controllers
{
    public class UrunController : Controller
    {
        AnInterestingWebSiteName_Model db = new AnInterestingWebSiteName_Model();
        public ActionResult Index()
        {
            return View(db.Urunlers.ToList().Where(s => s.Aktifmi == true));
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Kategori_ID = new SelectList(db.Kategoris.ToList(), "ID", "Ad");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Urunler model, HttpPostedFileBase icon, HttpPostedFileBase fullImage)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (icon != null)
                    {
                        FileInfo fi = new FileInfo(icon.FileName);
                        string name = Guid.NewGuid().ToString() + fi.Extension;
                        model.IkonYolu = name;
                        icon.SaveAs(Server.MapPath($"~/Fotograflar/UrunFotograflari/{name}"));
                    }
                    else
                    {
                        model.TamResimYolu = "Sad.png";
                    }

                    if (fullImage != null)
                    {
                        FileInfo fi = new FileInfo(fullImage.FileName);
                        string name = Guid.NewGuid().ToString() + fi.Extension;
                        model.IkonYolu = name;
                        fullImage.SaveAs(Server.MapPath($"~/Fotograflar/UrunFotograflari/{name}"));
                    }
                    else
                    {
                        model.IkonYolu = "Sad.png";
                    }
                    model.Aktifmi = true;
                    db.Urunlers.Add(model);
                    db.SaveChanges();
                    ViewBag.message = "Ürün Ekleme Başarılı";
                }
                catch (Exception ex)
                {
                    ViewBag.message = "Ürün Ekleme Başarısız\nHata =" + ex.Message;
                }
            }
            ViewBag.Kategori_ID = new SelectList(db.Kategoris.ToList(), "ID", "Ad");
            return View(model);
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            ViewBag.Kategori_ID = new SelectList(db.Kategoris.ToList(), "ID", "Ad");

            if (id == null || db.Urunlers.Find(id) == null)
            {
                ViewBag.message = "Yanlış Bir ID Girdiniz";
                return RedirectToAction("Index");
            }
            return View(db.Urunlers.Find(id));
        }
        
        [HttpPost]
        public ActionResult Edit(Urunler model, HttpPostedFileBase icon, HttpPostedFileBase fullImage)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (icon != null)
                    {
                        FileInfo fi = new FileInfo(icon.FileName);
                        string name = Guid.NewGuid().ToString() + fi.Extension;
                        model.IkonYolu = name;
                        icon.SaveAs(Server.MapPath($"~/Fotograflar/UrunFotograflari/{name}"));
                    }
                    else
                    {
                        model.TamResimYolu = "Sad.png";
                    }

                    if (fullImage != null)
                    {
                        FileInfo fi = new FileInfo(fullImage.FileName);
                        string name = Guid.NewGuid().ToString() + fi.Extension;
                        model.IkonYolu = name;
                        fullImage.SaveAs(Server.MapPath($"~/Fotograflar/UrunFotograflari/{name}"));
                    }
                    else
                    {
                        model.IkonYolu = "Sad.png";
                    }
                    model.Aktifmi = true;

                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.message = "Ürün Ekleme Başarılı";
                }
                catch (Exception ex)
                {
                    ViewBag.message = "Ürün Düzenleme Başarısız\nHata Kodu" + ex.Message;
                }
            }

            ViewBag.Kategori_ID = new SelectList(db.Kategoris.ToList(), "ID", "Ad");
            return View(model);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id!=null||db.Urunlers.Find(id)==null)
            {

                db.Urunlers.Remove(db.Urunlers.Find(id));
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


    }
}
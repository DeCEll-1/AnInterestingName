using AnInterestingWebSiteName.Areas.Admin.Filtre;
using AnInterestingWebSiteName.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnInterestingWebSiteName.Areas.Admin.Controllers
{
    [ModeratorAuthenticationFilter]
    public class UrunController : Controller
    {
        public ActionResult Index()
        {
            AnInterestingWebSiteName_Model db = new AnInterestingWebSiteName_Model();
            return View(db.Urunlers.ToList().Where(s => s.Aktifmi == true));
        }

        [AdminAuthenticationFilter]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [AdminAuthenticationFilter]
        [HttpPost]
        public ActionResult Create(Urunler model, HttpPostedFileBase icon, HttpPostedFileBase fullImage)
        {
            if (icon == null || fullImage == null)
            {
                ViewBag.message = "Lütfen Fotoğraf Giriniz";
                return View();
            }
            ModelState.Remove("IkonYolu");
            ModelState.Remove("TamResimYolu");
            if (ModelState.IsValid)
            {
                try
                {
                    AnInterestingWebSiteName_Model db = new AnInterestingWebSiteName_Model();

                    FileInfo fimr = new FileInfo(icon.FileName);
                    string namemr = Guid.NewGuid().ToString() + fimr.Extension;
                    model.IkonYolu = namemr;
                    icon.SaveAs(Server.MapPath($"~/Fotograflar/UrunFotograflari/{namemr}"));


                    FileInfo fifi = new FileInfo(fullImage.FileName);
                    string namefi = Guid.NewGuid().ToString() + fifi.Extension;
                    model.TamResimYolu = namefi;
                    fullImage.SaveAs(Server.MapPath($"~/Fotograflar/UrunFotograflari/{namefi}"));

                    if (model.Indirim == null)
                    {
                        model.Indirim = 0;
                    }

                    model.Aktifmi = true;

                    model.IndirimliFiyat = model.Fiyat - ((model.Fiyat * model.Indirim) / 100);

                    db.Urunlers.Add(model);
                    db.SaveChanges();
                    ViewBag.message = "Ürün Ekleme Başarılı";
                }
                catch (Exception ex)
                {
                    ViewBag.message = "Ürün Ekleme Başarısız\nHata =" + ex.Message;
                }
            }
            return View(model);
        }


        [AdminAuthenticationFilter]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            AnInterestingWebSiteName_Model db = new AnInterestingWebSiteName_Model();

            if (id == null || db.Urunlers.Find(id) == null)
            {
                ViewBag.message = "Yanlış Bir ID Girdiniz";
                return RedirectToAction("Index");
            }
            return View(db.Urunlers.Find(id));
        }

        [AdminAuthenticationFilter]
        [HttpPost]
        public ActionResult Edit(Urunler model, HttpPostedFileBase icon, HttpPostedFileBase fullImage)
        {


            ModelState.Remove("IkonYolu");
            ModelState.Remove("TamResimYolu");
            if (ModelState.IsValid)
            {
                try
                {
                    AnInterestingWebSiteName_Model db = new AnInterestingWebSiteName_Model();

                    if (icon != null)
                    {
                        FileInfo fidi = new FileInfo(Server.MapPath($"~/Fotograflar/UrunFotograflari/{db.Urunlers.FirstOrDefault(s => s.ID == model.ID).IkonYolu}"));
                        fidi.Delete();

                        FileInfo fii = new FileInfo(icon.FileName);
                        string namei = Guid.NewGuid().ToString() + fii.Extension;
                        model.IkonYolu = namei;
                        icon.SaveAs(Server.MapPath($"~/Fotograflar/UrunFotograflari/{namei}"));
                    }
                    else
                    {
                        model.IkonYolu = db.Urunlers.FirstOrDefault(s => s.ID == model.ID).IkonYolu;
                    }


                    if (fullImage != null)
                    {
                        FileInfo fidfi = new FileInfo(Server.MapPath($"~/Fotograflar/UrunFotograflari/{db.Urunlers.FirstOrDefault(s => s.ID == model.ID).TamResimYolu}"));
                        fidfi.Delete();

                        FileInfo fifi = new FileInfo(fullImage.FileName);
                        string namefi = Guid.NewGuid().ToString() + fifi.Extension;
                        model.TamResimYolu = namefi;
                        fullImage.SaveAs(Server.MapPath($"~/Fotograflar/UrunFotograflari/{namefi}"));
                    }
                    else
                    {
                        model.TamResimYolu = db.Urunlers.FirstOrDefault(s => s.ID == model.ID).TamResimYolu;
                    }

                    model.Aktifmi = true;

                    model.IndirimliFiyat = model.Fiyat - ((model.Fiyat * model.Indirim) / 100);


                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.message = "Ürün Ekleme Başarılı";
                }
                catch (Exception ex)
                {
                    ViewBag.message = "Ürün Düzenleme Başarısız\nHata Kodu" + ex.Message;
                }
            }

            return View(model);
        }
        [AdminAuthenticationFilter]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            AnInterestingWebSiteName_Model db = new AnInterestingWebSiteName_Model();
            if (id != null || db.Urunlers.Find(id) == null)
            {
                Urunler u = db.Urunlers.Find(id);

                FileInfo fii = new FileInfo(Server.MapPath($"~/Fotograflar/UrunFotograflari/{u.IkonYolu}"));
                fii.Delete();

                FileInfo fifi = new FileInfo(Server.MapPath($"~/Fotograflar/UrunFotograflari/{u.TamResimYolu}"));
                fifi.Delete();

                foreach (var item in db.OyunResimleris.ToList().Where(s => s.Oyun_ID == u.ID))
                {
                    db.OyunResimleris.Remove(item);
                }

                db.Urunlers.Remove(u);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


    }
}
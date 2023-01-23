using AnInterestingWebSiteName.Areas.Admin.Filtre;
using AnInterestingWebSiteName.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnInterestingWebSiteName.Areas.Admin.Controllers
{
    [ModeratorAuthenticationFilter]
    public class TagController : Controller
    {
        AnInterestingWebSiteName_Model db = new AnInterestingWebSiteName_Model();

        [ModeratorAuthenticationFilter]
        // GET: Admin/Tag
        public ActionResult Index()
        {
            return View(db.Tags.ToList());
        }

        public ActionResult UrunIndex(int? id)
        {
            if (id == null || db.Urunlers.Find(id) == null)
            {
                ViewBag.message = "Yanlış Bir ID Girdiniz";
                return RedirectToAction("Index", "Urun");
            }
            ViewBag.etcints1 = (int)id;
            ViewBag.urunad = db.Urunlers.Find(id).Ad;
            return View(db.TagsVeUrunAraClasses.ToList().Where(s => s.Urun_ID == id));
        }

        [HttpGet]
        public ActionResult UrunTagCreate(int? id)
        {
            if (id == null || db.Urunlers.Find(id) == null)
            {
                ViewBag.message = "Yanlış Bir ID Girdiniz";
                return RedirectToAction("UrunIndex", "Tag", ViewBag.etcints1);
            }

            #region MyRegion
            //ViewBag.Tag_ID = new SelectList(null, "ID", "Ad");

            //List<Tag> tagl = db.Tags.ToList();

            //if (db.TagsVeUrunAraClasses.ToList() != null)
            //{
            //    if (db.TagsVeUrunAraClasses.ToList().Where(s => s.Urun_ID == id) != null)
            //    {
            //        foreach (var item in db.TagsVeUrunAraClasses.ToList().Where(s => s.Urun_ID == id))
            //        {
            //            foreach (var item1 in tagl)
            //            {
            //                if (!(item.Tag_ID == item1.ID))
            //                {
            //                    ViewBag.Tag_ID += item1;
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        ViewBag.Tag_ID = db.Tags.ToList();
            //    }
            //}

            #endregion

            #region List
            List<SelectListItem> etiketler = new List<SelectListItem>();

            List<Tag> tagl = db.Tags.ToList();


            foreach (Tag item in db.Tags.ToList())
            {
                if (db.TagsVeUrunAraClasses.FirstOrDefault(s => s.Urun_ID == id) != null)
                {
                    foreach (var item2 in db.TagsVeUrunAraClasses.ToList().Where(s => s.Urun_ID == id))
                    {
                        if (!(item.ID == item2.Tag_ID))
                        {
                            etiketler.Add(new SelectListItem { Text = item.Ad, Value = item.ID.ToString(), Selected = false });
                        }
                    }
                }
                else
                {
                    etiketler.Add(new SelectListItem { Text = item.Ad, Value = item.ID.ToString(), Selected = false });
                }
            }
            ViewBag.Etiketler = etiketler;
            #endregion

            TempData["etcint2"] = (int)id;

            TagsVeUrunAraClass model = new TagsVeUrunAraClass();

            return View(model);
        }

        [HttpPost]
        public ActionResult UrunTagCreate(TagsVeUrunAraClass model, int Tag_ID)
        {
            int a = (int)TempData["etcint2"];


            if (ModelState.IsValid)
            {
                try
                {
                    model.Urun_ID = a;

                    model.Tag_ID = Tag_ID;

                    model.Tag = db.Tags.Find(Tag_ID);

                    model.Urunler = db.Urunlers.Find(model.Urun_ID);

                    db.TagsVeUrunAraClasses.Add(model);
                    db.SaveChanges();
                    ViewBag.message = "Etiket Ekelndi";
                }
                catch (Exception ex)
                {
                    ViewBag.message = "Etiket Eklenemedi\nHata:" + ex.Message;
                }
            }

            #region List
            List<SelectListItem> etiketler = new List<SelectListItem>();

            List<Tag> tagl = db.Tags.ToList();


            foreach (Tag item in db.Tags.ToList())
            {
                if (db.TagsVeUrunAraClasses.FirstOrDefault(s => s.Urun_ID == a) != null)
                {
                    foreach (var item2 in db.TagsVeUrunAraClasses.ToList().Where(s => s.Urun_ID == a))
                    {
                        if (!(item.ID == item2.Tag_ID))
                        {
                            etiketler.Add(new SelectListItem { Text = item.Ad, Value = item.ID.ToString(), Selected = false });
                        }
                    }
                }
                else
                {
                    etiketler.Add(new SelectListItem { Text = item.Ad, Value = item.ID.ToString(), Selected = false });
                }
            }
            ViewBag.Etiketler = etiketler;
            #endregion

            return View(model);
        }

        [HttpGet]
        [AdminAuthenticationFilter]
        public ActionResult Create()
        {
            #region List
            List<SelectListItem> etiketler = new List<SelectListItem>();
            etiketler.Add(new SelectListItem { Text = "Üst Etiket", Value = "0", Selected = true });
            foreach (Tag item in db.Tags.ToList())
            {
                etiketler.Add(new SelectListItem { Text = item.Ad, Value = item.ID.ToString(), Selected = false });
            }
            ViewBag.UstEtiketler = etiketler;
            #endregion

            return View();
        }

        [AdminAuthenticationFilter]
        [HttpPost]
        public ActionResult Create(Tag model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    db.Tags.Add(model);
                    db.SaveChanges();
                    ViewBag.message = "Etiket Eklendi";
                }
                catch (Exception ex)
                {
                    ViewBag.message = "Etiket Ekleme Başarısız\nHata =" + ex.Message;

                }
            }
            #region List
            List<SelectListItem> etiketler = new List<SelectListItem>();
            etiketler.Add(new SelectListItem { Text = "Üst Etiket", Value = "0", Selected = true });
            foreach (Tag item in db.Tags.ToList())
            {
                etiketler.Add(new SelectListItem { Text = item.Ad, Value = item.ID.ToString(), Selected = false });
            }
            ViewBag.UstEtiketler = etiketler;
            #endregion
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            #region List
            List<SelectListItem> etiketler = new List<SelectListItem>();
            etiketler.Add(new SelectListItem { Text = "Üst Etiket", Value = "0", Selected = true });
            foreach (Tag item in db.Tags.ToList())
            {
                etiketler.Add(new SelectListItem { Text = item.Ad, Value = item.ID.ToString(), Selected = false });
            }
            ViewBag.UstEtiketler = etiketler;
            #endregion

            if (id == null || db.Tags.Find(id) == null)
            {
                ViewBag.message = "Yanlış Bir ID Girdiniz";
                return RedirectToAction("Index");
            }

            Tag model = db.Tags.Find(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Tag model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    ViewBag.message = "Etiket Düzenlendi";
                }
                catch (Exception ex)
                {
                    ViewBag.message = "Etiket Düzenleme Başarısız\nHata =" + ex.Message;

                }
            }
            #region List
            List<SelectListItem> etiketler = new List<SelectListItem>();
            etiketler.Add(new SelectListItem { Text = "Üst Etiket", Value = "0", Selected = true });
            foreach (Tag item in db.Tags.ToList())
            {
                etiketler.Add(new SelectListItem { Text = item.Ad, Value = item.ID.ToString(), Selected = false });
            }
            ViewBag.UstEtiketler = etiketler;
            #endregion
            return View(model);
        }

        [AdminAuthenticationFilter]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null || db.Tags.Find(id) == null)
            {
                ViewBag.message = "Yanlış Bir ID Girdiniz";
                return RedirectToAction("Index");
            }

            foreach (var item in db.TagsVeUrunAraClasses.ToList().Where(s => s.Tag_ID == id))
            {
                db.TagsVeUrunAraClasses.Remove(item);
            }

            db.Tags.Remove(db.Tags.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteUrun(int? id)
        {
            if (id == null || db.TagsVeUrunAraClasses.Find(id) == null)
            {
                ViewBag.message = "Yanlış Bir ID Girdiniz";
                return RedirectToAction("Index", "Urun");
            }

            int a = db.TagsVeUrunAraClasses.Find(id).Urun_ID;

            db.TagsVeUrunAraClasses.Remove(db.TagsVeUrunAraClasses.Find(id));



            db.SaveChanges();
            return RedirectToAction("UrunIndex", a);
        }
    }
}
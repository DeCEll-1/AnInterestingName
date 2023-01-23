﻿using AnInterestingWebSiteName.Areas.Admin.Filtre;
using AnInterestingWebSiteName.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnInterestingWebSiteName.Areas.Admin.Controllers
{
    [ModeratorAuthenticationFilter]
    public class AnaMenuController : Controller
    {
        AnInterestingWebSiteName_Model db = new AnInterestingWebSiteName_Model();
        // GET: Admin/AnaMenu
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit()
        {


            if (Session["adminSession"]==null)
            {
                ViewBag.message = "Bir hata oluştu lütfen tekrar giriş yapıp tekrar deneyiniz";

                return RedirectToAction("Index","Login");
            }
            Yonetici model = (Yonetici)Session["adminSession"];


            ViewBag.YoneticiTur_ID = new SelectList(db.YoneticiTurs.ToList(), "ID", "Ad");

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Yonetici model,HttpPostedFileBase pfp,string pfpcb)
        {
            ViewBag.YoneticiTur_ID = new SelectList(db.YoneticiTurs.ToList(), "ID", "Ad");

            try
            {

                if (pfpcb !=null)
                {
                    model.ProfilFotografi = "Smile.png";
                }
                else if (pfp != null)
                {
                    FileInfo fidpfp = new FileInfo(Server.MapPath($"~/Fotograflar/KullaniciFotograflari/{db.Yoneticis.Find(model.ID).ProfilFotografi}"));
                    fidpfp.Delete();

                    FileInfo fipfp = new FileInfo(pfp.FileName);
                    string namepfp = Guid.NewGuid().ToString() + fipfp.Extension;
                    model.ProfilFotografi = namepfp;
                    pfp.SaveAs(Server.MapPath($"~/Fotograflar/KullaniciFotograflari/{namepfp}"));
                }
                else
                {
                    model.ProfilFotografi = db.Yoneticis.Find(model.ID).ProfilFotografi;
                }

                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                ViewBag.message = "Düzenleme Başarılı";
            }
            catch (Exception)
            {
            }


            return View(model);
        }
    }
}
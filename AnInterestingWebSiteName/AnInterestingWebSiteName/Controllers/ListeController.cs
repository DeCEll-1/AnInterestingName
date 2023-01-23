using AnInterestingWebSiteName.Areas.Admin.Controllers;
using AnInterestingWebSiteName.Classes;
using AnInterestingWebSiteName.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnInterestingWebSiteName.Controllers
{
    public class ListeController : Controller
    {
        AnInterestingWebSiteName_Model db = new AnInterestingWebSiteName_Model();

        // GET: Liste
        [HttpGet]
        public ActionResult Index(int? id)
        {
            MagazaList ml = new MagazaList();

            ml.Urunler = db.Urunlers.ToList().Where(s => s.Aktifmi == true);

            ml.OyunResimleri = db.OyunResimleris.ToList();

            ml.TagsVeUrunAraClass = db.TagsVeUrunAraClasses.ToList();

            ml.Tag = db.Tags.ToList();

            return View(ml);
        }

        [HttpPost]
        public ActionResult Index(string searchBarText)
        {
            if (searchBarText == ""||searchBarText==null)
            {
                MagazaList mlm = new MagazaList();

                mlm.Urunler = db.Urunlers.ToList().Where(s => s.Aktifmi == true);

                mlm.OyunResimleri = db.OyunResimleris.ToList();

                mlm.TagsVeUrunAraClass = db.TagsVeUrunAraClasses.ToList();

                mlm.Tag= db.Tags.ToList();

                return View(mlm);
            }

            MagazaList ml = new MagazaList();

            MagazaList mlv = new MagazaList();

            ml.Urunler = db.Urunlers.ToList().Where(s => s.Aktifmi == true);

            ml.TagsVeUrunAraClass.ToList();

            ml.OyunResimleri.ToList();



            foreach (Urunler item in ml.Urunler)
            {
                if (item.Ad.ToLower().Contains(searchBarText.ToLower())||item.Aciklama.ToLower().Contains(searchBarText.ToLower()))
                {

                    if (mlv.Urunler == null)
                    {
                        List<Urunler> geciciu = new List<Urunler>();

                        geciciu.Add(item);

                        mlv.Urunler = geciciu;


                    }
                    else
                    {
                        List<Urunler> geciciu = (List<Urunler>)mlv.Urunler;

                        geciciu.Add(item);

                        mlv.Urunler = geciciu;
                    }

                    if (db.OyunResimleris.FirstOrDefault(s => s.Oyun_ID == item.ID) != null)
                    {
                        if (mlv.OyunResimleri == null)
                        {
                            List<OyunResimleri> gecicior = new List<OyunResimleri>();

                            gecicior.Add(db.OyunResimleris.FirstOrDefault(s => s.Oyun_ID == item.ID));

                            mlv.OyunResimleri = gecicior;
                        }
                        else
                        {
                            List<OyunResimleri> gecicior = (List<OyunResimleri>)mlv.OyunResimleri;

                            gecicior.Add(db.OyunResimleris.FirstOrDefault(s => s.Oyun_ID == item.ID));

                            mlv.OyunResimleri = gecicior;
                        }
                    }

                    if (db.TagsVeUrunAraClasses.FirstOrDefault(s => s.Urun_ID == item.ID) != null)
                    {
                        if (mlv.TagsVeUrunAraClass == null)
                        {
                            List<TagsVeUrunAraClass> gecicitvuac = new List<TagsVeUrunAraClass>();

                            gecicitvuac.Add(db.TagsVeUrunAraClasses.FirstOrDefault(s => s.Urun_ID == item.ID));

                            mlv.TagsVeUrunAraClass = gecicitvuac;
                        }
                        else
                        {
                            List<TagsVeUrunAraClass> gecicitvuac = (List<TagsVeUrunAraClass>)mlv.TagsVeUrunAraClass;

                            gecicitvuac.Add(db.TagsVeUrunAraClasses.FirstOrDefault(s => s.Urun_ID == item.ID));

                            mlv.TagsVeUrunAraClass = gecicitvuac;
                        }
                    }
                }
            }

            mlv.Tag = db.Tags.ToList();

            ViewBag.searchBarText = searchBarText;

            return View(mlv);
        }


    }
}
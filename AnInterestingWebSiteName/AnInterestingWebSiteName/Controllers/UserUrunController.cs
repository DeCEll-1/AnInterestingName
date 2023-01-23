using AnInterestingWebSiteName.Classes;
using AnInterestingWebSiteName.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnInterestingWebSiteName.Controllers
{
    public class UserUrunController : Controller
    {
        AnInterestingWebSiteName_Model db = new AnInterestingWebSiteName_Model();

        // GET: UserUrun
        public ActionResult Index(int? id)
        {

            if (id == null || db.Urunlers.Find(id).Aktifmi == false)
            {
                ViewBag.message = "Yanlış Bir ID Girildi";
                return RedirectToAction("Index", "Liste");
            }

            ProductViewUser pvu = new ProductViewUser();

            pvu.Urun = db.Urunlers.Find(id);

            pvu.Tag = null;

            bool a = true;

            foreach (var item in db.TagsVeUrunAraClasses.ToList().Where(s=>s.Urun_ID==pvu.Urun.ID))
            {

                if (a)
                {
                    List<Tag> gecicit = new List<Tag>();

                    gecicit.Add(db.Tags.FirstOrDefault(s=>s.ID==item.Tag_ID));

                    pvu.Tag = gecicit;

                    a = false;
                }
                else
                {
                    List<Tag> gecicit = (List<Tag>)pvu.Tag;

                    gecicit.Add(db.Tags.FirstOrDefault(s => s.ID == item.Tag_ID));

                    pvu.Tag = gecicit;
                }
            }




            pvu.OyunResimleri = db.OyunResimleris.ToList().Where(s=>s.Oyun_ID==pvu.Urun.ID);

            return View(pvu);
        }
    }
}
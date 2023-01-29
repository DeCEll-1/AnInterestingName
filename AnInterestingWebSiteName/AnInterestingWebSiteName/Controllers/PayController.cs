using AnInterestingWebSiteName.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnInterestingWebSiteName.Controllers
{
    public class PayController : Controller
    {
        AnInterestingWebSiteName_Model db = new AnInterestingWebSiteName_Model();
        // GET: Pay
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(int? id)
        {
            if (Session["userSession"] == null)
            {
                return RedirectToAction("Index", "UserLogin");
            }

            if (id == null || db.Urunlers.Find(id) == null)
            {
                TempData["message"] = "Ürün Bulunamadı";
                return RedirectToAction("Index", "Liste");
            }

            if (db.Urunlers.Find(id).IndirimliFiyat != 0)
            {
                TempData["message"] = "Ürün Bedava Değil";
                return RedirectToAction("Index", "Liste");
            }

            Kutuphane kui = new Kutuphane();

            Kullanici k = (Kullanici)Session["userSession"];

            kui.Kullanici_ID = k.ID;

            kui.Oyun_ID = (int)id;

            db.Kutuphanes.Add(kui);

            db.SaveChanges();

            return RedirectToAction("Index","Kutuphane");
        }
    }
}
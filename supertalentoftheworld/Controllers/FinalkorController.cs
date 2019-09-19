using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using supertalentoftheworld.Models;

namespace supertalentoftheworld.Controllers
{
    public class FinalkorController : Controller
    {
        SupertalentE db = new SupertalentE();

        // GET: Finalkor
        public ActionResult Attend_ko()
        {
            return View();
        }

        public ActionResult Index_ko()
        {
            return View();
        }

        public ActionResult Thal_ko()
        {
            return View();
        }

        public ActionResult Final_tal_ko()
        {
            var _list = db.Model_Data.Where(a => a.M_season == 13 & a.Category == 2).ToList();
            return View(_list);
        }

        public ActionResult Model_details_ko(int id)
        {
            var _sel = db.Model_Data.Find(id);
            var _images = db.Md_Image.Where(a => a.Md_id == id);

            ViewBag.images = _images;
            return View(_sel);
        }
    }
}
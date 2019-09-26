using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using supertalentoftheworld.Models;

namespace supertalentoftheworld.Controllers
{
    public class FinalistsController : Controller
    {
        SupertalentE db = new SupertalentE();

        // GET: Finalists
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index_ko()
        {
            return View();
        }

        public ActionResult Model_details(int id)
        {
            var _sel = db.Model_Data.Find(id);
            var _images = db.Md_Image.Where(a => a.Md_id == id);

            ViewBag.images = _images;
            return View(_sel);
        }

        public ActionResult Model_details_ko(int id)
        {
            var _sel = db.Model_Data.Find(id);
            var _images = db.Md_Image.Where(a => a.Md_id == id);



            ViewBag.images = _images;
            return View(_sel);
        }

        public ActionResult Models(int cate, int season)
        {
            var _list = db.Model_Data.Where(a => a.M_season == season && a.Category == cate).ToList().OrderBy(a => a.M_name);
            return View(_list);
        }

        public ActionResult Models_ko(int cate, int season)
        {
            var _list = db.Model_Data.Where(a => a.M_season == season && a.Category == cate).ToList().OrderBy(a => a.M_name);
            return View(_list);
        }


       

    }
}
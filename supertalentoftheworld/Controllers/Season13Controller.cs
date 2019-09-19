using supertalentoftheworld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace supertalentoftheworld.Controllers
{
    public class Season13Controller : Controller
    {
        SupertalentE db = new SupertalentE();


        // GET: Season13

        public ActionResult My_2019_19()
        {
            int id = 89;
            var _data = db.Travel_Goods.Find(id);
            var _courses = (from a in db.Courses where a.TG_id == id select a).ToList();
            var _images = (from a in db.TG_Image where a.TG_id == id select a).ToList();

            ViewBag.course = _courses;
            ViewBag.images = _images;

            return View(_data);
        }


        public ActionResult My_2019_15()
        {
            int id = 90;
            var _data = db.Travel_Goods.Find(id);
            var _courses = (from a in db.Courses where a.TG_id == id select a).ToList();

            var _images = (from a in db.TG_Image where a.TG_id == id select a).ToList();

            ViewBag.images = _images;
            ViewBag.course = _courses;

            return View(_data);
        }

        public ActionResult My_2019_12()
        {
            int id = 91;
            var _data = db.Travel_Goods.Find(id);
            var _courses = (from a in db.Courses where a.TG_id == id select a).ToList();

            var _images = (from a in db.TG_Image where a.TG_id == id select a).ToList();

            ViewBag.images = _images;
            ViewBag.course = _courses;

            return View(_data);
        }

        public ActionResult My_2019_8()
        {

            int id = 92;
            var _data = db.Travel_Goods.Find(id);
            var _courses = (from a in db.Courses where a.TG_id == id select a).ToList();

            var _images = (from a in db.TG_Image where a.TG_id == id select a).ToList();

            ViewBag.images = _images;
            ViewBag.course = _courses;

            return View(_data);
        }
    }
}
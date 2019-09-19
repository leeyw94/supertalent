using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using supertalentoftheworld.Models;

namespace supertalentoftheworld.Controllers
{
    public class LayoutController : Controller
    {
        SupertalentE db = new SupertalentE();

        // GET: Layout
        public ActionResult Header()
        {
            return View();
        }

        public ActionResult Footer()
        {

            string language =  "korea";
            try {
                language = Request.Cookies["language"].Value ?? "";
            }
            catch {


            }
            ViewBag.language = language;

            return View();
        }

        public ActionResult Header_ko()
        {
            return View();
        }

        public ActionResult Header_admin()
        {
            return View();
        }

        public ActionResult _Finalist_Menu()
        {
            return View();
        }
    }
}
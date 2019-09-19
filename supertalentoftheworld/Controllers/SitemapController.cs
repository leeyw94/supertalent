using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace supertalentoftheworld.Controllers
{
    public class SitemapController : Controller
    {
        // GET: Sitemap
        public ActionResult Cookie()
        {
            return View();
        }
        public ActionResult Privacy()
        {
            return View();
        }
        public ActionResult Terms()
        {
            return View();
        }
    }
}
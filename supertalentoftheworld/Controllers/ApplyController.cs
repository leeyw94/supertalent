using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using supertalentoftheworld.Models;


namespace supertalentoftheworld.Controllers
{
    public class ApplyController : Controller
    {
        private SupertalentE db = new SupertalentE();

        // GET: Apply
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Application()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Application(Application application)
        {
            var _insert = new Application()
            {
                lastname = application.lastname,
                firstname = application.firstname,
                country = application.country,
                city = application.city,
                job = application.job,
                language = application.language,
                hope = application.hope,
                phone = application.phone,
                facebook = application.facebook,
                insta = application.insta,
                youtube = application.youtube,
                b_y = application.b_y,
                b_m = application.b_m,
                b_d = application.b_d,
                height = application.height,
                rdate = DateTime.Now,
                category = application.category,
                email = application.email,
                talent = application.talent,
                age = application.age
            };

            db.Application.Add(_insert);
            db.SaveChanges();

            return Content("<html><script>alert('Send Your Application.'); window.top.location.href = '/Apply/Application';</script></html>");
        }

        public ActionResult Index_ko()
        {
            return View();
        }

        public ActionResult Application_ko()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Application_ko(Application_Ko application_Ko)
        {
            var _insert = new Application_Ko()
            {
                name = application_Ko.name,
                firstname = application_Ko.firstname,
                city = application_Ko.city,
                job = application_Ko.job,
                age = application_Ko.age,
                language = application_Ko.language,
                hope = application_Ko.hope,
                phone = application_Ko.phone,
                facebook = application_Ko.facebook,
                insta = application_Ko.insta,
                youtube = application_Ko.youtube,
                b_y = application_Ko.b_y,
                b_m = application_Ko.b_m,
                b_d = application_Ko.b_d,
                height = application_Ko.height,
                rdate = DateTime.Now,
                category = application_Ko.category
            };

            db.Application_Ko.Add(_insert);
            db.SaveChanges();

            return Content("<html><script>alert('지원이 완료되었습니다.'); window.top.location.href = '/Apply/Application_ko';</script></html>");
        }

    }
}
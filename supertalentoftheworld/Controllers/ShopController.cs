using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using supertalentoftheworld.Models;
using System.Web.Security;

namespace supertalentoftheworld.Controllers
{
    public class ShopController : Controller
    {
        SupertalentE db = new SupertalentE();
        db_e db2 = new db_e();
        // GET: Shop
        public ActionResult Index()
        {            

            return View();
        }


        public ActionResult hotel()
        {

            return View();
        }


        public ActionResult hotel_action(hotel_ask doc)
        {

           
            string msg = "호텔 단체 요금 견적이 접수되었습니다.";

            doc.write_date = DateTime.Now;
            doc.state = "접수";
            db2.hotel_ask.Add(doc);
            db2.SaveChanges(); // 실제로 저장 

            return
              Content("<html><script>alert('" + msg +
                      "'); window.top.location.href ='/Shop/producthotel_ko';</script></html>");
        }


        //[HttpGet]
        //[Route("pay_complete")]
        //public ActionResult pay_result(int id)
        //{
        //    var _data = db.Pay.Find(id);
        //    _data.Status = "Success";
        //    _data.Pay_date = DateTime.Now;
        //    db.SaveChanges();
        //    return View();
        //}


        //[HttpGet]
        //public ActionResult pay_ing(int id)
        //{
        //    var _data = db.Pay.Find(id);
        //    var _user_email = Request.Cookies["user_email"].Value;
        //    var _user_data = db.UserData.Find(_user_email);

        //    _data.Buyer_email = _user_data.user_email;
        //    _data.Buyer_phone = _user_data.user_phone;
        //    _data.Buyer_name = _user_data.user_name;

        //    db.SaveChanges();

        //    ViewBag.user_data = _user_data;

        //    return View(_data);
        //}

        public ActionResult Detailst(int id)
        {
            var _list = db.Travel_Goods.Find(id);
            return View(_list);
        }

        public ActionResult Product()
        {
            var _list = db.Travel_Goods.Where(a=>a.Category == 0).ToList();
            return View(_list);
        }

        public ActionResult Product_ko()
        {
            var _list = db.Travel_Goods.Where(a=>a.Category == 1).ToList();
            return View(_list);
        }

        //public ActionResult Detailst(int id)
        //{
        //    var _data = db.Travel_Goods.Find(id);
        //    var _course = db.Courses.Where(a => a.TG_id == id).ToList();
        //    var _image = db.TG_Image.Where(a => a.TG_id == id).ToList();

        //    Travel_Goods_D tgd = new Travel_Goods_D()
        //    {
        //        id = _data.id,
        //        Description = _data.Description,
        //        Title = _data.Title,
        //        //Course = _course.Course,
        //        Benefits = _data.Benefits,
        //        Includes = _data.Includes,
        //        Not_included = _data.Not_included,
        //        Notices_Policy = _data.Notices_Policy,
        //        Cancel_Refund = _data.Cancel_Refund,
        //        Sdate = _data.Sdate,
        //        Edate = _data.Edate,
        //        //ImagePath = _image.ImagePath,
        //        Price_d = _data.Price_d,
        //        Price_w = _data.Price_w
        //    };

        //    return View(tgd);         
        //}

        public ActionResult ProductHotel_ko()
        {

            return View();
        }
        public ActionResult Index2()
        {
            var _list = db.Travel_Goods.OrderByDescending(a => a.Rdate).ToList().AsEnumerable();
        

            return View(_list);
        }

        public ActionResult Details(int id)
        {
            var _data = db.Travel_Goods.Find(id);
            var _courses = (from a in db.Courses where a.TG_id == id select a).ToList();
            var _images = (from a in db.TG_Image where a.TG_id == id select a).ToList();

            ViewBag.course = _courses;
            ViewBag.images = _images;

            return View(_data);
        }


        public ActionResult Details_19(int id)
        {
            var _data = db.Travel_Goods.Find(id);
            var _courses = (from a in db.Courses where a.TG_id == id select a).ToList();
            var _images = (from a in db.TG_Image where a.TG_id == id select a).ToList();

            ViewBag.course = _courses;
            ViewBag.images = _images;

            return View(_data);
        }

        public ActionResult Details_ko(int id)
        {
            var _data = db.Travel_Goods.Find(id);
            var _courses = (from a in db.Courses where a.TG_id == id select a).ToList();
            var _images = (from a in db.TG_Image where a.TG_id == id select a).ToList();

            ViewBag.course = _courses;
            ViewBag.images = _images;

            return View(_data);
        }

        public ActionResult Support_ko()
        {
            var _list = db.Travel_Goods.OrderByDescending(a => a.Rdate).ToList().AsEnumerable();


            return View(_list);
        }

        public ActionResult Influencer()
        {
            return View();
        }

        public ActionResult Brochures()
        {
            return View();
        }

        public ActionResult Index_ko()
        {
            return View();
        }

        public ActionResult Brochures_ko()
        {
            return View();
        }

         
        //public ActionResult Details(int id)
        //{
        //    var _data = db.Travel_Goods.Find(id);
        //    var _course = db.Courses.Where(a => a.TG_id == id).ToList();
        //    var _image = db.TG_Image.Where(a => a.TG_id == id).ToList();

        //    Travel_Goods_D tgd = new Travel_Goods_D()
        //    {
        //        id = _data.id,
        //        Description = _data.Description,
        //        Title = _data.Title,
        //        //Course = _course.Course,
        //        Benefits = _data.Benefits,
        //        Includes = _data.Includes,
        //        Not_included = _data.Not_included,
        //        Notices_Policy = _data.Notices_Policy,
        //        Cancel_Refund = _data.Cancel_Refund,
        //        Sdate = _data.Sdate,
        //        Edate = _data.Edate,
        //        //ImagePath = _image.ImagePath,
        //        Price_d = _data.Price_d,
        //        Price_w = _data.Price_w
        //    };

        //    return View(tgd);

        //}
    }

    
   
}


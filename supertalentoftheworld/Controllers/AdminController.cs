using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using supertalentoftheworld.Models;
using supertalentoftheworld.Manager;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Configuration;


namespace supertalentoftheworld.Controllers
{
    public class AdminController : Controller
    {
        private SupertalentE db = new SupertalentE();


        public ActionResult payment()
        {
            return View();
        }

        public ActionResult Application_list_ko()
        {
            var _list = db.Application_Ko.ToList().OrderByDescending(a => a.rdate);

            return View(_list);
        }

        public ActionResult Product_Img_Setting(int id)
        {
            var _TG = db.Travel_Goods.Find(id);
            var _list = db.TG_Image.Where(a => a.TG_id == id).OrderByDescending(a => a.rdate);

            ViewBag.TG = _TG;

            return View(_list);
        }

        [HttpPost]
        public ActionResult Product_Img_Setting(TG_Image tG_Image)
        {
            var files = FileManager.FileUpload();

            if (files.Count() > 0)
            {
                var _insert = new TG_Image()
                {
                    TG_id = tG_Image.id,
                    ImagePath = "http://supertalent.theblueeye.com/Upload/" + files[0],
                    rdate = DateTime.Now,

                };

                db.TG_Image.Add(_insert);
                db.SaveChanges();
            };

            return Content("<html><script>alert('이미지가 추가 되었습니다.'); window.top.location.href = '/Admin/Product_Img_Setting?id=" + tG_Image.id + "';</script></html>");

        }

        public ActionResult Application_Ko_List()
        {
            var _list = db.Application_Ko.ToList().OrderByDescending(a => a.rdate);

            return View(_list);
        }

        public ActionResult Application_List()
        {
            var _list = db.Application.ToList().OrderByDescending(a => a.rdate);

            return View(_list);
        }

        public ActionResult MI_List(int id)
        {
            var _images = db.Md_Image.Where(a => a.Md_id == id).ToList();
            var _main = db.Model_Data.Find(id);
            ViewBag.main = _main;

            return View(_images);
        }


        public ActionResult MI_delete(int id)
        {
            var _image = db.Md_Image.Where(a => a.Md_id == id).FirstOrDefault();

            if (_image != null)
            {
                db.Md_Image.Remove(_image);
            }

            db.SaveChanges();

            return Content("<html><script>alert('이미지가 삭제 되었습니다.'); window.top.location.href = '/Admin/MI_list?id=" + id + "';</script></html>");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult MI_add(Md_Image Md_Image)
        {
            var files = FileManager.FileUpload2();

            if (files.Count() > 0)
            {
                var _insert = new Md_Image()
                {
                    Md_id = Md_Image.id,
                    ImagePath = "http://supertalent.theblueeye.com/Upload2/" + files[0],
                };

                db.Md_Image.Add(_insert);
                db.SaveChanges();
            };

            return Content("<html><script>alert('이미지가 추가 되었습니다.'); window.top.location.href = '/Admin/MI_list?id=" + Md_Image.id + "';</script></html>");

        }



        // GET: Admin
        //모델 수정 페이지
        public ActionResult Model_edit(int id)
        {
            var _sel = db.Model_Data.Find(id);

            return View(_sel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Model_edit(Model_Data model_Data)
        {

            var files = FileManager.FileUpload2();

            var _data = db.Model_Data.Find(model_Data.id);

            if (files.Count() > 0)
            {
                _data.Category = model_Data.Category;
                _data.M_season = model_Data.M_season;
                _data.M_name = model_Data.M_name;
                _data.M_age = model_Data.M_age;
                _data.M_country = model_Data.M_country;
                _data.M_city = model_Data.M_city;
                _data.M_height = model_Data.M_height;
                _data.M_language = model_Data.M_language;
                _data.M_facebook = model_Data.M_facebook;
                _data.M_insta = model_Data.M_insta;
                _data.M_youtube = model_Data.M_youtube;
                _data.MainImgPath = "http://supertalent.theblueeye.com/Upload/" + files[0];

                db.SaveChanges();

            }
            else
            {
                _data.Category = model_Data.Category;
                _data.M_season = model_Data.M_season;
                _data.M_name = model_Data.M_name;
                _data.M_age = model_Data.M_age;
                _data.M_country = model_Data.M_country;
                _data.M_city = model_Data.M_city;
                _data.M_height = model_Data.M_height;
                _data.M_language = model_Data.M_language;
                _data.M_facebook = model_Data.M_facebook;
                _data.M_insta = model_Data.M_insta;
                _data.M_youtube = model_Data.M_youtube;

                db.SaveChanges();
            }

            return Content("<html><script>alert('모델이 수정 되었습니다.'); window.top.location.href = '/Admin/Model_list';</script></html>");
        }





        //모델 삭제 페이지
        public ActionResult Model_delete(int id)
        {
            var _sel = db.Model_Data.Find(id);
            var _c = db.Md_Image.Where(a => a.Md_id == id).ToList();


            foreach (var item in _c)
            {
                var _i = db.Md_Image.Where(a => a.Md_id == item.id).ToList();
                db.Md_Image.Remove(item);
            }
            db.Model_Data.Remove(_sel);
            db.SaveChanges();

            return Content("<html><script>alert('모델이 삭제 되었습니다.'); window.top.location.href = '/Admin/Model_list';</script></html>");
        }



        //모델 리스트 페이지 
        public ActionResult Model_list()
        {
            var _list = db.Model_Data.ToList().OrderByDescending(a => a.M_season);
            return View(_list);
        }


        //모델 추가 페이지 
        public ActionResult Model_add()
        {
            return View();
        }

        //모델 추가 기능
        [HttpPost]
        public ActionResult Model_add(Model_Data model_Data)
        {
            var files = FileManager.FileUpload2();

            if (files.Count() > 0)
            {
                var _insert = new Model_Data()
                {
                    M_name = model_Data.M_name,
                    Category = model_Data.Category,
                    M_age = model_Data.M_age,
                    M_country = model_Data.M_country,
                    M_city = model_Data.M_city,
                    M_height = model_Data.M_height,
                    M_content = model_Data.M_content,
                    MainImgPath = "http://supertalent.theblueeye.com/Upload2/" + files[0],
                    M_facebook = model_Data.M_facebook,
                    M_youtube = model_Data.M_youtube,
                    M_insta = model_Data.M_insta,
                    M_country_img = "http://supertalent.theblueeye.com/Upload2/country_img/" + model_Data.M_country_img + ".png",
                    M_language = model_Data.M_language,
                    M_season = model_Data.M_season,
                };
                db.Model_Data.Add(_insert);
                db.SaveChanges();

                return Content("<html><script>alert('모델 리스트 페이지로 이동합니다.'); window.top.location.href = '/Admin/Model_list';</script></html>");
            }
            else
            {
                var _insert2 = new Model_Data()
                {
                    M_name = model_Data.M_name,
                    Category = model_Data.Category,
                    M_age = model_Data.M_age,
                    M_country = model_Data.M_country,
                    M_city = model_Data.M_city,
                    M_height = model_Data.M_height,
                    M_content = model_Data.M_content,
                    M_facebook = model_Data.M_facebook,
                    M_youtube = model_Data.M_youtube,
                    M_insta = model_Data.M_insta,
                    M_country_img = "http://supertalent.theblueeye.com/Upload2/country_img" + model_Data.M_country_img,
                    M_language = model_Data.M_language,
                    M_season = model_Data.M_season,
                };
                db.Model_Data.Add(_insert2);
                db.SaveChanges();

                return Content("<html><script>alert('모델 리스트 페이지로 이동합니다.'); window.top.location.href = '/Admin/Model_list';</script></html>");
            }
        }


        public ActionResult Product_list()
        {
            var _list = db.Travel_Goods.ToList().OrderByDescending(a => a.Rdate);

            return View(_list);
        }


        public ActionResult Course_setting_list()
        {
            var _list = db.Get_Course_Content.ToList();

            return View(_list);
        }


        public ActionResult Course_setting_edit(int id)
        {
            var _data = db.Get_Course_Content.Find(id);

            return View(_data);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Course_setting_edit(Get_Course_Content get_Course_Content)
        {
            var _data = db.Get_Course_Content.Find(get_Course_Content.id);

            _data.Title = get_Course_Content.Title;
            _data.Get_content = get_Course_Content.Get_content;

            db.SaveChanges();

            return Content("<html><script>alert('코스 세팅이 수정 되었습니다.'); window.top.location.href = '/Admin/Course_setting_list';</script></html>");
        }



        public ActionResult Course_setting_add()
        {
            return View();
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Course_setting_add(Get_Course_Content get_Course_Content)
        {
            var _insert = new Get_Course_Content()
            {
                Title = get_Course_Content.Title,
                Get_content = get_Course_Content.Get_content,
                Writer = get_Course_Content.Writer,
                Rdate = DateTime.Now
            };

            db.Get_Course_Content.Add(_insert);
            db.SaveChanges();

            return View();
        }

        public ActionResult Course_list(int id)
        {
            var _data = db.Travel_Goods.Find(id);

            var _courses = (from a in db.Courses where a.TG_id == id select a).ToList();
            var _setting = db.Get_Course_Content.ToList().OrderByDescending(a => a.Num);


            ViewBag.course = _courses;
            ViewBag.setting = _setting;

            return View(_data);
        }

        public ActionResult Course_edit(int id, int c_id)
        {
            var _data = db.Courses.Find(c_id);
            var _TG = db.Travel_Goods.Find(id);

            ViewBag.TG = _TG;

            return View(_data);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Course_edit(Courses courses)
        {
            //var files = FileManager.FileUpload();
            var _data = db.Courses.Find(courses.id);
            //var _image = db.TG_Image.Where(a => a.courses_id == courses.id).FirstOrDefault();
            var id = db.Travel_Goods.Where(a => a.id == _data.TG_id).FirstOrDefault().id;

            _data.Course = courses.Course;
            _data.Course_title = courses.Course_title;

            //if (files.Count() > 0)
            //{
            //    _data.Course = courses.Course;
            //    _image.ImagePath = "http://supertalent.theblueeye.com/Upload/" + files[0];
            //    _data.Course_title = courses.Course_title;
            //}else
            //{
            //    _data.Course = courses.Course;
            //    _data.Course_title = courses.Course_title;
            //}

            db.SaveChanges();

            return Content("<html><script>alert('코스가 수정 되었습니다.'); window.top.location.href = '/Admin/Course_list?id=" + id + "';</script></html>");
        }


        public ActionResult Course_delete(int id, int c_id)
        {
            //var _image = db.TG_Image.Where(a => a.courses_id == c_id).FirstOrDefault();
            var _data = db.Courses.Find(c_id);

            //if(_image != null)
            //{
            //    db.TG_Image.Remove(_image);
            //}

            db.Courses.Remove(_data);

            db.SaveChanges();

            return Content("<html><script>alert('코스가 삭제 되었습니다.'); window.top.location.href = '/Admin/Course_list?id=" + id + "';</script></html>");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Course_add(Courses courses)
        {
            //var files = FileManager.FileUpload();

            //if (files.Count() > 0)
            //{
            var _insert = new Courses()
            {
                TG_id = courses.id,
                Course = courses.Course,
                Course_title = courses.Course_title
            };

            var _result = db.Courses.Add(_insert);
            db.SaveChanges();

            //var _insert2 = new TG_Image()
            //{
            //    courses_id = _result.id,
            //    ImagePath = "http://supertalent.theblueeye.com/Upload/" + files[0],
            //    Image_Cate = 1                   
            //};

            //db.TG_Image.Add(_insert2);
            //db.SaveChanges();

            return Content("<html><script>alert('코스가 추가 되었습니다.'); window.top.location.href = '/Admin/Course_list?id=" + courses.id + "';</script></html>");
        }
        //else
        //{
        //    var _insert = new Courses()
        //    {
        //        TG_id = courses.id,
        //        Course = courses.Course,
        //        Course_title = courses.Course_title
        //    };
        //    db.Courses.Add(_insert);
        //    db.SaveChanges();
        //}


        //    return Content("<html><script>alert('코스가 추가 되었습니다.'); window.top.location.href = '/Admin/Course_list?id="+courses.id+"';</script></html>");
        //}


        public ActionResult Product_add()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Product_add(Travel_Goods travel_Goods)
        {
            var files = FileManager.FileUpload();

            //string Sdate = "" + travel_Goods.Sdate;
            //string sd = Sdate.Substring(0,10);

            //string Edate = "" + travel_Goods.Edate;
            //string ed = Edate.Substring(0,10);
            if (files.Count() > 0)
            {
                var _insert = new Travel_Goods()
                {
                    Title = travel_Goods.Title,
                    Category = travel_Goods.Category,
                    Sdate = travel_Goods.Sdate,
                    Edate = travel_Goods.Edate,
                    Rdate = DateTime.Now,
                    Price_d = travel_Goods.Price_d,
                    Price_w = travel_Goods.Price_w,
                    MainImgPath = "http://supertalent.theblueeye.com/Upload/" + files[0],
                    Description = travel_Goods.Description,
                    Benefits = travel_Goods.Benefits,
                    Includes = travel_Goods.Includes,
                    Not_included = travel_Goods.Not_included,
                    Notices_Policy = travel_Goods.Notices_Policy,
                    Cancel_Refund = travel_Goods.Cancel_Refund,
                    City = travel_Goods.City,
                    Country = travel_Goods.Country,
                    Startc = travel_Goods.Startc,
                    Endc = travel_Goods.Endc,
                    days = travel_Goods.days,
                    code = travel_Goods.code,
                    Notice = travel_Goods.Notice,
                    Reservation = travel_Goods.Reservation,
                    Accommodations = travel_Goods.Accommodations,
                    Cancel_Notice = travel_Goods.Cancel_Notice,
                    Seller_Info = travel_Goods.Seller_Info

                };

                db.Travel_Goods.Add(_insert);
                db.SaveChanges();

                return Content("<html><script>alert('상품 리스트 페이지로 이동합니다.'); window.top.location.href = '/Admin/Product_list';</script></html>");

            }
            else
            {
                var _insert = new Travel_Goods()
                {
                    Title = travel_Goods.Title,
                    Category = travel_Goods.Category,
                    Sdate = travel_Goods.Sdate,
                    Edate = travel_Goods.Edate,
                    Rdate = DateTime.Now,
                    Price_d = travel_Goods.Price_d,
                    Price_w = travel_Goods.Price_w,
                    Description = travel_Goods.Description,
                    Benefits = travel_Goods.Benefits,
                    Includes = travel_Goods.Includes,
                    Not_included = travel_Goods.Not_included,
                    Notices_Policy = travel_Goods.Notices_Policy,
                    Cancel_Refund = travel_Goods.Cancel_Refund,
                    City = travel_Goods.City,
                    Country = travel_Goods.Country,
                    Startc = travel_Goods.Startc,
                    Endc = travel_Goods.Endc,
                    days = travel_Goods.days,
                    code = travel_Goods.code,
                    Notice = travel_Goods.Notice,
                    Reservation = travel_Goods.Reservation,
                    Accommodations = travel_Goods.Accommodations,
                    Cancel_Notice = travel_Goods.Cancel_Notice,
                    Seller_Info = travel_Goods.Seller_Info

                };

                db.Travel_Goods.Add(_insert);
                db.SaveChanges();
            }
            //var _image = new TG_Image()
            //{
            //    TG_id = _result.id,
            //    Image_Cate = 0,
            //    ImagePath = "http://supertalent.theblueeye.com/Upload/" +"main"+ files[0]
            //};

            //db.TG_Image.Add(_image);
            //db.SaveChanges();


            //return Content("<html><script>alert('상품 리스트 페이지로 이동합니다.'); window.top.location.href = '/Admin/Product_add_detail?id=" + _result.id + "';</script></html>");
            return Content("<html><script>alert('상품 리스트 페이지로 이동합니다.'); window.top.location.href = '/Admin/Product_list';</script></html>");
        }


        public ActionResult Product_edit(int id)
        {
            var _sel = db.Travel_Goods.Find(id);

            return View(_sel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Product_edit(Travel_Goods travel_Goods)
        {

            var files = FileManager.FileUpload();

            var _data = db.Travel_Goods.Find(travel_Goods.id);

            if (files.Count() > 0)
            {

                _data.Title = travel_Goods.Title;
                _data.Description = travel_Goods.Description;
                _data.Category = travel_Goods.Category;
                _data.Includes = travel_Goods.Includes;
                _data.Not_included = travel_Goods.Not_included;
                _data.Cancel_Refund = travel_Goods.Cancel_Refund;
                _data.Benefits = travel_Goods.Benefits;
                _data.Sdate = travel_Goods.Sdate;
                _data.Edate = travel_Goods.Edate;
                _data.Price_d = travel_Goods.Price_d;
                _data.Price_w = travel_Goods.Price_w;
                _data.MainImgPath = "http://supertalent.theblueeye.com/Upload/" + files[0];
                _data.Usable = travel_Goods.Usable;
                _data.Writer = travel_Goods.Writer;
                _data.Notices_Policy = travel_Goods.Notices_Policy;
                _data.City = travel_Goods.City;
                _data.Country = travel_Goods.Country;
                _data.Startc = travel_Goods.Startc;
                _data.Endc = travel_Goods.Endc;
                _data.days = travel_Goods.days;
                _data.code = travel_Goods.code;
                _data.Notice = travel_Goods.Notice;
                _data.Reservation = travel_Goods.Reservation;
                _data.Accommodations = travel_Goods.Accommodations;
                _data.Cancel_Notice = travel_Goods.Cancel_Notice;
                _data.Seller_Info = travel_Goods.Seller_Info;

                db.SaveChanges();

            }
            else
            {
                _data.Title = travel_Goods.Title;
                _data.Description = travel_Goods.Description;
                _data.Category = travel_Goods.Category;
                _data.Includes = travel_Goods.Includes;
                _data.Not_included = travel_Goods.Not_included;
                _data.Cancel_Refund = travel_Goods.Cancel_Refund;
                _data.Benefits = travel_Goods.Benefits;
                _data.Sdate = travel_Goods.Sdate;
                _data.Edate = travel_Goods.Edate;
                _data.Price_d = travel_Goods.Price_d;
                _data.Price_w = travel_Goods.Price_w;
                _data.Usable = travel_Goods.Usable;
                _data.Writer = travel_Goods.Writer;
                _data.Notices_Policy = travel_Goods.Notices_Policy;
                _data.City = travel_Goods.City;
                _data.Country = travel_Goods.Country;
                _data.Startc = travel_Goods.Startc;
                _data.Endc = travel_Goods.Endc;
                _data.days = travel_Goods.days;
                _data.code = travel_Goods.code;
                _data.Notice = travel_Goods.Notice;
                _data.Reservation = travel_Goods.Reservation;
                _data.Accommodations = travel_Goods.Accommodations;
                _data.Cancel_Notice = travel_Goods.Cancel_Notice;
                _data.Seller_Info = travel_Goods.Seller_Info;
                db.SaveChanges();
            }

            return Content("<html><script>alert('상품이 수정 되었습니다.'); window.top.location.href = '/Admin/Product_list';</script></html>");
        }


        public ActionResult Product_delete(int id)
        {
            var _sel = db.Travel_Goods.Find(id);
            var _c = db.TG_Image.Where(a => a.TG_id == id).ToList();


            foreach (var item in _c)
            {
                db.TG_Image.Remove(item);
            }
            db.Travel_Goods.Remove(_sel);
            db.SaveChanges();

            return Content("<html><script>alert('상품이 삭제 되었습니다.'); window.top.location.href = '/Admin/Product_list';</script></html>");
        }
    }


    //[HttpPost]
    //[ValidateInput(false)]
    //public ActionResult Product_add_detail(Travel_Goods_Data travel_Goods_Data)
    //{
    //    var files = FileManager.FileUpload2();
    //    String[] a = Request.Form.GetValues("Course");
    //    int b = a.Length;
    //    int c = Request["course"].Count();

    //    if (files.Count() > 0)
    //    {
    //        Travel_Goods travel_Goods = db.Travel_Goods.Find(travel_Goods_Data.id);

    //        travel_Goods.Description = travel_Goods_Data.Description;
    //        travel_Goods.Benefits = travel_Goods_Data.Benefits;
    //        travel_Goods.Includes = travel_Goods_Data.Includes;
    //        travel_Goods.Not_included = travel_Goods_Data.Not_included;
    //        travel_Goods.Notices_Policy = travel_Goods_Data.Notices_Policy;
    //        travel_Goods.Cancel_Refund = travel_Goods_Data.Cancel_Refund;

    //        db.SaveChanges();


    //        for (var i = 0; b > i; i++)
    //        {
    //            var _course = new Courses()
    //            {
    //                TG_id = travel_Goods_Data.id,
    //                Course = a[i],
    //                Num = i
    //            };
    //            db.Courses.Add(_course);
    //            db.SaveChanges();

    //        };

    //        for(var i =0; files.Count() > i; i++) { 
    //        var _image = new TG_Image()
    //        {
    //            TG_id = travel_Goods_Data.id,
    //            ImagePath = "http://supertalent.theblueeye.com/Upload/"+ "c" + files[i]
    //        };
    //        db.TG_Image.Add(_image);
    //        db.SaveChanges();
    //        }

    //        return View();

    //    }
    //    else

    //    {               
    //        Travel_Goods travel_Goods = db.Travel_Goods.Find(travel_Goods_Data.id);

    //        travel_Goods.Description = travel_Goods_Data.Description;
    //        travel_Goods.Benefits = travel_Goods_Data.Benefits;
    //        travel_Goods.Includes = travel_Goods_Data.Includes;
    //        travel_Goods.Not_included = travel_Goods_Data.Not_included;
    //        travel_Goods.Notices_Policy = travel_Goods_Data.Notices_Policy;
    //        travel_Goods.Cancel_Refund = travel_Goods_Data.Cancel_Refund;

    //        for (var i = 0; b > i; i++)
    //        {
    //            var _course = new Courses()
    //            {
    //                TG_id = travel_Goods_Data.id,
    //                Course = a[i],

    //            };
    //            db.Courses.Add(_course);
    //            db.SaveChanges();

    //        };

    //        return View();
    //    }


    //}



    //}

    //public class Travel_Goods_Data
    //{
    //    public int id { get; set; }
    //    public int TG_id { get; set; }
    //    public string Title { get; set; }    
    //    public string Description { get; set; }
    //    public string Course { get; set; }
    //    public string Benefits { get; set; }
    //    public string Includes { get; set; }
    //    public string Not_included { get; set; }
    //    public string Notices_Policy { get; set; }
    //    public string Cancel_Refund { get; set; }
    //    public string Sdate { get; set; }
    //    public string Edate { get; set; }
    //    public DateTime Rdate { get; set; }     
    //    public string ImagePath { get; set; }
    //    public int Image_Cate { get; set; }
    //    public int Num { get; set; }
    //    public string Content { get; set; }
    //    public HttpPostedFileBase ImageFile { get; set; }
    //    public string Price_d { get; set; }
    //    public string Price_w { get; set; }
    //}

}
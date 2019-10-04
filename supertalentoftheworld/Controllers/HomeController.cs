using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using supertalentoftheworld.Models;
using supertalentoftheworld.Manager;
using System.Web.Security;
using System.Net.Mail;

namespace supertalentoftheworld.Controllers
{
    public class HomeController : Controller
    {
        private SupertalentE db = new SupertalentE();

        private db_e db_e = new db_e();

        private bool EmailAuth = true;

        //[OutputCache(Duration = 600, VaryByParam = "none")]
        public ActionResult Index()
        {
            var _list = db.Travel_Goods.Where(a => a.Category == 0).ToList();

            // 공지사항 2  , 언론보도 3, 공식일정 6, Notice 7 , News 9, Event 10
            var _list1 = (from a in db_e.Board where a.BD_BM_idx == 7 && a.BD_useable == 1 select a).OrderByDescending(p => p.BD_wdate).Take(4).ToList();
            var _list2 = (from a in db_e.Board where a.BD_BM_idx == 9 && a.BD_useable == 1 select a).OrderByDescending(p => p.BD_wdate).Take(4).ToList();
            var _list3 = (from a in db_e.Board where a.BD_BM_idx == 10 && a.BD_useable == 1 select a).OrderByDescending(p => p.BD_wdate).Take(4).ToList();

            ViewBag.Notice = _list1;
            ViewBag.News = _list2;
            ViewBag.Event = _list3;

            #region 언어선택
            //언어 추가




            if (Request.Cookies["language"] != null)
            {
                Response.Cookies["language"].Expires = DateTime.Today.AddDays(-1);

                var language = new HttpCookie("language");
                language.Value = "english";
                Response.Cookies.Add(language);
            }
            else
            {
                var language = new HttpCookie("language");
                language.Value = "english";
                Response.Cookies.Add(language);


            }









            #endregion


            return View(_list);

        }
       

        public ActionResult Season_13_ko()
        {
            return View();
        }

        public ActionResult Season_13()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(Contact contact)
        {


            var _insert = new Contact()
            {
                Name = contact.Name,
                Email = contact.Email,
                Phone = contact.Phone,
                Content = contact.Content,
                Inquiry = contact.Inquiry,
                Company_name = contact.Company_name,
                gubun_code = contact.gubun_code,
                write_date = DateTime.Now

            };

            db.Contact.Add(_insert);
            db.SaveChanges();

            if (contact.Inquiry == "korea") {

                return Content("<html><script>alert('Your message has been sent.'); window.top.location.href = '/Home/Contact_ko';</script></html>");
            }
            else {
                return Content("<html><script>alert('Your message has been sent.'); window.top.location.href = '/Home/Contact';</script></html>");
            }


           
        }

        [HttpPost]
        public ActionResult Contact2(Contact contact)
        {
            var _insert = new Contact()
            {
                Name = contact.Name,
                Email = contact.Email,
                Phone = contact.Phone,
                Content = contact.Content,
                Inquiry = "english",
                Company_name = contact.Company_name,
                gubun_code = contact.gubun_code,
                write_date = DateTime.Now
               
                
            };

            db.Contact.Add(_insert);
            db.SaveChanges();
            return Content("<html><script>alert('Your message has been sent.'); window.top.location.href = '/Shop/Support_ko';</script></html>");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            //회사별 데이터 드롭다운==============================================================================================================================================          
            var category =
                db.code_1.Where(a => a.use_yn == "Y").OrderBy(a => a.index_order).Select(a => new { 값 = a.idx, 이름 = a.code_name });
            ViewBag.category = new SelectList(category.AsEnumerable(), "값", "이름");
            //==================================================================================================================================================================


            return View();
        }
        //[OutputCache(Duration = 600, VaryByParam = "none")]
        public ActionResult Index_ko()
        {
            var _list = db.Travel_Goods.Where(a => a.Category == 1).ToList();


            // 공지사항 2  , 언론보도 3, 공식일정 6, Notice 7 , News 9, Event 10

            var _list1 = (from a in db_e.Board where a.BD_BM_idx == 2 && a.BD_useable == 1 select a).OrderByDescending(p => p.BD_wdate).Take(4).ToList();
            var _list2 = (from a in db_e.Board where a.BD_BM_idx == 3 && a.BD_useable == 1 select a).OrderByDescending(p => p.BD_wdate).Take(4).ToList();
            var _list3 = (from a in db_e.Board where a.BD_BM_idx == 6 && a.BD_useable == 1 select a).OrderByDescending(p => p.BD_wdate).Take(4).ToList();

            ViewBag.공지사항 = _list1;
            ViewBag.언론보도 = _list2;
            ViewBag.공식일정 = _list3;


            #region 언어선택
            //언어 추가



           
            if (Request.Cookies["language"] != null)
            {
                Response.Cookies["language"].Expires = DateTime.Today.AddDays(-1);

                var language = new HttpCookie("language");
                language.Value = "korea";
                Response.Cookies.Add(language);
            }
            else
            {
                var language = new HttpCookie("language");
                language.Value = "korea";
                Response.Cookies.Add(language);


            }









            #endregion


            return View(_list);          
        }
        public ActionResult About_ko()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact_ko()
        {
            //회사별 데이터 드롭다운==============================================================================================================================================          
            var category =
                db.code_1.Where(a => a.use_yn =="Y").OrderBy(a=> a.index_order).Select( a => new {값 = a.idx, 이름= a.code_name_ko });
            ViewBag.category = new SelectList(category.AsEnumerable(), "값", "이름");
            //=====================================================================================================================================================================
        


            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Login_ko()
        {
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        public ActionResult SignUp_ko()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(UserData login_user, string remember)
        {
           

            if (!string.IsNullOrEmpty(login_user.user_email) && !string.IsNullOrEmpty(login_user.user_password) && login_user != null)
            {
                var user = db.UserData.Find(login_user.user_email);

                //이번엔 사용자가 있어야됨
                if (user != null)
                {
                    //비밀번호 확인
                    if (user.user_password.Equals(CryptoManager.GetMd5Hash(login_user.user_password)))
                    {

                        if (!user.verified)
                        {
                            //이메일 미인증
                            return Content("<html><script>alert('Please verify your email.'); window.top.location.href = '/Home/ConfirmEmail?email="+login_user.user_email+"';</script></html>");

                        }
                        else {                                    

                        ////인증쿠키 추가
                        FormsAuthentication.SetAuthCookie(user.user_email, false);
                        Response.Cookies["user_email"].Value = user.user_email;
                        Response.Cookies["user_level"].Value = Convert.ToString(user.user_level);

                            return Redirect("/Home/Index");

                        }
                    }
                    else
                    {
                        //비밀번호 틀림
                        ViewBag.Error = "Please check your password.";
                    }
                }
                else
                {
                    //사용자 없음
                    ViewBag.Error = "The user name is not registered.";
                }

            }
            else
            {
                //리퀘스트 값이 없음
                ViewBag.Error = "Please enter your username and password.";
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login_ko(UserData login_user, string remember)
        {


            if (!string.IsNullOrEmpty(login_user.user_email) && !string.IsNullOrEmpty(login_user.user_password) && login_user != null)
            {
                var user = db.UserData.Find(login_user.user_email);

                //이번엔 사용자가 있어야됨
                if (user != null)
                {
                    //비밀번호 확인
                    if (user.user_password.Equals(CryptoManager.GetMd5Hash(login_user.user_password)))
                    {

                        if (!user.verified)
                        {
                            //이메일 미인증
                            return Content("<html><script>alert('이메일 인증을 해주세요.'); window.top.location.href = '/Home/ConfirmEmail?email=" + login_user.user_email + "';</script></html>");

                        }
                        else
                        {

                            ////인증쿠키 추가
                            FormsAuthentication.SetAuthCookie(user.user_email, false);
                            Response.Cookies["user_email"].Value = user.user_email;
                            Response.Cookies["user_level"].Value = Convert.ToString(user.user_level);

                            return Redirect("/Home/Index_ko");

                        }
                    }
                    else
                    {
                        //비밀번호 틀림
                        ViewBag.Error = "비밀번호를 확인해주세요";
                    }
                }
                else
                {
                    //사용자 없음
                    ViewBag.Error = "등록되지 않은 아이디 입니다.";
                }

            }
            else
            {
                //리퀘스트 값이 없음
                ViewBag.Error = "아이디와 비밀번호를 입력해주세요";
            }

            return View();
        }


        [HttpPost]
        public ActionResult SignUp(UserData user)
        {

            if (!string.IsNullOrEmpty(user.user_email) && !string.IsNullOrEmpty(user.user_password) && user != null)
            {

                var db_user = db.UserData.Find(user.user_email);

                //존재하는 사용자인지 확인
                if (db_user == null)
                {

                    //db에 추가하고, 인증키 만들어둔다.

                    user.temp_confirm_key = CryptoManager.GetUniqeString(15).Replace("/", "").Replace("+", "");

                    user.user_password = CryptoManager.GetMd5Hash(user.user_password);

                    user.rdate = DateTime.Now;

                    user.use_yn = "Y";

                    db.UserData.Add(user);

                    db.SaveChanges();

                    //이메일 인증 분기점
                    if (EmailAuth)
                    {
                        //이메일도 보낸다.
                        string body = "Click an address to verify your email.";
                        body += "http://www.supertalent.co/Home/Verification?key=" + user.temp_confirm_key;

                        MailMessage msg = new MailMessage("yyyoungh724@gmail.com", user.user_email, "SuperTalent of The World authentication mail.", body);

                        MailManager.SendGmail(msg);
                        ViewBag.email = user.user_email;
                        return Redirect("/Home/ConfirmEmail?email=" + user.user_email);
                    }
                    else
                    {
                        return Redirect("/Home/Login");
                    }

                }
                else
                {
                    ViewBag.Error = "User does not exist.";
                }

            }
            else
            {
                ViewBag.Error = "Email, password, and name are required values.";
            }



            return Redirect("/Home/Login");
        }

        [HttpPost]
        public ActionResult SignUp_ko(UserData user)
        {

            if (!string.IsNullOrEmpty(user.user_email) && !string.IsNullOrEmpty(user.user_password) && user != null)
            {

                var db_user = db.UserData.Find(user.user_email);

                //존재하는 사용자인지 확인
                if (db_user == null)
                {

                    //db에 추가하고, 인증키 만들어둔다.

                    user.temp_confirm_key = CryptoManager.GetUniqeString(15).Replace("/", "").Replace("+", "");

                    user.user_password = CryptoManager.GetMd5Hash(user.user_password);

                    user.rdate = DateTime.Now;

                    user.use_yn = "Y";

                    db.UserData.Add(user);

                    db.SaveChanges();

                    //이메일 인증 분기점
                    if (EmailAuth)
                    {
                        //이메일도 보낸다.
                        string body = "다음의 주소를 클릭하여 이메일을 인증하세요";
                        body += "http://www.supertalent.co/Home/Verification?key=" + user.temp_confirm_key;

                        MailMessage msg = new MailMessage("yyyoungh724@gmail.com", user.user_email, "SuperTalent of The World 인증 메일입니다", body);

                        MailManager.SendGmail(msg);
                        ViewBag.email = user.user_email;
                        return Redirect("/Home/ConfirmEmail?email=" + user.user_email);
                    }
                    else
                    {
                        return Redirect("/Home/Login_ko");
                    }

                }
                else
                {
                    ViewBag.Error = "존재하지 않는 사용자입니다.";
                }

            }
            else
            {
                ViewBag.Error = "이메일과 비밀번호, 이름은 필수 값입니다.";
            }



            return Redirect("/Home/Login_ko");
        }

        public ActionResult ConfirmEmail(string email)
        {

            ViewBag.Email = email;
            return View();
        }

        public ActionResult Verification(string key)
        {
            var data = db.UserData.Where(a => a.temp_confirm_key == key);

            if (data.Any())
            {
                if (data.Count() == 1)
                {
                    var user = data.First();

                    user.verified = true;
                    user.temp_confirm_key = null;                  
                    db.SaveChanges();

                    return Redirect("/Home/Login");

                }
            }

            return Redirect("/Home/Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            if (HttpContext != null)
            {
                int cookieCount = HttpContext.Request.Cookies.Count;
                for (var i = 0; i < cookieCount; i++)
                {
                    var cookie = HttpContext.Request.Cookies[i];
                    if (cookie != null)
                    {
                        var expiredCookie = new HttpCookie(cookie.Name)
                        {
                            Expires = DateTime.Now.AddDays(-1),
                            Domain = cookie.Domain
                        };
                        HttpContext.Response.Cookies.Add(expiredCookie); // overwrite it
                    }
                }

                // clear cookies server side
                HttpContext.Request.Cookies.Clear();
            }

            return Redirect("/Home/Login");
        }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using supertalentoftheworld.Models;
using System.Web.Security;

namespace supertalentoftheworld.Controllers
{
    public class PaymentController : Controller
    {
        SupertalentE db = new SupertalentE();
        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult pay_result(int imp_uid, int merchant_uid)
        {
            var _data = db.Pay.Where(a => a.imp_uid == imp_uid && a.merchant_uid == merchant_uid).FirstOrDefault();    
            return View(_data);
        }

        [HttpPost]     
        public ActionResult pay_result(Pay pay)
        {
            
            var _data = db.Pay.Find(pay.id);
            _data.Status = pay.Status;
            _data.imp_uid = pay.imp_uid;
            _data.merchant_uid = pay.merchant_uid;
            
            db.SaveChanges();
            return View();
        }

        [HttpGet]
        public ActionResult Pay_BankTransfer_ko(int id)
        {
            var _data = db.Pay.Find(id);
            var _user_email = Request.Cookies["user_email"].Value;
            var _user_data = db.UserData.Find(_user_email);
            var _product = db.Travel_Goods.Find(_data.TG_id);

            _data.Buyer_email = _user_data.user_email;
            _data.Buyer_phone = _user_data.user_phone;
            _data.Buyer_name = _user_data.user_name;

            db.SaveChanges();

            ViewBag.user_data = _user_data;
            ViewBag.p_data = _product;

            return View(_data);
        }

        [HttpGet]
        public ActionResult Pay_Paypal(int id)
        {
            var _data = db.Pay.Find(id);
            var _user_email = Request.Cookies["user_email"].Value;
            var _user_data = db.UserData.Find(_user_email);

            _data.Buyer_email = _user_data.user_email;
            _data.Buyer_phone = _user_data.user_phone;
            _data.Buyer_name = _user_data.user_name;

            db.SaveChanges();

            ViewBag.user_data = _user_data;

            return View(_data);
        }

        [HttpPost]
        public ActionResult Pay_BankTransfer_ko(Pay pay)
        {
            var _data = db.Pay.Find(pay.id);

            _data.TG_name = pay.TG_name;
            _data.Cart_date = DateTime.Now;
            _data.Buyer_account = pay.Buyer_account;
            _data.Currency = "KRW";       

            db.SaveChanges();

            return Content("<html><script>alert('주문이 정보가 입력되었습니다.'); window.top.location.href = '/Payment/Order_complete_ko?id="+pay.id+"'</script></html>");
        }

        public ActionResult Order_complete_ko(int id)
        {
            var _data = db.Pay.Find(id);

            return View(_data);
        }
    }
}
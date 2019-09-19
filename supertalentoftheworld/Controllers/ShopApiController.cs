using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using supertalentoftheworld.Models;

namespace supertalentoftheworld.Controllers
{
    [RoutePrefix("api/shop")]
    public class ShopApiController : ApiController
    {
        private SupertalentE db = new SupertalentE();

        [HttpPost]
        [Route("pay_ing")]
        public IHttpActionResult Payment_Info(Pay payment)
        {

            if (User.Identity.IsAuthenticated == true)
            {

                var _insert = new Pay
                {
                    Amount = payment.Amount,
                    Total_Price = payment.Total_Price,
                    TG_id = payment.TG_id,
                    Price = payment.Price,
                    Pay_method = payment.Pay_method,
                    Status = "Unpaid",
                    Cart_date = DateTime.Now
                };

                var a = db.Pay.Add(_insert);
                db.SaveChanges();

                return Ok(a.id);
            }
            else
            {
                return Ok("");
                //return Ok("<html><script>alert('Please use after Login.'); window.top.location.href = '/Home/Login';</script></html>");
            }
        }

        [HttpGet]
        [Route("pay_complete")]
        public IHttpActionResult pay_complete(int id)
        {
            var _data = db.Pay.Find(id);

            _data.Status = "Success";
            _data.Pay_date = DateTime.Now;
            db.SaveChanges();
            return Ok();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using supertalentoftheworld.Models;

namespace supertalentoftheworld.Controllers
{
    [RoutePrefix("api/payment")]
    public class PaymentApiController : ApiController
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
                    Status = "Unpaid"                    
                };

                var a = db.Pay.Add(_insert);
                db.SaveChanges();

                return Ok(a.id);
            }
            else
            {
                return Ok("");                
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

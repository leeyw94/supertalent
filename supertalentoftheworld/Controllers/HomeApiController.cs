using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using supertalentoftheworld.Models;

namespace supertalentoftheworld.Controllers
{
    [RoutePrefix("api/home")]
    public class HomeApiController : ApiController
    {
        private SupertalentE db = new SupertalentE();

        [HttpPost]
        [Route("user/confirm")]
        public IHttpActionResult ConfirmEmail(UserData user)
        {
            if (!string.IsNullOrEmpty(user.user_email) && user != null)
            {

                var user_data = db.UserData.Find(user.user_email);

                //없으면 false, 있으면 true
                return Ok(user_data != null);
            }
            else
            {
                return BadRequest("No Request Data");
            }



        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using supertalentoftheworld.Models;

namespace supertalentoftheworld.Controllers
{
    [RoutePrefix("api/Admin")]
    public class AdminApiController : ApiController
    {
        SupertalentE db = new SupertalentE();

        [HttpGet]
        [Route("GetCourseContent")]
        public IHttpActionResult GetCourseContent(int id)
        {
            var _sel = db.Get_Course_Content.Find(id);
            
            return Ok(_sel);
        }
    }
}

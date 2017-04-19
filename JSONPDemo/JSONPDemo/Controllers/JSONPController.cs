using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JSONPDemo.Controllers
{

    public class JSONPController : ApiController
    {
        public HttpResponseMessage Get(int id, string callback)
        {
            var content = new JSONPReturn
            {
                Callback = callback,
                JSON = "{'id':"+id.ToString()+"', 'data':'Hello from JSONP'"
            };
            var msg = Request.CreateResponse(HttpStatusCode.OK, content, "application/javascript");
            return msg;
        }

    }
}

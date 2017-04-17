using HelloWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

namespace HelloWebAPI.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        public int Get()
        {
            return 0;
        }

        // GET api/values/5
        public int Post(int id,int quantity=1)
        {
            return id+quantity;
        }
        public IHttpActionResult PostUser([FromUri]User user)
        
        {
            return Ok(user);
        }
        
        // POST api/values
       

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}

using Market.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Market.Controllers
{
    public class Market
    {
        public static User manager = new User { login = "manager", password = "manager" };
        public static User buyer = new User { login = "buyer", password = "buyer" };
        public static bool manager_log = false, buyer_log = false;
        public static List<Product> list = new List<Product>();
    }
    public class ValuesController : ApiController
    {

        public IHttpActionResult GetList()
        {
            if (Market.list.Count == 0)
                return Ok("Market is empty! Please log in as manager and add products.");
            else
                return Ok(Market.list);
        }



        public IHttpActionResult GetById(int id)
        {
            Product product = null;
            foreach (var m in Market.list)
                if (m.Id.Equals(id))
                {
                    product = m;
                    break;
                }
            if (product == null)
                return Ok("Item doesn't exist!");
            else
                return Ok(product);
        }


        public IHttpActionResult DeleteById(int id)
        {
            if (Market.manager_log)
            {
                bool t = false;
                foreach (var m in Market.list)
                    if (m.Id.Equals(id))
                    {
                        Market.list.Remove(m);
                        t = true;
                        break;
                    }
                if (t)
                    return Ok("Item removed!");
                else
                    return Ok("Item doesn't exist!");
            }
            else
                return Ok("Please log in as manager!");
        }


        public IHttpActionResult PutProduct([FromUri] Product p)
        {
            if(ModelState.IsValid)
            {
                bool t = false;
                if (Market.manager_log)
                {
                    foreach (var m in Market.list)
                    {
                        if (m.Id.Equals(p.Id))
                        {
                            t = true;
                            m.Quantity += p.Quantity;
                            break;
                        }
                    }
                    if (!t)
                    {
                        Market.list.Add(p);
                    }
                    return Ok("Item added!");
                }
                else
                {
                    return Ok("Please log in as manager!");
                }
            }
            else
            {
                return BadRequest("Bad request");
            }
            

        }



    }
}

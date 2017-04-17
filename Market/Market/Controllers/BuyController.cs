using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Market.Controllers
{
    public class BuyController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok(Market.list);
        }
        public IHttpActionResult Post_BuyProductByID(int id)
        {
            int quantity = 1;
            if (Market.buyer_log)
            {
                foreach (var m in Market.list)
                {
                    if (m.Id.Equals(id))
                    {
                        if (m.Quantity >= quantity)
                        {
                            m.Quantity -= quantity;
                            return Ok("Thank You!");
                        }
                        else
                        {
                            return Ok("There are less items than you want to buy!");
                        }
                    }
                }
                return Ok("Sorry,the product doesn't exist!");
            }
            else
                return Ok("Please log in as buyer");
        }
        public IHttpActionResult Post_BuyProduct(int id, int quantity)
        {
            if (Market.buyer_log)
            {
                foreach (var m in Market.list)
                {
                    if (m.Id.Equals(id))
                    {
                        if (m.Quantity >= quantity)
                        {
                            m.Quantity -= quantity;
                            return Ok("Thank You!");
                        }
                        else
                        {
                            return Ok("There are less items than you want to buy!");
                        }
                    }
                }
                return Ok("Sorry,the product doesn't exist!");
            }
            else
                return Ok("Please log in as buyer");
        }
    }
}
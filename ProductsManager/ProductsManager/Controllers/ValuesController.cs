using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ProductsManager.Controllers
{

    [RoutePrefix("api")]
    public class ValuesController : ApiController
    {
        static List<Product> list = new List<Product>();
        static User manager = new User { login = "manager", password = "manager" };
        static User buyer = new User { login = "buyer", password = "buyer" };
        static bool manager_log = false, buyer_log = false;

        [Route("login")]
        [HttpPost]
        public IHttpActionResult LogIn(User user)
        {
            if (user.password == buyer.password && user.login == buyer.login)
            {
                buyer_log = true;
                return Ok("You are logegd in as buyer! You can see the list of products and buy something.");
            }
            else if (user.password == manager.password && user.login == manager.login)
            {
                manager_log = true;
                return Ok("You are logged in as manager! You can add new products,remove some products,see list of products.");
            }
            else
            {
                return Ok("Wrong login or password.You can see the list of products.");
            }
        }

        [Route("products")]
        [HttpGet]
        public IHttpActionResult GetInfoAboutProducts()
        {
            return Ok(list);
        }

        [Route("products")]
        [HttpPost]
        public IHttpActionResult GetProductByName(Product p)
        {
            Product product = new Product();
            foreach (var m in list)
                if (m.Type.Equals(p.Type))
                {
                    product = m;
                    break;
                }
            if (product == null)
                return Ok("Item doesn't exist!");
            else
                return Ok(product);
        }
        [Route("products")]
        [HttpDelete]
        public IHttpActionResult DeleteProduct(Product p)
        {
            if (manager_log)
            {
                bool t = false;
                foreach (var m in list)
                    if (m.Equals(p))
                    {
                        list.Remove(m);
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
        [Route("products")]
        [HttpPut]
        public IHttpActionResult AddProduct(Product p)
        {
            bool t = false;
            if (manager_log)
            {
                foreach (var m in list)
                {
                    if (m.Type.Equals(p.Type))
                    {
                        t = true;
                        m.Quantity++;
                        break;
                    }
                }
                if (!t)
                {
                    p.Quantity = 1;
                    list.Add(p);
                }
                return Ok("Item added!");
            }
            else
            {
                return Ok("Please log in as manager!");
            }

        }
    }
}
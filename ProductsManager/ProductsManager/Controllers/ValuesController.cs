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
        public IHttpActionResult LogIn(string login, string password)
        {
            if (password == buyer.password && login == buyer.login)
            {
                buyer_log = true;
                return Ok("You are logegd in as buyer! You can see the list of products and buy something.");
            }
            else if (password == manager.password && login == manager.login)
            {
                manager_log = true;
                return Ok("You are logged in as manager! You can add new products,remove some products,see list of products.");
            }
            else
            {
                return Ok("Wrong login or password.You can see the list of products.");
            }
        }
        [Route("logout")]
        [HttpGet]
        public IHttpActionResult LogOut()
        {
            manager_log = false;
            buyer_log = false;
            return Ok("You are logged out!");
        }

        [Route("products")]
        [HttpGet]
        public IHttpActionResult GetInfoAboutProducts()
        {
            if (list.Count == 0)
                return Ok("Market is empty! Please log in as manager and add products.");
            else
                return Ok(list);
        }

        [Route("products")]
        [HttpPost]
        public IHttpActionResult GetProductByName(string type)
        {
            Product product = new Product();
            foreach (var m in list)
                if (m.Type.Equals(type))
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
        public IHttpActionResult AddProduct(string type, int quantity, int price)
        {
            bool t = false;
            if (manager_log)
            {
                foreach (var m in list)
                {
                    if (m.Type.Equals(type))
                    {
                        t = true;
                        m.Quantity += quantity;
                        break;
                    }
                }
                if (!t)
                {
                    Product p = new Product();
                    p.Type = type;
                    p.Quantity = quantity;
                    p.Price = price;
                    list.Add(p);
                }
                return Ok("Item added!");
            }
            else
            {
                return Ok("Please log in as manager!");
            }

        }

        [Route("products/buy")]
        [HttpPost]
        public IHttpActionResult BuyProduct(string type, int quantity)
        {
            if (buyer_log)
            {
                foreach (var m in list)
                {
                    if (m.Type.Equals(type))
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
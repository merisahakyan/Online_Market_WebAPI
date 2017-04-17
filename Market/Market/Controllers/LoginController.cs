using Market.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Market.Controllers
{
    public class LoginController:ApiController
    {
        
        public IHttpActionResult PostLogIn(string login,string password)
        {
            if (password == Market.buyer.password && login == Market.buyer.login)
            {
                Market.buyer_log = true;
                return Ok("You are logegd in as buyer! You can see the list of products and buy something.");
            }
            else if (password == Market.manager.password && login == Market.manager.login)
            {
                Market.manager_log = true;
                return Ok("You are logged in as manager! You can add new products,remove some products,see list of products.");
            }
            else
            {
                return Ok("Wrong login or password.You can see the list of products.");
            }
        }
        
        public IHttpActionResult GetLogOut()
        {
            Market.manager_log = false;
            Market.buyer_log = false;
            return Ok("You are logged out!");
        }
    }
}
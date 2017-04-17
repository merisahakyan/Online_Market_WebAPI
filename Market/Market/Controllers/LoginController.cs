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
        
        public IHttpActionResult PostLogIn([FromUri] User user)
        {
            if(ModelState.IsValid)
            {
                if (user.password == Market.buyer.password && user.login == Market.buyer.login)
                {
                    Market.buyer_log = true;
                    return Ok("You are logegd in as buyer! You can see the list of products and buy something.");
                }
                else if (user.password == Market.manager.password && user.login == Market.manager.login)
                {
                    Market.manager_log = true;
                    return Ok("You are logged in as manager! You can add new products,remove some products,see list of products.");
                }
                else
                {
                    return Ok("Wrong login or password.You can see the list of products.");
                }
            }
            else
            {
                return BadRequest("Enter login and password!");
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
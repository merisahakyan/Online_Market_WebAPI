using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Market.Models
{
    public class User
    {
        [Required]
        public string login { get; set; }
        [Required]
        public string password { get; set; }
    }
}
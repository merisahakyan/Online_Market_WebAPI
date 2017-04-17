using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Market.Models
{
    public class Product
    {
        public string Type { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Id { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webshop.Models;

namespace webshop.Models
{
    public class CartModel : ProductModel
    {
        public string CartId { get; set; }
        public int ProductId { get; set; }
    }
}
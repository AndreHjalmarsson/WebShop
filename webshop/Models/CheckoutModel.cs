using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webshop.Models
{
    public class CheckoutModel : CartModel
    {
        public int MemberId { get; internal set; }
        public string CartNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }

    }
}
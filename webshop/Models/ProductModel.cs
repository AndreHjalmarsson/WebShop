﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace webshop.Models
{
    public class ProductModel
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Id { get; internal set; }
        public string Description { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }

        //public string Image { get; set; }

        //[Required]
        //public HttpPostedFileBase Image { get; set; }

        public string Category { get; set; }
        public IEnumerable<SelectListItem> CategoryList
        {
            get
            {
                return new List<SelectListItem>
               {
                new SelectListItem { Text = "Laptop", Value = "Laptop"},
                new SelectListItem { Text = "Mouse", Value = "Mouse"},
                new SelectListItem { Text = "Keybord", Value = "Keybord"},
                new SelectListItem { Text = "Headset", Value = "Headset"},
                new SelectListItem { Text = "Speakers", Value = "Speakers"}
               };
            }
        }
    }
}
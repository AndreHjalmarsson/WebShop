using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webshop.Models;
using Dapper;

namespace webshop.Controllers
{
    public class ProductController : Controller
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public ActionResult Index()
        {
            List<ProductModel> Products;

            using (var connection = new SqlConnection(this.connectionString))
            {
                Products = connection.Query<ProductModel>("SELECT * FROM Products").ToList();

                if (Products.Count == 0)
                {
                    ViewBag.Message = "Currently no products in the shop.";
                }
            }

            return View(Products);
        }

        public ActionResult Get(string id)
        {
            ProductModel SingleProduct;

            using (var connection = new SqlConnection(this.connectionString))
            {
                SingleProduct = connection.QuerySingleOrDefault<ProductModel>("SELECT * FROM Products WHERE id = @id", new { id });
            }

            if (SingleProduct == null)
                return HttpNotFound();

            return View(SingleProduct);
        }
    }
}
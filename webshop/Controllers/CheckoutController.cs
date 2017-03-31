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
    public class CheckoutController : Controller
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public ActionResult Index()
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult VerifyCheckout(CheckoutModel CheckoutInfo)
        {
            var CartId = Request.Cookies["cart"].Value;

            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Execute("INSERT INTO Members (CartNumber, FirstName, LastName, Email, Street, ZipCode, City) VALUES (@CartNumber, @FirstName, @Lastname, @Email, @Street, @ZipCode, @City)",
                    new {CartNumber = CartId, FirstName = CheckoutInfo.FirstName, LastName = CheckoutInfo.LastName, Email = CheckoutInfo.Email, Street = CheckoutInfo.Street, ZipCode = CheckoutInfo.ZipCode, City = CheckoutInfo.City });

                return View();
            }
        }

        [HttpGet]
        public ActionResult VerifyCheckout()
        {
            List<CheckoutModel> CheckoutInfo;

            var CartId = Request.Cookies["cart"].Value;

            using (var connection = new SqlConnection(this.connectionString))
            {
                CheckoutInfo = connection.Query<CheckoutModel>("SELECT * FROM Cart JOIN Members ON Cart.CartId = Members.CartNumber WHERE Cart.CartId = @CartId",
                     new { CartId = CartId }).ToList();

                return View(CheckoutInfo);
            }
        }
    }
}
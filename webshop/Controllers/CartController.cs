using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using System.Data.SqlClient;
using webshop.Models;

namespace webshop.Controllers
{
    public class CartController : Controller
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DisplayCart()
        {
            List<CartModel> Cart;

            if (Request.Cookies["cart"] != null)
            {
                var CartId = Request.Cookies["cart"].Value;

                using (var connection = new SqlConnection(this.connectionString))
                {
                    Cart = connection.Query<CartModel>("SELECT * FROM Products JOIN Cart ON Products.Id = Cart.ProductId WHERE CartId = @CartId",
                        new { CartId = CartId }).ToList();

                    if (Cart.Count == 0)
                    {
                        ViewBag.Message = "Your cart is empty";
                    }
                }
                return View(Cart);
            }
            ViewBag.Message = "Your cart is empty";
            return View();
        }

        [HttpPost]
        public ActionResult AddProductToCart(string ProductId, string ProductName)
        {
            string CartId;

            if (Request.Cookies["cart"] != null)
            {
                CartId = Request.Cookies["cart"].Value;
            }
            else
            {
                CartId = Guid.NewGuid().ToString();
                HttpCookie Cookie = new HttpCookie("cart");
                Cookie.Value = CartId;
                Cookie.Expires = DateTime.Now.AddDays(10d);
                Response.Cookies.Add(Cookie);
            }

            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Execute("INSERT INTO Cart (CartId, ProductId) VALUES (@CartId, @ProductId)",
                    new { CartId = CartId, ProductId = ProductId });
            }

            TempData["Message"] = $"Added {ProductName} to your cart.";
            return Redirect(this.Request.UrlReferrer.AbsolutePath);
        }

        [HttpPost]
        public ActionResult DeleteProduct(int CartProductId, string ProductName)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Execute("DELETE FROM Cart WHERE Id = @CartProductId",
                    new { CartProductId = CartProductId });
            }

            TempData["Message"] = $"Removed {ProductName} from your cart.";
            return Redirect(this.Request.UrlReferrer.AbsolutePath);
        }

    }
}
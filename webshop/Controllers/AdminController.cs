using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webshop.Models;
using Dapper;
using System.Data.SqlClient;
using System.IO;

namespace webshop.Controllers
{
    public class AdminController : Controller
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public ActionResult Index()
        {
            if (Session["Admin"] != null)
            {
                return RedirectToAction("AdminAccess");
            }

           return View();
        }

        [HttpGet]
        public ActionResult AddProductToShop()
        {
            return View(new ProductModel());
        }

        [HttpPost]
        public ActionResult AddProductToShop(ProductModel product, HttpPostedFileBase Image)
        {

            using (var connection = new SqlConnection(this.connectionString))
            {
                if (ModelState.IsValid)
                {
                    if (Image != null)
                    {
                        string image1 = Image.FileName;
                        product.Image = image1;
                        var Image1Path = Path.Combine(Server.MapPath("~/Content/img"), image1);
                        Image.SaveAs(Image1Path);
                    }
                }

                    connection.Execute("INSERT INTO Products(Name, Category, Price, Image) values(@Name, @Category, @Price, @Image)", 
                    new { Name = product.Name, Category = product.Category, Price = product.Price, Image = product.Image });
            }

            TempData["Message"] = $"Added following product to the shop: {product.Name}";
            return RedirectToAction("AddProductToShop");
        }

        [HttpGet]
        public ActionResult DisplayAdminProducts()
        {
            List<ProductModel> Products;

            using (var connection = new SqlConnection(this.connectionString))
            {
                Products = connection.Query<ProductModel>("SELECT * FROM Products").ToList();
            }
            return View(Products);
        }

        [HttpPost]
        public ActionResult RemoveProductFromShop(int Id)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Execute("DELETE FROM Products WHERE Id = @Id",
                    new { Id = Id });
            }

            return Redirect(this.Request.UrlReferrer.AbsolutePath);
        }

        [HttpPost]
        public ActionResult AdminAccess(string PostUsername, string PostPassword)
        {
            var username = "admin";
            var password = "admin";

            if (PostUsername == username && PostPassword == password)
            {
                Session["Admin"] = "valid";
                return View();
            }

            TempData["Message"] = "Wrong username or password, try again...";
            return RedirectToAction("Index");
        }

        public ActionResult AdminAccess()
        {
            if (Session["Admin"] != null)
            {
                return View();
            }

            return RedirectToAction("index");
        }
    }
}
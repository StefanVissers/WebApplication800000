using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using WebApplication800000.Models;
using WebApplication800000.Data;
using MySql.Data.MySqlClient;

namespace WebApplication800000.Controllers
{
    public class ProductController : Controller
    {
        public static List<ProductModels> products = new List<ProductModels>();
        public static List<ProductModels> addedproducts = new List<ProductModels>();
        public static List<ProductModels> addedwishes = new List<ProductModels>();
        private MySqlConnection conn;

        // GET: Products
        public ActionResult Singleproduct()
        {
            ViewBag.Message = "Your product page.";
            return View();
        }
        public ActionResult Browseproduct()
        {
            ViewBag.Message = "Your browseproduct page.";
            return View();
        }

        public ActionResult Productshowcase()
        {
            HttpCookie cookie = new HttpCookie("productcookie");
            cookie.Name = "productcookie";
            cookie.Values.Add("id", "product_id");
            cookie.Value = DateTime.Now.ToString();
            cookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cookie);
            var id = Request.Cookies["productcookie"].Value;

            HttpCookie cookiewishlist = new HttpCookie("wishcookie");
            cookiewishlist.Name = "wishcookie";
            cookiewishlist.Values.Add("id", "product_id");
            cookiewishlist.Value = DateTime.Now.ToString();
            cookiewishlist.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cookiewishlist);
            var idee = Request.Cookies["wishcookie"].Value;

            products.Clear();
            ViewBag.Message = "Your Productshowcase page.";
            for (int product_id = 1; product_id <= 100; product_id++)
            {
                products.Add(new ProductModels(product_id));
            }
            return View(products);
        }
        public ActionResult addShop(String m)
        {
            HttpCookie cookie = new HttpCookie("product");
            cookie.Values.Add(m, m);
            Response.Cookies.Add(cookie);
            addedproducts.Add(new ProductModels(Int32.Parse(m)));
            ViewBag.Message = "Your addShop page.";
            return RedirectToAction("Shoppingcart", "Product");
        }
        public ActionResult remShop(String m)
        {
            foreach (var x in addedproducts)
            {
                if (x.products_id == (Int32.Parse(m)))
                {
                    addedproducts.Remove(x);
                    return RedirectToAction("Shoppingcart", "Product");
                }
            }
            ViewBag.Message = "Your addShop page.";
            return RedirectToAction("Shoppingcart", "Product");
        }

        public ActionResult CrWishlist(String w)
        {
            HttpCookie cookiewishlist = new HttpCookie("wish");
            cookiewishlist.Values.Add(w, w);
            Response.Cookies.Add(cookiewishlist);
            addedwishes.Add(new ProductModels(Int32.Parse(w)));
            ViewBag.Message = "Your CrWishlist page.";
            return RedirectToAction("Wishlist", "Product");
        }
        public ActionResult ReWishlist(String w)
        {
            foreach (var x in addedwishes)
            {
                if (x.products_id == (Int32.Parse(w)))
                {
                    addedwishes.Remove(x);
                    return RedirectToAction("Wishlist", "Product");
                }
            }
            ViewBag.Message = "Your ReWishlist page.";
            return RedirectToAction("Wishlist", "Product");
        }
        public ActionResult Wishlist()
        {
            if (Request.Cookies.Get("wishcookie") != null && addedwishes.Count == 0)
            {
                for (int x = 0; x < 1500; x = x + 1)
                {
                    if (Request.Cookies.Get("wishcookie").Values.Get(x + "") != null)
                    {
                        addedwishes.Add(new ProductModels(x));
                    }
                }
            }
            Request.Cookies.Get("wishcookie");
            ViewBag.Message = "Your Wishlist page.";
            return View(addedwishes);
        }
        public ActionResult Shoppingcart()
        {
            if (Request.Cookies.Get("productcookie") != null && addedproducts.Count == 0)
            {
                for (int x = 0; x < 1500; x = x + 1)
                {
                    if (Request.Cookies.Get("productcookie").Values.Get(x + "") != null)
                    {
                        addedproducts.Add(new ProductModels(x));
                    }
                }
            }
            Request.Cookies.Get("productcookie");
            ViewBag.Message = "Your Shopping Cart page.";
            return View(addedproducts);
        }

        public ActionResult BuyProducts(List<ProductModels> addedProducts)
        {
            addedProducts = addedproducts;
            ViewBag.Message = "Checkout";


            if (Request.Cookies.Get("loggedInCookie") == null)
            {
                return RedirectToAction("Login", "Customers");

            }
            return View(addedproducts);
        }

        public ActionResult AfterBuyProducts(List<ProductModels> addedProducts)
        {
            addedProducts = addedproducts;
            conn = Connection.Initialize();
            conn.Open();

            var cook = Request.Cookies["customerIdCookie"].Value;

            //orders insert
            MySqlCommand ordercmd = new MySqlCommand("INSERT INTO orders (customer_id, current_status) VALUES (" + cook + ", 'Pending'", conn);
            ordercmd.Prepare();
            ordercmd.ExecuteNonQuery();

           

            foreach (var x in addedProducts)
            {
                //stock update
                MySqlCommand productcmd = new MySqlCommand("UPDATE products SET stock = stock - 1 WHERE product_id = " + x.products_id, conn);
                productcmd.Prepare();
                productcmd.ExecuteNonQuery();
                //orderedproducts insert
                MySqlCommand orderedproductscmd = new MySqlCommand("INSERT INTO orderedproducts (price_on_purchase, catagory, manufactorer, name) VALUES (" + x.Price + ", '" + x.Catagory + "', '" + x.Manufactorer + "', '" + x.Name + "')", conn);
                orderedproductscmd.Prepare();
                orderedproductscmd.ExecuteNonQuery();
                //orderproduct insert WIP
                
                MySqlCommand orderproductscmd = new MySqlCommand("INSERT INTO orderproducts (order_id, product_id, customer_id) SELECT (SELECT order_id FROM orders WHERE customer_id = " + Request.Cookies["customerIdCookie"].Value + "), (SELECT product_id FROM orderedproducts WHERE product_id = (SELECT max(product_id) FROM orderedproducts)), (SELECT customer_id FROM orders WHERE customer_id = " + Request.Cookies["customerIdCookie"].Value + ");", conn);
                orderproductscmd.Prepare();
                orderproductscmd.ExecuteNonQuery();

                
            }

            conn.Close();
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using WebApplication800000.Models;

namespace WebApplication800000.Controllers
{
    public class ProductController : Controller
    {
        public static List<ProductModels> products = new List<ProductModels>();
        public static List<ProductModels> addedproducts = new List<ProductModels>();
        public static List<ProductModels> addedwishes = new List<ProductModels>();

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
    }
}
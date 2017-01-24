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
        //public static List<ProductModels> addproducts = new List<ProductModels>();
        public static List<ProductModels> addedproducts = new List<ProductModels>();

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
        public ActionResult ikShop(String m)
        {
            HttpCookie cookie = new HttpCookie("product");

            cookie.Values.Add(m, m);




            Response.Cookies.Add(cookie);
            addedproducts.Add(new ProductModels(Int32.Parse(m)));
            ViewBag.Message = "Your ikShop page.";
            return RedirectToAction("Shoppingcart", "Product");
        }

        public ActionResult meShop(String m)
        {
            foreach (var x in addedproducts)
            {
                if (x.products_id == (Int32.Parse(m)))
                {
                    addedproducts.Remove(x);
                    return RedirectToAction("Shoppingcart", "Product");
                }
            }
            ViewBag.Message = "Your ikShop page.";
            return RedirectToAction("Shoppingcart", "Product");
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

            //HttpCookie cookie2 = new HttpCookie("ghjhgf");
            //cookie2.Values.Add("name", "x");
            //Response.Cookies.Add(cookie2);
            //var x = Request.Cookies["productcookie"].Value;

            products.Clear();
            ViewBag.Message = "Your Productshowcase page.";
            for (int product_id = 1; product_id <= 100; product_id++)
            {
                products.Add(new ProductModels(product_id));
            }
            return View(products);
        }
        public ActionResult Shoppingcart()
        {
            if (Request.Cookies.Get("product") != null && addedproducts.Count == 0)
            {
                for (int i = 0; i < 1500; i = i + 1)
                {
                    if (Request.Cookies.Get("product").Values.Get(i + "") != null)
                    {
                        addedproducts.Add(new ProductModels(i));
                    }
                }
            }


            Request.Cookies.Get("productcookie");
            ViewBag.Message = "Your Shopping Cart page.";
            
            return View(addedproducts);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebApplication800000.Models;

namespace WebApplication800000.Controllers
{
    public class ProductController : Controller
    {
        public static List<ProductModels> products = new List<ProductModels>();

        // GET: Product
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
            ViewBag.Message = "Your Productshowcase page.";
            for (int id = 1; id <= 2; id++)

            {
                products.Add(new ProductModels(id));
            }

            return View(products);
        }
        public ActionResult Shoppingcart()
        {
            ViewBag.Message = "Your Shopping Cart page.";
            for (int id = 1; id <= 2; id++)
            {
                products.Add(new ProductModels(id));
            }
            return View();
        }

    }
}
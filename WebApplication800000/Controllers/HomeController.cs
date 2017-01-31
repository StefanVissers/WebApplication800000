using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication800000.Models;

namespace WebApplication800000.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ProductGraphs()
        {
            //if (Response.Cookies["adminCookie"] != null)
            //{
            //    if (Response.Cookies["adminCookie"].Value == "true")
            //    {
            //        return RedirectToAction("ProductSales");

            //    }
            //    else if (Response.Cookies["adminCookie"].Value == "false")
            //    {
            //        return RedirectToAction("Index");
            //    }
            //    else
            //    {
            //        return View();
            //    }
            //}
            //else
            //{
            //    HttpCookie adminCookie = new HttpCookie("adminCookie");

            //    Response.Cookies.Add(adminCookie);
            //    Response.Cookies["adminCookie"].Value = "false";
            //    Response.Cookies["adminCookie"].Expires = DateTime.Now.AddMinutes(1);
            //    var x = Request.Cookies["adminCookie"].Value;

            //    return RedirectToAction("Index");

            List<Product> Products = db.Products.ToList();
            return View(Products);
        }

        public ActionResult SalesPerManufactorer()
        {
            OrderModelView vm = new OrderModelView();
            vm.ordered_products = db.Ordered_products.ToList();
            vm.orders = db.Orders.ToList();
            vm.order_products = db.Order_products.ToList();

            return View(vm);
        }

        public ActionResult MostOrderedFromCity()
        {
            List<Address> Addresses = db.Addresses.ToList();
            return View(Addresses);
        }

        public ActionResult ProductSales()
        {
            try
            {
                ViewBag.DataPoints = JsonConvert.SerializeObject(db.Products.ToList(), _jsonSetting);

                return View(db.Products.ToList());
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                return View("Error");
            }
            catch (System.Data.SqlClient.SqlException)
            {
                return View("Error");
            }
        }
        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
    }
}
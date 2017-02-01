using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication800000.Models;
using WebApplication800000.Data;
using MySql.Data.MySqlClient;

namespace WebApplication800000.Controllers
{
    public class UserController : Controller
    {

        public static List<UserModels> userlist = new List<UserModels>();
        public static List<ProductModels> addedwishesz = new List<ProductModels>();
        public ActionResult Edit()
        {
            HttpCookie cookieuser = new HttpCookie("usercookie");
            cookieuser.Name = "usercookie";
            cookieuser.Values.Add("id", "name");
            cookieuser.Value = DateTime.Now.ToString();
            cookieuser.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cookieuser);
            var id = Request.Cookies["usercookie"].Value;
            ViewBag.Message = "Your Edit page.";
            return View(userlist);
        }

        // GET: User
        public ActionResult addUser()
        {
            String id = Request.Cookies["customerIdCookie"].Value;
            HttpCookie usercookie = new HttpCookie("user");
            usercookie.Values.Add(id, id);
            Response.Cookies.Add(usercookie);
            userlist.Add(new UserModels(Int32.Parse(id)));
            MySqlConnection conn = Connection.Initialize();
            addedwishesz.Clear();
            conn.Open();
            MySqlCommand orderproductscmd = new MySqlCommand("SELECT * FROM wishlist_products WHERE customer_id = @val1", conn);
            orderproductscmd.Parameters.AddWithValue("@val1",id);
            orderproductscmd.Prepare();
            var mrTtheBoss = orderproductscmd.ExecuteReader();
            while (mrTtheBoss.Read())
            {
                addedwishesz.Add(new ProductModels(Int32.Parse(mrTtheBoss[0].ToString())));
            }
            conn.Close();
            ViewBag.Message = "Your adduser page.";
            return RedirectToAction("Wishlist", "Product");
        }
        public ActionResult remUser()
        {
            String id = Request.Cookies["customerIdCookie"].Value;
            addedwishesz.Clear();
            foreach (var x in userlist)
            {
                if (x.Id == (Int32.Parse(id)))
                {
                    userlist.Remove(x);
                    return RedirectToAction("Wishlist", "Product");
                }
            }
            //foreach (var x in addedwishes)
            //{
            //    if (x.Id == (Int32.Parse(id)))
            //    {
            //        addedwishes.Remove(x);
            //        return RedirectToAction("Wishlist", "Product");
            //    }
            //}
            ViewBag.Message = "Your remUser page.";
            return RedirectToAction("Wishlist", "Product");
        }

        public ActionResult Userwishlist()
        {
            userlist.Clear();
            ViewBag.Message = "Your Productshowcase page.";
            if (Request.Cookies.Get("usercookie") != null && userlist.Count == 0)
            {
                for (int x = 0; x < 1500; x = x + 1)
                {
                    if (Request.Cookies.Get("usercookie").Values.Get(x + "") != null)
                    {
                        userlist.Add(new UserModels(x));
                    }
                }
            }
            Request.Cookies.Get("usercookie");
            ViewBag.Message = "Your Userwishlist page.";
            return View(userlist);
        }
        public ActionResult Wishlist()
        {
            userlist.Clear();
            ViewBag.Message = "Your Productshowcase page.";
            if (Request.Cookies.Get("usercookie") != null && userlist.Count == 0)
            {
                for (int x = 0; x < 1500; x = x + 1)
                {
                    if (Request.Cookies.Get("usercookie").Values.Get(x + "") != null)
                    {
                        userlist.Add(new UserModels(x));
                    }
                }
            }
            Request.Cookies.Get("usercookie");
            ViewBag.Message = "Your Userwishlist page.";
            return View(userlist);
        }
    }
}
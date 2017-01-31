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
        public ActionResult addUser(String m)
        {
            HttpCookie usercookie = new HttpCookie("user");
            usercookie.Values.Add(m, m);
            Response.Cookies.Add(usercookie);
            userlist.Add(new UserModels(Int32.Parse(m)));
            ViewBag.Message = "Your adduser page.";
            return RedirectToAction("Userwishlist", "Product");
        }
        public ActionResult remUser(String m)
        {
            foreach (var x in userlist)
            {
                if (x.Id == (Int32.Parse(m)))
                {
                    userlist.Remove(x);
                    return RedirectToAction("Userwishlist", "Product");
                }
            }
            ViewBag.Message = "Your remUser page.";
            return RedirectToAction("Userwishlist", "Product");
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
    }
}
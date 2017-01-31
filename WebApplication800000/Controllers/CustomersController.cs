﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication800000.Models;

namespace WebApplication800000.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Surname,Email,Password,date_of_birth,phone_number")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "Email,Password")] Customer customer)
        {
            for (int i = 1; i <= db.Customers.Max(p => p.Id); i++)
            {
                if (db.Customers.Find(i) != null) {
                    if ((customer.Email == db.Customers.Find(i).Email) && (customer.Password == db.Customers.Find(i).Password))
                    {
                        if (db.Customers.Find(i).is_admin)
                        {
                            HttpCookie adminCookie = new HttpCookie("adminCookie");
                            HttpCookie loggedinCookie = new HttpCookie("loggedInCookie");
                            HttpCookie customerIdCookie = new HttpCookie("customerIdCookie");

                            Response.Cookies.Add(adminCookie);
                            Response.Cookies["adminCookie"].Value = "true";
                            Response.Cookies["adminCookie"].Expires = DateTime.Now.AddDays(1);
                            var x = Request.Cookies["adminCookie"].Value;

                            Response.Cookies.Add(loggedinCookie);
                            Response.Cookies["loggedInCookie"].Value = "true";
                            Response.Cookies["loggedInCookie"].Expires = DateTime.Now.AddDays(1);
                            var y = Request.Cookies["loggedInCookie"].Value;

                            Response.Cookies.Add(customerIdCookie);
                            Response.Cookies["customerIdCookie"].Value = i.ToString();

                            return RedirectToAction("Index");
                        }
                        else
                        {
                            HttpCookie adminCookie = new HttpCookie("adminCookie");
                            HttpCookie loggedinCookie = new HttpCookie("loggedInCookie");
                            HttpCookie customerIdCookie = new HttpCookie("customerIdCookie");
                                
                            Response.Cookies.Add(adminCookie);
                            Response.Cookies["adminCookie"].Value = "false";
                            Response.Cookies["adminCookie"].Expires = DateTime.Now.AddMinutes(1);
                            var x = Request.Cookies["adminCookie"].Value;

                            Response.Cookies.Add(loggedinCookie);
                            Response.Cookies["loggedInCookie"].Value = "true";
                            Response.Cookies["loggedInCookie"].Expires = DateTime.Now.AddDays(1);
                            var y = Request.Cookies["loggedInCookie"].Value;

                            Response.Cookies.Add(customerIdCookie);
                            Response.Cookies["customerIdCookie"].Value = i.ToString();

                            return RedirectToAction("Edit/" + i);
                        }
                    }
                }
            }
            return View();
        }

        // GET: Customers/Logout
        public ActionResult Logout()
        {
            Response.Cookies["adminCookie"].Value = "false";
            Response.Cookies["loggedInCookie"].Value = "false";
            Response.Cookies["customerIdCookie"].Value = null;

            return RedirectToAction("/");
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Surname,Email,Password,date_of_birth,phone_number,is_admin")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

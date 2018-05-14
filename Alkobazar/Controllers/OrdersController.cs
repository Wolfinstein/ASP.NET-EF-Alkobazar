using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Alkobazar.Models;
using Alkobazar.Models.DTOs;
using Microsoft.AspNet.Identity;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace Alkobazar.Controllers
{
    [Authorize(Roles = "Admin, Employee")]
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index(string sortOrder, string searchString)
        {

            ViewBag.NumberSortParm = String.IsNullOrEmpty(sortOrder) ? "number_desc" : "";

            var orders = from s in db.Orders
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(a => a.Order_Number.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "number_desc":
                    orders = orders.OrderByDescending(s => s.Order_Number);
                    break;
                default:
                    orders = orders.OrderBy(s => s.Order_Number);
                    break;
            }

            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDTO order = new OrderDTO(db.Orders.Find(id), db.Order_Items
                                                                            .Include(o => o.Order)
                                                                                .Where(i => i.Order.Id == id)
                                                                                .Include(p => p.Product)
                                                                                .Include(c => c.Order.Customer)
                                                                                .Include(u => u.Order.User)
                                                                                .ToList());

            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");

            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Deadline,Order_Number, CustomerId")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.UserId = User.Identity.GetUserId();
               
                order.Create_timestamp = DateTime.Now;
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");

            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Create_timestamp,Deadline,Order_Number")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Income()
        {
        
            var myChart = new Chart(width: 1980, height: 1000)
                  .AddTitle("Monthly Orders in " + DateTime.Now.Year.ToString())
                  .AddSeries(
                      name: "Employee",
                      xValue: new[] {"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" },
                      yValues: new[] {  db.Orders.Where(d => d.Create_timestamp.Month == 1 && d.Create_timestamp.Year == DateTime.Now.Year).Count().ToString(),
                                        db.Orders.Where(d => d.Create_timestamp.Month == 2 && d.Create_timestamp.Year == DateTime.Now.Year).Count().ToString(),
                                        db.Orders.Where(d => d.Create_timestamp.Month == 3 && d.Create_timestamp.Year == DateTime.Now.Year).Count().ToString(),
                                        db.Orders.Where(d => d.Create_timestamp.Month == 4 && d.Create_timestamp.Year == DateTime.Now.Year).Count().ToString(),
                                        db.Orders.Where(d => d.Create_timestamp.Month == 5 && d.Create_timestamp.Year == DateTime.Now.Year).Count().ToString(),
                                        db.Orders.Where(d => d.Create_timestamp.Month == 6 && d.Create_timestamp.Year == DateTime.Now.Year).Count().ToString(),
                                        db.Orders.Where(d => d.Create_timestamp.Month == 7 && d.Create_timestamp.Year == DateTime.Now.Year).Count().ToString(),
                                        db.Orders.Where(d => d.Create_timestamp.Month == 8 && d.Create_timestamp.Year == DateTime.Now.Year).Count().ToString(),
                                        db.Orders.Where(d => d.Create_timestamp.Month == 9 && d.Create_timestamp.Year == DateTime.Now.Year).Count().ToString(),
                                        db.Orders.Where(d => d.Create_timestamp.Month == 10 && d.Create_timestamp.Year == DateTime.Now.Year).Count().ToString(),
                                        db.Orders.Where(d => d.Create_timestamp.Month == 11 && d.Create_timestamp.Year == DateTime.Now.Year).Count().ToString(),
                                        db.Orders.Where(d => d.Create_timestamp.Month == 12 && d.Create_timestamp.Year == DateTime.Now.Year).Count().ToString()})
                  .Write();

            myChart.Save("~/Content/chart", "jpeg");
            // Return the contents of the Stream to the client
            return base.File("~/Content/chart", "jpeg");

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

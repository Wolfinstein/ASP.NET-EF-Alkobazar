using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Alkobazar.Models;

namespace Alkobazar.Controllers
{
    public class Order_ItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Order_Items
        public ActionResult Index()
        {
            return View(db.Order_Items.ToList());
        }

        // GET: Order_Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Items order_Items = db.Order_Items.Find(id);
            if (order_Items == null)
            {
                return HttpNotFound();
            }
            return View(order_Items);
        }

        // GET: Order_Items/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Product.Where(c => c.QuantityInStock > 0), "Id", "Name");
            ViewBag.OrderId = new SelectList(db.Orders, "Id", "Order_Number");

            return View();
        }

        // POST: Order_Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Order_Quantity")] Order_Items order_Items)
        {
            if (ModelState.IsValid)
            {
                db.Order_Items.Add(order_Items);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order_Items);
        }

        // GET: Order_Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Items order_Items = db.Order_Items.Find(id);
            if (order_Items == null)
            {
                return HttpNotFound();
            }
            return View(order_Items);
        }

        // POST: Order_Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Order_Quantity")] Order_Items order_Items)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order_Items).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order_Items);
        }

        // GET: Order_Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Items order_Items = db.Order_Items.Find(id);
            if (order_Items == null)
            {
                return HttpNotFound();
            }
            return View(order_Items);
        }

        // POST: Order_Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order_Items order_Items = db.Order_Items.Find(id);
            db.Order_Items.Remove(order_Items);
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

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Alkobazar.Models;

namespace Alkobazar.Controllers
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
        public ActionResult Details(int? id)
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
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Shipment_Address,Phone")] Customer customer, HttpPostedFileBase image)
        {

            if (ModelState.IsValid)
            {

                if(image != null)
                {
                    customer.Logo = new byte[image.ContentLength];
                    image.InputStream.Read(customer.Logo, 0, image.ContentLength);
                }
                else
                {
                    // Set the default image:
                    Image img = Image.FromFile(
                        Server.MapPath(Url.Content("~/Content/biedronka.jpg")));
                    MemoryStream ms = new MemoryStream();
                    img.Save(ms, ImageFormat.Png); // change to other format
                    ms.Seek(0, SeekOrigin.Begin);
                    customer.Logo = new byte[ms.Length];
                    ms.Read(customer.Logo, 0, (int)ms.Length);
                }

                try
                {

                    db.Customers.Add(customer);
                    db.SaveChanges();

                }
                catch(Exception ex)
                {
                   
                }

                return RedirectToAction("Index");
            }

            return View(customer);
        }

        public ActionResult GetImage(int id)
        {
            Customer p = db.Customers.FirstOrDefault(n => n.Id == id);
            if (p.Logo != null)
            {
                return File(p.Logo, "image/png");
            }
            else
            {
                return null;
            }
        }



        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
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
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Shipment_Address,Phone,Logo")] Customer customer, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    customer.Logo = new byte[image.ContentLength];
                    image.InputStream.Read(customer.Logo, 0, image.ContentLength);
                }

                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
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

using System.Linq;
using System.Net;
using System.Web.Mvc;
using Alkobazar.Models;
using System.Data.Entity;

namespace Alkobazar.Controllers
{
    [Authorize(Roles = "Admin, Employee")]
    public class Order_ItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Order_Items
        public ActionResult Index()
        {
            var ordered_items = db.Order_Items.Include(p => p.Product).Include(o => o.Order).Include(c => c.Order.Customer);

            return View(ordered_items.ToList());
        }

        // GET: Order_Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Items order_Items = db.Order_Items.Where(i => i.Id == id).Include(o => o.Order).Include(p => p.Product).Include(or => or.Order.Customer).First();
            if (order_Items == null)
            {
                return HttpNotFound();
            }
            return View(order_Items);
        }

        // GET: Order_Items/Create
        public ActionResult Create(int? orderId)
        {
            ViewBag.ProductId = new SelectList(db.Product, "Id", "Name");

            return View();
        }

        // POST: Order_Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id, ProductId, Order_Quantity")] Order_Items order_Items, int? orderId)
        {
            Product prod = db.Product.Find(order_Items.ProductId);

            if (prod.QuantityInStock < order_Items.Order_Quantity)
            {
                ModelState.AddModelError("", "Our warehouse contains only " +prod.QuantityInStock);
                ViewBag.ProductId = new SelectList(db.Product, "Id", "Name");
                return View(order_Items);
            }

            if (ModelState.IsValid)
            {
                order_Items.Order = db.Orders.Find(orderId);
                order_Items.OrderId = orderId.Value;
                db.Order_Items.Add(order_Items);
                prod.QuantityInStock = prod.QuantityInStock - order_Items.Order_Quantity;
                db.Entry(prod).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Orders", new { id = order_Items.OrderId });
            }
            ViewBag.ProductId = new SelectList(db.Product, "Id", "Name");
            return View(order_Items);
        }
      
        // GET: Order_Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Items order_Items = db.Order_Items.Where(i => i.Id == id).Include(o => o.Order).Include(p => p.Product).Include(or => or.Order.Customer).First();
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
            Product prod = db.Product.Find(order_Items.ProductId);
            prod.QuantityInStock = prod.QuantityInStock + order_Items.Order_Quantity;
            db.Entry(prod).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", "Orders", new { id = order_Items.OrderId });
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

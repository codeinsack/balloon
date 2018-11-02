using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Balloon.Models;
using System;

namespace Balloon.Controllers
{
    public class OrderAdminController : Controller
    {
        private ApplicationContext db = new ApplicationContext();

        public ActionResult Index(DateTime? start, DateTime? end)
        {
            ViewBag.Start = start;
            ViewBag.End = end;

            DateTime tempStart = DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0));
            DateTime tempEnd = DateTime.Now;

            ViewBag.Start = DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0)).ToString("d");
            ViewBag.End = DateTime.Now.ToString("d");

            if (start == null)
            {
                var orders = db.Orders.Include(i => i.User)
                .Where(x => x.Date > tempStart && x.Date < tempEnd)
                .ToList();

                return View(orders);
            }

            else
            {
                var orders = db.Orders.Include(i => i.User)
                .Where(x => x.Date > start && x.Date < end)
                .ToList();

                return View(orders);
            }
            
        }

        public RedirectToRouteResult Confirmation(Cart cart, int orderId /*,string returnUrl*/)
        {
            Order order = db.Orders
                .FirstOrDefault(p => p.OrderId == orderId);
            order.Confirmation = true;

            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index"/*, new { returnUrl }*/);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var orders = db.Orders
                .Where(o => o.OrderId == id)
                .Include(o => o.OrderItems.Select(oi => oi.Good));

            return View(orders);
        }

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
            ViewBag.UserId = new SelectList(db.Users, "Id", "NickName", order.UserId);
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,Date,TotalSum,Delivery,UserId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "NickName", order.UserId);
            return View(order);
        }

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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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

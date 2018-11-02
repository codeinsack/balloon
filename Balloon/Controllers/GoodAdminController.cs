using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Balloon.Models;
using System;
using System.Collections.Generic;
using System.Web;

namespace Balloon.Controllers
{
    public class GoodAdminController : Controller
    {
        private ApplicationContext db = new ApplicationContext();

        public ActionResult Index(string category)
        {
            IQueryable<Good> goods = db.Goods.Include(p => p.Category);

            if (!String.IsNullOrEmpty(category) && !category.Equals("Все"))
            {
                goods = goods.Where(p => p.Category.Name == category);
            }

            GoodsListViewModel listView = new GoodsListViewModel
            {
                Goods = goods.ToList(),
                Categories = new SelectList(new List<string>()
                {
                    "Все",
                    "Гелиевые",
                    "Фигуры",
                    "Композиции",
                    "Оформление"
                })
            };
            return View(listView);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Good good = db.Goods.Find(id);
            if (good == null)
            {
                return HttpNotFound();
            }
            return View(good);
        }

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GoodId,Name,Price,ClickCount,InOrderCount,SellingSuccess,Availability,ImageData,ImageMimeType,CategoryId")] Good good)
        {
            if (ModelState.IsValid)
            {
                db.Goods.Add(good);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", good.CategoryId);
            return View(good);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Good good = db.Goods.Find(id);
            if (good == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", good.CategoryId);
            return View(good);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GoodId,Name,Price,ClickCount,InOrderCount,SellingSuccess,Availability,ImageData,ImageMimeType,CategoryId")] Good good, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    good.ImageMimeType = image.ContentType;
                    good.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(good.ImageData, 0, image.ContentLength);

                    db.Entry(good).State = EntityState.Modified;
                }
                else
                {
                    Good dbEntry = db.Goods.Find(good.GoodId);

                    if (dbEntry != null)
                    {
                        dbEntry.Name = good.Name;
                        dbEntry.Price = good.Price;
                        dbEntry.Availability = good.Availability;
                        dbEntry.Category = good.Category;
                    }
                }
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", good.CategoryId);
            return View(good);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Good good = db.Goods.Find(id);
            if (good == null)
            {
                return HttpNotFound();
            }
            return View(good);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Good good = db.Goods.Find(id);
            db.Goods.Remove(good);
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

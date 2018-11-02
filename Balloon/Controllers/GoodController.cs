using Balloon.Abstract;
using Balloon.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Balloon.Controllers
{
    public class GoodController : Controller
    {
        private IGoodRepository repository;
        ApplicationContext db = new ApplicationContext();

        public GoodController(IGoodRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category)
        {
            GoodListViewModel viewModel = new GoodListViewModel
            {
                Goods = repository.Goods
                    .Where(p => category == null || p.Category.Name == category && p.Availability == true),
                CurrentCategory = category
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult GoodPartial()
        {
            GoodListViewModel viewModel = new GoodListViewModel
            {
                Goods = repository.Goods
                    .Where(p => p.Availability == true)
                    .Take(9)
                    .OrderBy(p => Guid.NewGuid())
            };

            return View(viewModel);
        }

        public FileContentResult GetImage(int goodId)
        {
            Good good = repository.Goods.FirstOrDefault(p => p.GoodId == goodId);
            if (good != null)
            {
                return File(good.ImageData, good.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        public ActionResult Details(int id)
        {
            Good good = db.Goods.Find(id);

            good.ClickCount++;

            double result;

            if (good.ClickCount != 0)
            {
                result = (double)good.InOrderCount / (double)good.ClickCount * 100;
            }
            else
            {
                if (good.InOrderCount == 0)
                {
                    result = -1;
                }
                else
                {
                    result = (double)good.InOrderCount * 100;
                }
            }

            good.SellingSuccess = result;

            db.Entry(good).State = EntityState.Modified;
            db.SaveChanges();

            return View(good);
        }

        [HttpGet]
        public ActionResult Search(string searching)
        {
            var goods = repository.Goods;

            if (!String.IsNullOrEmpty(searching))
            {
                goods = goods.Where(i => i.Name.Contains(searching) && i.Availability == true);
            }

            return View(goods);
        }


    }
}
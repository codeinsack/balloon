using Balloon.Abstract;
using Balloon.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Balloon.Controllers
{
    public class AdminController : Controller
    {
        ApplicationContext db = new ApplicationContext();
        private IGoodRepository repository;

        public AdminController(IGoodRepository repo)
        {
            repository = repo;
        }

        [Authorize(Roles = "Администратор, Модератор")]
        public ViewResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Администратор")]
        public ViewResult List()
        {
            return View(repository.Goods);
        }

        [Authorize(Roles = "Администратор")]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(repository.Categories, "CategoryId", "Name");
            return View(new Good());
        }

        [HttpPost]
        public ActionResult Create(Good good, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    good.ImageMimeType = image.ContentType;
                    good.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(good.ImageData, 0, image.ContentLength);
                }

                repository.SaveGood(good);
                TempData["message"] = string.Format("Товар «{0}» был сохранен", good.Name);

                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(repository.Categories, "CategoryId", "Name", good.CategoryId);
            return View(good);
        }

        [Authorize(Roles = "Администратор")]
        public ViewResult Edit(int goodId)
        {
            Good good = repository.Goods
                .FirstOrDefault(p => p.GoodId == goodId);

            ViewBag.CategoryId = new SelectList(repository.Categories, "CategoryId", "Name", good.CategoryId);
            return View(good);
        }

        [HttpPost]
        public ActionResult Edit(Good good, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    good.ImageMimeType = image.ContentType;
                    good.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(good.ImageData, 0, image.ContentLength);
                }

                repository.SaveGood(good);
                TempData["message"] = string.Format("Товар «{0}» был сохранен", good.Name);

                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(repository.Categories, "CategoryId", "Name", good.CategoryId);
            return View(good);
        }

        [HttpPost]
        public ActionResult Delete(int goodId)
        {
            Good deletedGood = repository.DeleteGood(goodId);

            if (deletedGood != null)
            {
                TempData["message"] = string.Format("Товар «{0}» был удален!", deletedGood.Name);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Администратор")]
        public ActionResult Orders()
        {
            var users = db.Users.Include(u => u.Orders.Select(o => o.OrderItems.Select(ot => ot.Good).Select(s => s.Category)));
            TempData["Count"] = users.Count();
            return View(users);
        }
        
    }
}
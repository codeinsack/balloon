using Balloon.Abstract;
using Balloon.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Balloon.Controllers
{
    public class ArticleController : Controller
    {
        private IGoodRepository repository;

        public ArticleController(IGoodRepository repo)
        {
            repository = repo;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View(repository.Articles
                .Include(i => i.User)
                .OrderByDescending(i => i.Date));
        }

        [HttpPost]
        public ActionResult Create(Article article, string Title, string Content)
        {
            if (Title == "")
            {
                TempData["Title"] = "Вы не ввели заголовок!";
                return RedirectToAction("Index", "Article");
            }

            if (Content == "")
            {
                TempData["Content"] = "Вы не ввели текст отзыва!";
                return RedirectToAction("Index", "Article");
            }
            if (ModelState.IsValid)
            {
                repository.SaveArticle(article);

                return RedirectToAction("Index");
            }

            return View(article);
        }

        [HttpGet]
        public ActionResult ArticlePartial()
        {
            return View(repository.Articles
                .Include(i => i.User)
                .OrderByDescending(i => i.Date)
                .Take(2));
        }
    }
}
using Balloon.Abstract;
using System.Web.Mvc;
using System.Data.Entity;

namespace Balloon.Controllers
{
    public class HomeController : Controller
    {
        private IGoodRepository repository;

        public HomeController(IGoodRepository repo)
        {
            repository = repo;
        }

        public ActionResult Index()
        {
            return View(repository.Goods.Include(i => i.Category));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //public string Ajax()
        //{
        //    return "Hello, Ajaax";
        //}
    }
}
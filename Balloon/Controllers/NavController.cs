using Balloon.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Balloon.Controllers
{
    public class NavController : Controller
    {
        private IGoodRepository repository;

        public NavController(IGoodRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = repository.Categories
                .Select(x => x.Name);

            return PartialView(categories);
        }
    }
}
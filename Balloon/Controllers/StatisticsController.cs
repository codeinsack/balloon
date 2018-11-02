using Balloon.Abstract;
using Balloon.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Balloon.Controllers
{
    public class StatisticsController : Controller
    {
        ApplicationContext db = new ApplicationContext();

        public ActionResult Index(string SortOrder)
        {
            ViewBag.GoodName = SortOrder == "GoodName_desc" ? "GoodName_asc" : "GoodName_desc";
            ViewBag.BrowseCount = SortOrder == "BrowseCount_desc" ? "BrowseCount_asc" : "BrowseCount_desc";
            ViewBag.OrderCount = SortOrder == "OrderCount_desc" ? "OrderCount_asc" : "OrderCount_desc";
            ViewBag.SellingSuccess = SortOrder == "SellingSuccess_desc" ? "SellingSuccess_asc" : "SellingSuccess_desc";

            var goods = from g in db.Goods
                        select g;

            switch (SortOrder)
            {
                case "GoodName_asc":
                    goods = goods.OrderBy(g => g.Name);
                    break;
                case "GoodName_desc":
                    goods = goods.OrderByDescending(g => g.Name);
                    break;
                case "BrowseCount_asc":
                    goods = goods.OrderBy(g => g.ClickCount);
                    break;
                case "BrowseCount_desc":
                    goods = goods.OrderByDescending(g => g.ClickCount);
                    break;
                case "OrderCount_asc":
                    goods = goods.OrderBy(g => g.InOrderCount);
                    break;
                case "OrderCount_desc":
                    goods = goods.OrderByDescending(g => g.InOrderCount);
                    break;
                case "SellingSuccess_asc":
                    goods = goods.OrderBy(g => g.SellingSuccess);
                    break;
                case "SellingSuccess_desc":
                    goods = goods.OrderByDescending(g => g.SellingSuccess);
                    break;
                default:
                    goods = goods.OrderBy(g => g.Name);
                    break;
            }

            return View(goods.ToList());
        }
    }
}
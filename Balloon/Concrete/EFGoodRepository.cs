using Balloon.Abstract;
using Balloon.Models;
using System.Linq;
using System.Collections.Generic;

namespace Balloon.Concrete
{
    public class EFGoodRepository : IGoodRepository
    {
        private ApplicationContext context = new ApplicationContext();

        public IQueryable<Good> Goods
        {
            get { return context.Goods; }
        }

        public IQueryable<OrderItem> OrderItems
        {
            get { return context.OrderItems; }
        }

        public IQueryable<Category> Categories
        {
            get { return context.Categories; }
        }

        public IQueryable<Article> Articles
        {
            get { return context.Articles; }
        }

        public IQueryable<Order> Orders
        {
            get { return context.Orders; }
        }

        public void SaveGood(Good good)
        {
            if (good.GoodId == 0)
            {
                context.Goods.Add(good);
            }
            else
            {
                Good dbEntry = context.Goods.Find(good.GoodId);

                if (dbEntry != null)
                {
                    dbEntry.Name = good.Name;
                    dbEntry.Price = good.Price;
                    dbEntry.Category = good.Category;
                    dbEntry.ImageData = good.ImageData;
                    dbEntry.ImageMimeType = good.ImageMimeType;
                }
            }
            context.SaveChanges();
        }

        public Good DeleteGood(int itemID)
        {
            Good dbEntry = context.Goods.Find(itemID);
            if (dbEntry != null)
            {
                context.Goods.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveArticle(Article article)
        {
            if (article.ArticleId == 0)
            {
                context.Articles.Add(article);
            }
            else
            {
                Article dbEntry = context.Articles.Find(article.ArticleId);

                if (dbEntry != null)
                {
                    dbEntry.Title = article.Title;
                    dbEntry.Content = article.Content;
                }
            }
            context.SaveChanges();
        }
    }
}

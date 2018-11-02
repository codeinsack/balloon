using Balloon.Models;
using System.Collections.Generic;
using System.Linq;

namespace Balloon.Abstract
{
    public interface IGoodRepository
    {
        IQueryable<Good> Goods { get; }
        IQueryable<OrderItem> OrderItems { get; }
        IQueryable<Category> Categories { get; }
        IQueryable<Article> Articles { get; }
        IQueryable<Order> Orders { get; }

        void SaveGood(Good good);
        Good DeleteGood(int goodId);

        void SaveArticle(Article article);
    }
}
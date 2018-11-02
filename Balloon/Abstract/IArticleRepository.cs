using Balloon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Balloon.Abstract
{
    public interface IArticleRepository
    {
        IQueryable<Article> Articles { get; }

        void DeleteArticle(Article art);
        void GetAllArticles(Article art);
        void GetById(Article art);
        Article SaveArticle(Article art);
    }
}
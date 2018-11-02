using System;
using System.ComponentModel;

namespace Balloon.Models
{
    public class Article
    {
        public int ArticleId { get; set; }

        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [DisplayName("Дата")]
        public DateTime Date { get; set; }

        [DisplayName("Содержание")]
        public string Content { get; set; }
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
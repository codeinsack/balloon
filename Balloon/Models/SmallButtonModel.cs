using System;
using System.Text;

namespace Balloon.Models
{
    public class SmallButtonModel
    {
        public string Action { get; set; }
        public string Text { get; set; }
        public string Glyph { get; set; }
        public string ButtonType { get; set; }
        public int? Id { get; set; }
        public int? ArticleId { get; set; }
        public int? OrderId { get; set; }
        public string UserId { get; set; }

        public string ActionParameters
        {
            get
            {
                var param = new StringBuilder("?");
                if (Id != null && Id > 0)
                    param.Append(String.Format("{0}={1}&", "id", Id));

                if (ArticleId != null && ArticleId > 0)
                    param.Append(String.Format("{0}={1}&", "articleId", ArticleId));

                if (OrderId != null && OrderId > 0)
                    param.Append(String.Format("{0}={1}&", "orderId", OrderId));

                if (UserId != null && !UserId.Equals(string.Empty))
                    param.Append(string.Format("{0}={1}&", "userId", UserId));

                return param.ToString().Substring(0, param.Length - 1);
            }
        }
    }

}
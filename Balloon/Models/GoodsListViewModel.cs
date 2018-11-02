using System.Collections.Generic;
using System.Web.Mvc;

namespace Balloon.Models
{
    public class GoodsListViewModel
    {
        public IEnumerable<Good> Goods { get; set; }
        public SelectList Categories { get; set; }
    }
}
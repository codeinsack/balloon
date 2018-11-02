using System.Collections.Generic;

namespace Balloon.Models
{
    public class GoodListViewModel
    {
        public IEnumerable<Good> Goods { get; set; }
        public string CurrentCategory { get; set; }
        public bool Availability { get; set; }
    }
}
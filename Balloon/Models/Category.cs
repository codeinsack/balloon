using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Balloon.Models
{
    public class Category
    {
        [Required]
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public List<Good> Goods { get; set; }
    }
}
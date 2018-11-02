using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Balloon.Models
{
    public class OrderItem
    {
        [HiddenInput(DisplayValue = false)]
        public int OrderItemId { get; set; }

        [Required(ErrorMessage = "Вы не указали цену")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Цена может принимать только положительные значения!")]
        [DisplayName("Цена")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int GoodId { get; set; }

        public Good Good { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}
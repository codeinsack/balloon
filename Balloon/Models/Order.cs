using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Balloon.Models
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [DisplayName("Дата")]
        public DateTime Date { get; set; }

        [DisplayName("Общая сумма")]
        public decimal TotalSum { get; set; }

        [DisplayName("Доставка")]
        public bool Delivery { get; set; }

        [DisplayName("Время доставки")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DeliveryDate { get; set; }

        [DisplayName("Адрес доставки")]
        public string DeliveryAddress { get; set; }

        [DisplayName("Подтверждение")]
        public bool Confirmation { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
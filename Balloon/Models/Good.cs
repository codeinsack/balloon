using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Balloon.Models
{
    public class Good
    {
        public int GoodId { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите наименование")]
        [DisplayName("Наименование")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Вы не указали цену")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Цена может принимать только положительные значения!")]
        [DisplayName("Цена")]
        public decimal Price { get; set; }

        [DisplayName("Количество просмотров")]
        public int ClickCount { get; set; }

        [DisplayName("Сколько раз товар был заказан")]
        public int InOrderCount { get; set; }

        //private double result;

        [DisplayName("Успешность продаваемости товара")]
        public double SellingSuccess { get; set; }

        [DisplayName("Наличие товара на складе")]
        public bool Availability { get; set; }

        //[DisplayName("Успешность продаваемости товара")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //public double SellingSuccess
        //{
        //    get
        //    {
        //        return result;
        //    }

        //    set
        //    {
        //        if (ClickCount != 0)
        //        {
        //            result = (double)InOrderCount / (double)ClickCount * 100;
        //        }
        //        else
        //        {
        //            if (InOrderCount == 0)
        //            {
        //                result = -1;
        //            }
        //            else
        //            {
        //                result = (double)InOrderCount * 100;
        //            }
        //        }
        //    }
        //}

        public byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

        [DisplayName("Категория")]
        [Required(ErrorMessage = "Необходимо выбрать категорию")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
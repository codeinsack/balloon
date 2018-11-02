using Balloon.Models;
using System.Web.Mvc;

namespace Balloon.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // Получить объект Cart из сессии
            Cart cart = (Cart)controllerContext.HttpContext.Session[sessionKey];

            // Создать объект Cart если его нет в данных сеанса
            if (cart == null)
            {
                cart = new Cart();
                controllerContext.HttpContext.Session[sessionKey] = cart;
            }

            return cart;
        }
    }
}
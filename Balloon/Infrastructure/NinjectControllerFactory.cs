using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Balloon.Abstract;
using Balloon.Concrete;

namespace Balloon.Infrastructure
{
    // Реализация пользовательской фабрики контроллеров, наследуясь от фабрики используемой по умолчанию
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            // Создание контейнера
            ninjectKernel = new StandardKernel();
            AddBindings();
        }
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            // Получение объекта контроллера из контейнера, используя его тип
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }
        private void AddBindings()
        {
            ninjectKernel.Bind<IGoodRepository>().To<EFGoodRepository>();
        }
    }
}
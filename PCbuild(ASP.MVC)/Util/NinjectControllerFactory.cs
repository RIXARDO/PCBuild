using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using System.Web.Mvc;
using Ninject;
using Moq;
using System.Configuration;
using PCbuild_ASP.MVC_.Domain.Abstract;
using PCbuild_ASP.MVC_.Domain.Concrete;

namespace PCbuild_ASP.MVC_.Util
{
        //реализация пользовательской фабрики контроллеров,
        //наследуюсь от фабрики используемой по умолчанию
        public class NinjectControllerFactory : DefaultControllerFactory
        {
            private IKernel ninjectKernel;
            public NinjectControllerFactory()
            {
                //создание контейнера
                ninjectKernel = new StandardKernel();
                AddBindings();
            }

            protected override IController GetControllerInstance(RequestContext requestContext,
                Type controllerType)
            {
                //получение объекта контроллера из контейнера
                //используя его тип
                return controllerType == null
                    ? null
                    : (IController)ninjectKernel.Get(controllerType);
            }

            private void AddBindings()
            {
                ninjectKernel.Bind<ICPURepository>().To<EFCPURepository>();
                ninjectKernel.Bind<IGPURepository>().To<EFGPURepository>();
                ninjectKernel.Bind<IGameRepository>().To<EFGameRepository>();
     
            }
        }
    }
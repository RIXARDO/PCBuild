using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using System.Web.Mvc;
using Ninject;
using Moq;
using System.Configuration;
using PCbuilder_ASP.MVC_.Domain.Abstract;
using PCbuilder_ASP.MVC_.Domain.Concrete;
using System.Web;
using System.Threading;
using PCbuilder_ASP.MVC_.Services.Interfaces;
using PCbuilder_ASP.MVC_.Services.Services;
using PCbuilder_ASP.MVC_.Domain.Entities;
using AutoMapper;

namespace PCbuilder_ASP.MVC_.Util
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
            ninjectKernel.Bind<IBuildEntityRepository>().To<EFBuildRepository>();
            ninjectKernel.Bind<IUnitOfWork>().To<EFUnitOfWork>().InScope(x => HttpContext.Current != null ? (object)HttpContext.Current : (object)Thread.CurrentThread);

            //GenericRepository
            ninjectKernel.Bind(typeof(IGenericRepository<>)).To(typeof(EFRepository<>));
            ninjectKernel.Bind<EFDbContext>().ToSelf().InScope(x => { return HttpContext.Current != null ? (object)HttpContext.Current : (object)Thread.CurrentThread; });

            //Services
            ninjectKernel.Bind<IBuildService>().To<BuildService>().
                WithConstructorArgument("unitOfWork", ninjectKernel.Get<IUnitOfWork>()).
                WithConstructorArgument("buildRepository", ninjectKernel.Get<IGenericRepository<BuildEntity>>()).
                WithConstructorArgument("gpus", ninjectKernel.Get<IGenericRepository<GPU>>()).
                WithConstructorArgument("cpus", ninjectKernel.Get<IGenericRepository<CPU>>()).
                WithConstructorArgument("games", ninjectKernel.Get<IGenericRepository<Game>>());
            ninjectKernel.Bind<ICPUService>().To<CPUService>().
                WithConstructorArgument("unitOfWork", ninjectKernel.Get<IUnitOfWork>()).
                WithConstructorArgument("repository", ninjectKernel.Get<IGenericRepository<CPU>>());
            ninjectKernel.Bind<IGPUService>().To<GPUService>().
                WithConstructorArgument("unitOfWork", ninjectKernel.Get<IUnitOfWork>()).
                WithConstructorArgument("repository", ninjectKernel.Get<IGenericRepository<GPU>>());
            ninjectKernel.Bind<IGameService>().To<GameService>().
                WithConstructorArgument("unitOfWork", ninjectKernel.Get<IUnitOfWork>()).
                WithConstructorArgument("repository", ninjectKernel.Get<IGenericRepository<Game>>());
            ninjectKernel.Bind<IPriceService>().To<PriceService>().
                WithConstructorArgument("unitOfWork", ninjectKernel.Get<IUnitOfWork>()).
                WithConstructorArgument("repository", ninjectKernel.Get<IGenericRepository<Price>>());

            //Mapper
            var mapperConfiguration = CreateConfiguration();
            ninjectKernel.Bind<MapperConfiguration>()
                .ToConstant(mapperConfiguration).InSingletonScope();

            ninjectKernel.Bind<IMapper>().ToMethod(ctx => new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));
        }

        private MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(GetType().Assembly);
            });

            return config;
        }
    }
}
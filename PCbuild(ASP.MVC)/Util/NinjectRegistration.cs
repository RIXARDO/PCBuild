using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using PCbuilder_ASP.MVC_.Domain.Concrete;
using PCbuilder_ASP.MVC_.Domain.Abstract;
using System.Threading;
using PCbuilder_ASP.MVC_.Services.Interfaces;
using PCbuilder_ASP.MVC_.Services.Services;
using Ninject;
using PCbuilder_ASP.MVC_.Domain.Entities;
using AutoMapper;
using PCbuilder_ASP.MVC_.Services.Util;

namespace PCbuilder_ASP.MVC_.Util
{
    public class NinjectRegistration : NinjectModule
    {
        public override void Load()
        {
            AddBindings();
        }


        private void AddBindings()
        {
            Bind<ICPURepository>().To<EFCPURepository>();
            Bind<IGPURepository>().To<EFGPURepository>();
            Bind<IGameRepository>().To<EFGameRepository>();
            Bind<IBuildEntityRepository>().To<EFBuildRepository>();
            Bind<IUnitOfWork>().To<EFUnitOfWork>().InScope(x => HttpContext.Current != null ? (object)HttpContext.Current : (object)Thread.CurrentThread);

            //GenericRepository
            Bind(typeof(IGenericRepository<>)).To(typeof(EFRepository<>));
            Bind<EFDbContext>().ToSelf().InScope(x => { return HttpContext.Current != null ? (object)HttpContext.Current : (object)Thread.CurrentThread; });

            //Services
            Bind<IBuildService>().To<BuildService>(); //.
                                                      //WithConstructorArgument("unitOfWork",  Kernel.Get<IUnitOfWork>()).
                                                      //WithConstructorArgument("buildRepository", Kernel.Get<IGenericRepository<BuildEntity>>()).
                                                      //WithConstructorArgument("gpus", Kernel.Get<IGenericRepository<GPU>>()).
                                                      //WithConstructorArgument("cpus", Kernel.Get<IGenericRepository<CPU>>()).
                                                      //WithConstructorArgument("games", Kernel.Get<IGenericRepository<Game>>());
            Bind<ICPUService>().To<CPUService>();//.
                                                 //WithConstructorArgument("unitOfWork", Kernel.Get<IUnitOfWork>()).
                                                 //WithConstructorArgument("repository", Kernel.Get<IGenericRepository<CPU>>());
            Bind<IGPUService>().To<GPUService>();//.
                                                 //WithConstructorArgument("unitOfWork", Kernel.Get<IUnitOfWork>()).
                                                 //WithConstructorArgument("repository", Kernel.Get<IGenericRepository<GPU>>());
            Bind<IGameService>().To<GameService>();//.
                                                   // WithConstructorArgument("unitOfWork", Kernel.Get<IUnitOfWork>()).
                                                   //WithConstructorArgument("repository", Kernel.Get<IGenericRepository<Game>>());
            Bind<IPriceService>().To<PriceService>();//.
                                                     //WithConstructorArgument("unitOfWork", Kernel.Get<IUnitOfWork>()).
                                                     //WithConstructorArgument("repository", Kernel.Get<IGenericRepository<Price>>());

            Bind<IShowService>().To<ShowService>();

            Bind<ICompareService>().To<CompareService>();

            //Mapper
            var mapperConfiguration = CreateConfiguration();
            Bind<MapperConfiguration>()
                .ToConstant(mapperConfiguration).InSingletonScope();

            Bind<IMapper>().ToMethod(ctx => new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));
        }

        private MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(AutoMapperPresentationProfile));
                cfg.AddProfile(typeof(AutoMapperServicesProfile));

                //cfg.AddMaps(GetType().Assembly);
            });

            return config;
        }

    }
}
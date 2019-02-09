using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using PCbuild_ASP.MVC_.Domain.Concrete;
using PCbuild_ASP.MVC_.Domain.Abstract;


namespace PCbuild_ASP.MVC_.Util
{
    public class NinjectRegistration : NinjectModule
    {
        public override void Load()
        {
            Bind<ICPURepository>().To<EFCPURepository>();
            Bind<IGPURepository>().To<EFGPURepository>();
            Bind<IGameRepository>().To<EFGameRepository>();

        }
    }
}
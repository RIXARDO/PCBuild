using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
using Microsoft.Owin;
using PCbuild_ASP.MVC_.Models;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using PCbuild_ASP.MVC_.Models.Identity;


[assembly:OwinStartup(typeof(PCbuild_ASP.MVC_.App_Start.Startup))]

namespace PCbuild_ASP.MVC_.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //settings for context and manager
            app.CreatePerOwinContext<ApplicationContext>(ApplicationContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
        }
    }
}
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.Owin;
using PCbuild_ASP.MVC_.Models.Identity;
using PCbuild_ASP.MVC_.Domain.Abstract;
using PCbuild_ASP.MVC_.Domain.Entities;
using Microsoft.AspNet.Identity;

namespace PCbuild_ASP.MVC_.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private ApplicationUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
        }

        public IBuildEntityRepository buildRepository;
        public ICPURepository cpuRepository;
        public IGPURepository gpuRepository;

        public UserController(IBuildEntityRepository buildEntityRepo, ICPURepository cpuRepo, IGPURepository gpuRepo)
        {
            buildRepository = buildEntityRepo;
            cpuRepository = cpuRepo;
            gpuRepository = gpuRepo;
        }

        //[Authorize]
        //public async Task<ActionResult> Index()
        //{
        //    ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
        //    return View();
        //}

        

        

        
    }

}
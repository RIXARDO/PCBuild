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

        [Authorize]
        public ActionResult Builds()
        {
            string UserID = User.Identity.GetUserId();
            IEnumerable<BuildEntity> buildEntities = buildRepository.Builds.Where(x => x.UserID == UserID).AsEnumerable();
            return View(buildEntities);
        }

        [HttpPost]
        public ActionResult DeleteBuild(int BuildEntityID)
        {
            var build = buildRepository.Delete(BuildEntityID);
            if (build != null)
            {
                TempData["message"] = "Build was deleted";
            }
            return RedirectToAction("Builds");
        }

        public ActionResult EditBuild(int BuildEntityID)
        {
            BuildEntity build = buildRepository.Builds.FirstOrDefault(x=>x.BuildEntityID==BuildEntityID);
            return View("Build",build);
        }

        [HttpPost]
        [Authorize]
        public ActionResult SaveBuild(string BuildId,string CPUid, string GPUid)
        {
                BuildEntity build = new BuildEntity();
                int BuildID = int.Parse(BuildId);
                int CPUID = int.Parse(CPUid);
                int GPUID = int.Parse(GPUid);
                build.CPUID = CPUID;
                build.GPUID = GPUID ;
                build.UserID = User.Identity.GetUserId();
                build.BuildEntityID = BuildID;
                buildRepository.SaveBuild(build);

                return RedirectToAction("Builds","User");
        }
    }

}
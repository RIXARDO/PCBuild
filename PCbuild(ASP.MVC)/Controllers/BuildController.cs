using PCbuild_ASP.MVC_.Domain.Abstract;
using PCbuild_ASP.MVC_.Models;
using PCbuild_ASP.MVC_.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using PCbuild_ASP.MVC_.Domain.Entities;

namespace PCbuild_ASP.MVC_.Controllers
{
    public class BuildController : Controller
    {
        ICPURepository CPURepository;
        IGPURepository GPURepository;
        IGameRepository GameRepository;
        private IBuildEntityRepository BuildRepository;



        public BuildController(ICPURepository cPU, IGPURepository gPU, IGameRepository game, IBuildEntityRepository build)
        {
            CPURepository = cPU;
            GPURepository = gPU;
            GameRepository = game;
            BuildRepository = build;
        }

        // GET: Build
        public ActionResult Index()
        {
            //Build model = new Build(CPURepository.CPUs.ToList(), GPURepository.GPUs.ToList(), GameRepository.Games.ToList());
            return View(new BuildEntity());
        }

        [HttpPost]
        public ActionResult Action(string CPU, string GPU, string ScreenRez, int? CPUs, int? GPUs)
        {
            if (CPUs != null & GPUs != null)
            {
                BuildResult buildResult = new BuildResult
                {
                    BuildGames = new List<BuildGame>()
                };
                float CPUbench = CPURepository.CPUs.Where(x => x.CPUID == CPUs).Select(x => x.AverangeBench).First() / 100f;
                float GPUbench = GPURepository.GPUs.Where(x => x.GPUID == GPUs).Select(x => x.AverageBench).First() / 100f;
                float ScreenRezConf = (ScreenRez == "p1080") ? 1 : ((ScreenRez == "p1440") ? 0.75f : 0.5f);
                float fp = 120 * CPUbench * GPUbench * ScreenRezConf;
                foreach (Game game in GameRepository.Games)
                {
                    float fps = fp / (game.AverangeRequirements / 100f);
                    buildResult.BuildGames.Add(new BuildGame { Game = game, FPS = (int)fps });
                }
                ////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////
                buildResult.BuildEntity = new BuildEntity
                {
                    CPU = CPURepository.CPUs.FirstOrDefault(x => x.CPUID == CPUs),
                    GPU = GPURepository.GPUs.FirstOrDefault(x => x.GPUID == GPUs),
                    UserID = User.Identity.GetUserId()
                };

                return PartialView(buildResult);
            }

            return PartialView();
        }


        public FileContentResult GetImage(int GameID, bool big = true)
        {
            Game game = GameRepository.Games.FirstOrDefault(g => g.GameID == GameID);
            if (game != null & game.ImageMimeType64 != null & game.ImageMimeType32 != null)
            {

                if (big)
                    return File(game.ImageData64, game.ImageMimeType64);
                else
                    return File(game.ImageData32, game.ImageMimeType32);

            }
            else
            {
                return null;
            }
        }

        public JsonResult DropDownListCPU(string value)
        {
            var cpus = CPURepository.CPUs.Where(x => x.Manufacture == value)
                .Select(x => new { name = x.ProcessorNumber, value = x.CPUID });
            return Json(cpus, JsonRequestBehavior.AllowGet);

        }

        public JsonResult DropDownListGPU(string value)
        {
            var gpus = GPURepository.GPUs.Where(x => x.Manufacture == value)
                .Select(x => new { name = x.Name, value = x.GPUID });
            return Json(gpus, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult Builds()
        {
            string UserID = User.Identity.GetUserId();
            IEnumerable<BuildEntity> buildEntities = BuildRepository.Builds.Where(x => x.UserID == UserID).AsEnumerable();
            return View(buildEntities);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DeleteBuild(int BuildEntityID)
        {
            var build = BuildRepository.Delete(BuildEntityID);
            if (build != null)
            {
                TempData["message"] = "Build was deleted";
            }
            return RedirectToAction("Builds");
        }

        [Authorize]
        public ActionResult EditBuild(int BuildEntityID)
        {
            BuildEntity build = BuildRepository.Builds.FirstOrDefault(x => x.BuildEntityID == BuildEntityID);
            return View("Build", build);
        }

        [HttpPost]
        [Authorize]
        public ActionResult SaveBuild(string BuildId, string CPUid, string GPUid)
        {
            BuildEntity build = new BuildEntity();
            int BuildID = int.Parse(BuildId);
            int CPUID = int.Parse(CPUid);
            int GPUID = int.Parse(GPUid);
            build.CPUID = CPUID;
            build.GPUID = GPUID;
            build.UserID = User.Identity.GetUserId();
            build.BuildEntityID = BuildID;
            BuildRepository.SaveBuild(build);

            return RedirectToAction("Builds", "Build");
        }

    }
}
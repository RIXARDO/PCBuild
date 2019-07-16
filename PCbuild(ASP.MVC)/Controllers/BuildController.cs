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
using PCbuild_ASP.MVC_.Services.Interfaces;
using AutoMapper;
using PCbuild_ASP.MVC_.Models.ViewModel;
using PCbuild_ASP.MVC_.Services.DTO;
using Ninject;

namespace PCbuild_ASP.MVC_.Controllers
{
    public class BuildController : Controller
    {
        ICPURepository CPURepository;
        IGPURepository GPURepository;
        IGameRepository GameRepository;

        private IBuildEntityRepository BuildRepository;


        public BuildController(
            ICPURepository cPU, 
            IGPURepository gPU, 
            IGameRepository game, 
            IBuildEntityRepository build,
            IBuildService service, 
            IMapper mapper)
        {
            CPURepository = cPU;
            GPURepository = gPU;
            GameRepository = game;
            BuildRepository = build;
            Service = service;
            Mapper = mapper;
        }

        IBuildService Service;
        IMapper Mapper;

        public BuildController(IBuildService service, IMapper mapper)
        {
            Service = service;
            Mapper = mapper;
        }

        // GET: Build
        public ActionResult Index()
        {
            //Build model = new Build(CPURepository.CPUs.ToList(), GPURepository.GPUs.ToList(), GameRepository.Games.ToList());
            return View(new BuildEntityViewModel());
        }

        [HttpPost]
        public ActionResult Action(ResolutionEnum ScreenRez, Guid CPUs, Guid GPUs)
        {
            if (CPUs != null & GPUs != null)
            {
                ResolutionDTO resolution = (ResolutionDTO)ScreenRez; 

                BuildResultDTO resultDTO = Service.Action(CPUs,GPUs,ResolutionDTO.res1080);

                BuildResult buildResult = Mapper.Map<BuildResultDTO, BuildResult>(resultDTO);

                //float CPUbench = CPURepository.CPUs.Where(x => x.ProductGuid == CPUs).Select(x => x.AverageBench).First() / 100f;
                //float GPUbench = GPURepository.GPUs.Where(x => x.ProductGuid == GPUs).Select(x => x.AverageBench).First() / 100f;
                //float ScreenRezConf = (ScreenRez == "p1080") ? 1 : ((ScreenRez == "p1440") ? 0.75f : 0.5f);
                //float fp = 120 * CPUbench * GPUbench * ScreenRezConf;
                //foreach (Game game in GameRepository.Games)
                //{
                //    float fps = fp / (game.AverangeRequirements / 100f);
                //    buildResult.BuildGames.Add(new BuildGame { Game = game, FPS = (int)fps });
                //}

                ///////////////////////////////////////////////////////////
                //buildResult.BuildEntity = new BuildEntityViewModel
                //{
                //    CPU = CPURepository.CPUs.FirstOrDefault(x => x.ProductGuid == CPUs),
                //    GPU = GPURepository.GPUs.FirstOrDefault(x => x.ProductGuid == GPUs),
                //    UserID = User.Identity.GetUserId()
                //};

                return PartialView(buildResult);
            }

            return PartialView();
        }


        public FileContentResult GetImage(Guid GameID, bool big = true)
        {
            Game game = GameRepository.Games.FirstOrDefault(g => g.GameGuid == GameID);
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
                .Select(x => new { name = x.ProcessorNumber, value = x.ProductGuid });
            return Json(cpus, JsonRequestBehavior.AllowGet);

        }

        public JsonResult DropDownListGPU(string value)
        {
            var gpus = GPURepository.GPUs.Where(x => x.Developer == value)
                .Select(x => new { name = x.Name, value = x.ProductGuid });
            return Json(gpus, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult Builds()
        {
            string UserID = User!=null?User.Identity.GetUserId():null;
            IEnumerable<BuildEntityDTO> buildEntityViewModels = Service.GetBuilds(null);
            IEnumerable<BuildEntityViewModel> buildEntities =  
                Mapper.Map<IEnumerable<BuildEntityDTO>, 
                IEnumerable<BuildEntityViewModel>>(buildEntityViewModels);
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
        public ActionResult EditBuild(Guid BuildEntityID)
        {
            BuildEntity build = BuildRepository.Builds.FirstOrDefault(x => x.BuildEntityGuid == BuildEntityID);
            return View("Build", build);
        }

        [HttpPost]
        [Authorize]
        public ActionResult SaveBuild(string BuildId, string CPUid, string GPUid)
        {
            BuildEntity build = new BuildEntity();
            Guid.TryParse(BuildId, out Guid BuildID);
            Guid.TryParse(CPUid, out Guid CPUID);
            Guid.TryParse(GPUid, out Guid GPUID);
            build.CPUID = CPUID;
            build.GPUID = GPUID;
            build.UserID = User.Identity.GetUserId();
            build.BuildEntityGuid = BuildID;
            BuildRepository.SaveBuild(build);

            return RedirectToAction("Builds", "Build");
        }
    }
}
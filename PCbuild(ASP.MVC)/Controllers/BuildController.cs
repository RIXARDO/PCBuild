using PCbuilder_ASP.MVC_.Domain.Abstract;
using PCbuilder_ASP.MVC_.Models;
using PCbuilder_ASP.MVC_.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using PCbuilder_ASP.MVC_.Domain.Entities;
using PCbuilder_ASP.MVC_.Services.Interfaces;
using AutoMapper;
using PCbuilder_ASP.MVC_.Models.ViewModel;
using PCbuilder_ASP.MVC_.Services.DTO;
using Ninject;

namespace PCbuilder_ASP.MVC_.Controllers
{
    public class BuildController : Controller
    {
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

                BuildResultDTO resultDTO = Service.Action(CPUs, GPUs, ResolutionDTO.res1080);

                BuildResult buildResult = Mapper.Map<BuildResultDTO, BuildResult>(resultDTO);

                return PartialView(buildResult);
            }

            return PartialView();
        }

        /// <summary>
        /// Get Game image by Id
         /// </summary>
        /// <param name="GameGuid"></param>
        /// <param name="big"></param>
        /// <returns></returns>
        public FileContentResult GetImage(Guid GameGuid)
        {
            FileDTO file = Service.GetImage(GameGuid);
            if (file != null && file.FileType != null)
            {
                return File(file.File,file.FileType);
            }
            else
            {
                return null;
            }
        }

        public JsonResult DropDownListCPU(string value)
        {
            var cpusDTO = Service.GetCPUsByManufacture(value);
            var cpus =
                Mapper
                .Map<IEnumerable<CPUItemDTO>, IEnumerable<CPUDropDownListItem>>(cpusDTO);
            return Json(cpus, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DropDownListGPU(string value)
        {
            var gpusDTO = Service.GetGPUsByDeveloper(value);
            var gpus =
                Mapper.Map<IEnumerable<GPUItemDTO>, IEnumerable<GPUDropDownListItem>>(gpusDTO);
            return Json(gpus, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult Builds()
        {
            //Doubtful
            string UserID = User != null ? User.Identity.GetUserId() : null;

            IEnumerable<BuildEntityDTO> buildEntityViewModels = Service.GetBuilds(UserID);
            IEnumerable<BuildEntityViewModel> buildEntities =
                Mapper.Map<IEnumerable<BuildEntityDTO>,
                IEnumerable<BuildEntityViewModel>>(buildEntityViewModels);
            return View(buildEntities);
        }

        [HttpPost]
        [Authorize]
        public ActionResult DeleteBuild(Guid BuildEntityGuid)
        {
            var build = Service.GetBuildById(BuildEntityGuid);
            Service.DeleteBuild(BuildEntityGuid);
            if (build != null)
            {
                TempData["message"] = "Build: "+build.BuildEntityGuid+" was deleted";
            }
            return RedirectToAction("Builds");
        }

        [Authorize]
        public ActionResult EditBuild(Guid? BuildEntityGuid)
        {
            if (BuildEntityGuid != null)
            {
                BuildEntityDTO buildDTO = Service.GetBuildById(BuildEntityGuid.Value);
                BuildEntityViewModel build = Mapper.Map<BuildEntityDTO, BuildEntityViewModel>(buildDTO);
                return View(build);
            }
            else
            {
                TempData["message"] = "No build selected";
                return RedirectToAction("Builds", "Build");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditBuild(BuildEntityViewModel buildEntity)
        {
            if (ModelState.IsValid)
            {
                var buildDTO = Mapper.Map<BuildEntityViewModel, BuildEntityDTO>(buildEntity);
                buildDTO.UserID = User != null ? User.Identity.GetUserId() : null;
                Service.EditBuild(buildDTO);
                return RedirectToAction("Builds");
            }
            return View(buildEntity);
        }


        [HttpPost]
        [Authorize]
        public ActionResult SaveBuild(string CPUGuid, string GPUGuid)
        {
            BuildEntityDTO build = new BuildEntityDTO();
            Guid.TryParse(CPUGuid, out Guid CPUID);
            Guid.TryParse(GPUGuid, out Guid GPUID);
            build.CPUID = CPUID;
            build.GPUID = GPUID;

            //doubtful
            build.UserID = User!=null?User.Identity.GetUserId():null;

            Service.SaveBuild(build);
            return RedirectToAction("Builds", "Build");
        }
    }
}
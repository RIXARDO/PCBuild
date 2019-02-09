using PCbuild_ASP.MVC_.Domain.Abstract;
using PCbuild_ASP.MVC_.Models;
using PCbuild_ASP.MVC_.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCbuild_ASP.MVC_.Domain.Entities;

namespace PCbuild_ASP.MVC_.Controllers
{
    public class BuildController : Controller
    { 
        ICPURepository CPURepository;
        IGPURepository GPURepository;
        IGameRepository GameRepository;

        

        public BuildController(ICPURepository cPU, IGPURepository gPU, IGameRepository game)
        {
            CPURepository = cPU;
            GPURepository = gPU;
            GameRepository = game;
        }

        // GET: Build
        public ActionResult Index()
        {

            Build model = new Build(CPURepository.CPUs.ToList(), GPURepository.GPUs.ToList(), GameRepository.Games.ToList());
            return View(model);
        }

        [HttpPost]
        public ActionResult Action(string CPU, string GPU, string ScreenRez, int? CPUs, int? GPUs)
        {if (CPUs != null & GPUs != null)
            {
                List<BuildResult> buildResults = new List<BuildResult>();
                float CPUbench = CPURepository.CPUs.Where(x => x.CPUID == CPUs).Select(x => x.AverangeBench).First() / 100f;
                float GPUbench = GPURepository.GPUs.Where(x => x.GPUID == GPUs).Select(x => x.AverageBench).First() / 100f;
                float ScreenRezConf = (ScreenRez == "p1080") ? 1 : ((ScreenRez == "p1440") ? 0.75f : 0.5f);
                float fp = 120 * CPUbench * GPUbench * ScreenRezConf;
                foreach (Game game in GameRepository.Games)
                {
                    float fps = fp / (game.AverangeRequirements / 100f);
                    buildResults.Add(new BuildResult { Game = game, FPS = (int)fps });
                }
                return PartialView(buildResults);
            }
            return PartialView();
        }


        public FileContentResult GetImage(int GameID, bool big=true)
        {
            Game game = GameRepository.Games.FirstOrDefault(g => g.GameID == GameID);
            if (game != null)
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
           var cpus = CPURepository.CPUs.Where(x => x.Manufacture == value).Select(x => new { text = x.ProcessorNumber, value = x.CPUID });
            return Json(cpus, JsonRequestBehavior.AllowGet);
            
        }

        public JsonResult DropDownListGPU(string value)
        {
            var gpus = GPURepository.GPUs.Where(x => x.Manufacture == value).Select(x => new { text = x.Name, value = x.GPUID });
            return Json(gpus, JsonRequestBehavior.AllowGet);
        }
    }
}
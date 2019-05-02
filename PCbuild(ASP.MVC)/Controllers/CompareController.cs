using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCbuild_ASP.MVC_.Domain.Entities;
using PCbuild_ASP.MVC_.Domain.Abstract;
using PCbuild_ASP.MVC_.Models;

namespace PCbuild_ASP.MVC_.Controllers
{
    public class CompareController : Controller
    {
        public ICPURepository cpurepo;
        public IGPURepository gpurepo;
        public IGameRepository gamerepo;

        public CompareController(ICPURepository cPURepository, IGPURepository gPURepository, IGameRepository gameRepository)
        {
            cpurepo = cPURepository;
            gpurepo = gPURepository;
            gamerepo = gameRepository;
        }

        // GET: Compare
        public ActionResult Index(string returnUrl)
        {
            return View(returnUrl);
        }

        public ActionResult CPUCompare(Comparison<CPU> comparison, string returnUrl)
        {
            return View(new CompareIndexViewModel<CPU>
            {
                Comparison = comparison,
                returnUrl = returnUrl
            });
        }

        [HttpPost]
        public RedirectToRouteResult CPUAddToCompare(Comparison<CPU> comparison, int CPUID, string returnUrl)
        {
            CPU cpu = cpurepo.CPUs.FirstOrDefault(x => x.CPUID == CPUID);
            if (cpu != null)
            {
                comparison.AddItem(cpu);
            }
            return RedirectToAction("CPUCompare", new { returnUrl });
        }
        [HttpPost]
        public RedirectToRouteResult CPURemoveFromCompare(Comparison<CPU> comparison, int CPUID, string returnUrl)
        {
            CPU cpu = cpurepo.CPUs.FirstOrDefault(x => x.CPUID == CPUID);
            if (cpu != null)
            {
                comparison.Remove(cpu);
            }
            return RedirectToAction("CPUCompare", new { returnUrl });
        }

        [HttpPost]
        public RedirectToRouteResult CPUClearCompare(Comparison<CPU> comparison, string returnUrl)
        {
            comparison.Clear();
            return RedirectToAction("CPUCompare", new { returnUrl });
        }

        public ActionResult GPUCompare(Comparison<GPU> comparison, string returnUrl)
        {
            return View(new CompareIndexViewModel<GPU>
            {
                Comparison = comparison,
                returnUrl = returnUrl
            });
        }

        [HttpPost]
        public RedirectToRouteResult GPUAddToCompare(Comparison<GPU> comparison, int GPUID, string returnUrl)
        {
            GPU gpu = gpurepo.GPUs.FirstOrDefault(x => x.GPUID == GPUID);
            if (gpu != null)
            {
                comparison.AddItem(gpu);
            }
            return RedirectToAction("GPUCompare", new { returnUrl });
        }

        [HttpPost]
        public RedirectToRouteResult GPURemoveFromCompare(Comparison<GPU> comparison, int GPUID, string returnUrl)
        {
            GPU gpu = gpurepo.GPUs.FirstOrDefault(x => x.GPUID == GPUID);
            if (gpu != null)
            {
                comparison.Remove(gpu);
            }
            return RedirectToAction("GPUCompare", new { returnUrl });
        }

        [HttpPost]
        public RedirectToRouteResult GPUClearCompare(Comparison<GPU> comparison, string returnUrl)
        {
            comparison.Clear();
            return RedirectToAction("GPUCompare", new { returnUrl });
        }
    }
}
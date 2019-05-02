using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCbuild_ASP.MVC_.Domain.Entities;
using PCbuild_ASP.MVC_.Domain.Abstract;
using PCbuild_ASP.MVC_.Models;
using System;


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

        public ActionResult CPUCompare(Domain.Entities.Comparison<CPU> comparison, string returnUrl)
        {
            return View(new CompareIndexViewModel<CPU>
            {
                Comparison = comparison,
                returnUrl = returnUrl
            });
        }

        [HttpPost]
        public RedirectToRouteResult CPUAddToCompare(Domain.Entities.Comparison<CPU> comparison, Guid CPUID, string returnUrl)
        {
            CPU cpu = cpurepo.CPUs.FirstOrDefault(x => x.ProductID == CPUID);
            if (cpu != null)
            {
                comparison.AddItem(cpu);
            }
            return RedirectToAction("CPUCompare", new { returnUrl });
        }
        [HttpPost]
        public RedirectToRouteResult CPURemoveFromCompare(Domain.Entities.Comparison<CPU> comparison, Guid CPUID, string returnUrl)
        {
            CPU cpu = cpurepo.CPUs.FirstOrDefault(x => x.ProductID == CPUID);
            if (cpu != null)
            {
                comparison.Remove(cpu);
            }
            return RedirectToAction("CPUCompare", new { returnUrl });
        }

        [HttpPost]
        public RedirectToRouteResult CPUClearCompare(Domain.Entities.Comparison<CPU> comparison, string returnUrl)
        {
            comparison.Clear();
            return RedirectToAction("CPUCompare", new { returnUrl });
        }

        public ActionResult GPUCompare(Domain.Entities.Comparison<GPU> comparison, string returnUrl)
        {
            return View(new CompareIndexViewModel<GPU>
            {
                Comparison = comparison,
                returnUrl = returnUrl
            });
        }

        [HttpPost]
        public RedirectToRouteResult GPUAddToCompare(Domain.Entities.Comparison<GPU> comparison,
            Guid GPUID, string returnUrl)
        {
            GPU gpu = gpurepo.GPUs.FirstOrDefault(x => x.ProductID == GPUID);
            if (gpu != null)
            {
                comparison.AddItem(gpu);
            }
            return RedirectToAction("GPUCompare", new { returnUrl });
        }

        [HttpPost]
        public RedirectToRouteResult GPURemoveFromCompare(Domain.Entities.Comparison<GPU> comparison,
            Guid GPUID, string returnUrl)
        {
            GPU gpu = gpurepo.GPUs.FirstOrDefault(x => x.ProductID == GPUID);
            if (gpu != null)
            {
                comparison.Remove(gpu);
            }
            return RedirectToAction("GPUCompare", new { returnUrl });
        }

        [HttpPost]
        public RedirectToRouteResult GPUClearCompare(Domain.Entities.Comparison<GPU> comparison, string returnUrl)
        {
            comparison.Clear();
            return RedirectToAction("GPUCompare", new { returnUrl });
        }
    }
}
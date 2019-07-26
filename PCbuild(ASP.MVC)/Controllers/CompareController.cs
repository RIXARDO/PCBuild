﻿using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCbuild_ASP.MVC_.Services.Comparison;
using PCbuild_ASP.MVC_.Domain.Abstract;
using PCbuild_ASP.MVC_.Models;
using PCbuild_ASP.MVC_.Models.ViewModel;
using PCbuild_ASP.MVC_.Services.Interfaces;
using PCbuild_ASP.MVC_.Services.DTO;
using AutoMapper;

namespace PCbuild_ASP.MVC_.Controllers
{
    public class CompareController : Controller
    {
        public ICPURepository cpurepo;
        public IGPURepository gpurepo;
        ICompareService Service;
        IMapper Mapper;

        public CompareController(ICPURepository cPURepository, IGPURepository gPURepository, IGameRepository gameRepository, ICompareService compareService, IMapper mapper)
        {
            cpurepo = cPURepository;
            gpurepo = gPURepository;

            Service = compareService;
            Mapper = mapper;
        }

        // GET: Compare
        public ActionResult Index(string returnUrl)
        {
            return View(returnUrl);
        }

        public ActionResult CPUCompare(Comparison<CPUViewModel> comparison, string returnUrl)
        {
            return View(new CompareIndexViewModel<CPUViewModel>
            {
                Comparison = comparison,
                returnUrl = returnUrl
            });
        }

        [HttpPost]
        public RedirectToRouteResult CPUAddToCompare(Comparison<CPUViewModel> comparison, System.Guid cpuId, string returnUrl)
        {
            CPUdto cpudto = Service.FindCPUByID(cpuId);
            var cpu = Mapper.Map<CPUdto, CPUViewModel>(cpudto);
            if (cpu != null)
            {
                comparison.AddItem(cpu);
            }
            return RedirectToAction("CPUCompare", new { returnUrl });
        }

        [HttpPost]
        public RedirectToRouteResult CPURemoveFromCompare(Comparison<CPUViewModel> comparison, System.Guid cpuId, string returnUrl)
        {
            CPUdto cpudto = Service.FindCPUByID(cpuId);
            CPUViewModel cpu = Mapper.Map<CPUdto,CPUViewModel>(cpudto);
            if (cpu != null)
            {
                comparison.Remove(cpu);
            }
            return RedirectToAction("CPUCompare", new { returnUrl });
        }

        [HttpPost]
        public RedirectToRouteResult CPUClearCompare(Comparison<CPUViewModel> comparison, string returnUrl)
        {
            comparison.Clear();
            return RedirectToAction("CPUCompare", new { returnUrl });
        }

        public ActionResult GPUCompare(Comparison<GPUViewModel> comparison, string returnUrl)
        {
            return View(new CompareIndexViewModel<GPUViewModel>
            {
                Comparison = comparison,
                returnUrl = returnUrl
            });
        }

        [HttpPost]
        public RedirectToRouteResult GPUAddToCompare(Comparison<GPUViewModel> comparison, System.Guid gpuId, string returnUrl)
        {
            GPUdto gpudto = Service.FindGPUByID(gpuId);
            GPUViewModel gpu = Mapper.Map<GPUdto,GPUViewModel>(gpudto);
            if (gpu != null)
            {
                comparison.AddItem(gpu);
            }
            return RedirectToAction("GPUCompare", new { returnUrl });
        }

        [HttpPost]
        public RedirectToRouteResult GPURemoveFromCompare(Comparison<GPUViewModel> comparison, System.Guid gpuId, string returnUrl)
        {
            GPUdto gpudto = Service.FindGPUByID(gpuId);
            GPUViewModel gpu = Mapper.Map<GPUdto, GPUViewModel>(gpudto);
            if (gpu != null)
            {
                comparison.Remove(gpu);
            }
            return RedirectToAction("GPUCompare", new { returnUrl });
        }

        [HttpPost]
        public RedirectToRouteResult GPUClearCompare(Comparison<GPUViewModel> comparison, string returnUrl)
        {
            comparison.Clear();
            return RedirectToAction("GPUCompare", new { returnUrl });
        }
    }
}
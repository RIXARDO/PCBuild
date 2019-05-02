using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCbuild_ASP.MVC_.Domain.Abstract;
using PCbuild_ASP.MVC_.Domain.Entities;

namespace PCbuild_ASP.MVC_.Controllers
{
    [Authorize(Roles ="Admin")]
    public class GPUsController : Controller
    {
        private IGPURepository repository;

        public GPUsController(IGPURepository gPURepository)
        {
            repository = gPURepository;
        }

        // GET: GPUs
        public ActionResult Index()
        {
            return View(repository.GPUs);
        }


        public ViewResult Edit(int id)
        {
            GPU gpu = repository.GPUs.FirstOrDefault(x => x.GPUID == id);
            return View(gpu);
        }

        [HttpPost]
        public ActionResult Edit(GPU gpu)
        {
            if (ModelState.IsValid)
            {
                repository.SaveGPU(gpu);
                TempData["message"] = string.Format("{0} has been saved", gpu.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(gpu);
            }
        }

        public ActionResult Create()
        {
            return View("Edit", new GPU());
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var deletedGPU=repository.DeleteGPU(id);
            if (deletedGPU != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedGPU.Name);
            }
            return RedirectToAction("Index");
        }
    }
}
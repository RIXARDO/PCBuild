using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PCbuilder_ASP.MVC_.Domain.Concrete;
using PCbuilder_ASP.MVC_.Domain.Entities;
using PCbuilder_ASP.MVC_.Domain.Abstract;
using PCbuilder_ASP.MVC_.Services.Interfaces;
using PCbuilder_ASP.MVC_.Services.Services;
using PCbuilder_ASP.MVC_.Models.ViewModel;
using AutoMapper;
using static PCbuilder_ASP.MVC_.Models.ViewModel.CPUViewModel;
using PCbuilder_ASP.MVC_.Services.DTO;

namespace PCbuilder_ASP.MVC_.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CPUsController : Controller
    {
        ICPUService Service;
        IMapper Mapper;

        public CPUsController(ICPUService service, IMapper mapper)
        {
            Service = service;
            Mapper = mapper;
        }
        

        // GET: CPUs
        public ActionResult Index()
        {
            //Mapper Async
            var getcpu = Service.GetCPUs();
            var cpus = Mapper.Map<IEnumerable<CPUdto>, IEnumerable<CPUViewModel>>(getcpu);
            return View(cpus);
        }

        //// GET: CPUs/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CPU cPU = await repository.CPUs.FirstAsync(x => x.CPUID == id);
        //    if (cPU == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(cPU);
        //}

        // GET: CPUs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CPUs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CPUID,Manufacture,ProcessorNumber,NumberOfCores,NumberOfThreads,PBF,Cache,TDP,AverageBench")] CPUViewModel cpu)
        {
            if (ModelState.IsValid)
            {
                var cpudto = Mapper.Map<CPUViewModel, CPUdto>(cpu);
                Service.SaveCPU(cpudto);
                return RedirectToAction("Index");
            }

            return View(cpu);
        }

        // GET: CPUs/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CPUViewModel cpu = Mapper.Map<CPUdto,CPUViewModel>(Service.GetCPUByID(id));
            if (cpu == null)
            {
                return HttpNotFound();
            }
            return View(cpu);
        }

        // POST: CPUs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CPUID,Manufacture,ProcessorNumber,NumberOfCores,NumberOfThreads,PBF,Cache,TDP,AverageBench")] CPUViewModel cpu)
        {
            if (ModelState.IsValid)
            {
                var cpudto = Mapper.Map<CPUViewModel, CPUdto>(cpu);
                Service.EditCPU(cpudto);
                return RedirectToAction("Index");
            }
            return View(cpu);
        }

        //// GET: CPUs/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CPU cPU = repository.DeleteCPU((int)id);
        //    if (cPU == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(cPU);
        //}

        // POST: CPUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Service.DeleteCPU(id);
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

    }
}

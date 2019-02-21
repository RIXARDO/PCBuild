using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PCbuild_ASP.MVC_.Domain.Concrete;
using PCbuild_ASP.MVC_.Domain.Entities;
using PCbuild_ASP.MVC_.Domain.Abstract;

namespace PCbuild_ASP.MVC_.Controllers
{
    public class CPUsController : Controller
    {
        private ICPURepository repository;

        public CPUsController(ICPURepository cPURepository)
        {
            repository = cPURepository;
        }
        

        // GET: CPUs
        public async Task<ActionResult> Index()
        {
            return View(await repository.CPUs.ToListAsync());
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
        public ActionResult Create([Bind(Include = "CPUID,Manufacture,ProcessorNumber,NumberOfCores,NumberOfThreads,PBF,Cache,TDP,AverangeBench")] CPU cPU)
        {
            if (ModelState.IsValid)
            {
                repository.SaveCPU(cPU);
                return RedirectToAction("Index");
            }

            return View(cPU);
        }

        // GET: CPUs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CPU cPU = await repository.CPUs.FirstAsync(x => x.CPUID == id);
            if (cPU == null)
            {
                return HttpNotFound();
            }
            return View(cPU);
        }

        // POST: CPUs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CPUID,Manufacture,ProcessorNumber,NumberOfCores,NumberOfThreads,PBF,Cache,TDP,AverangeBench")] CPU cPU)
        {
            if (ModelState.IsValid)
            {
                repository.SaveCPU(cPU);
                return RedirectToAction("Index");
            }
            return View(cPU);
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
        public ActionResult DeleteConfirmed(int id)
        {
            repository.DeleteCPU(id);
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

using PCbuild_ASP.MVC_.Domain.Abstract;
using PCbuild_ASP.MVC_.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCbuild_ASP.MVC_.Domain.Concrete
{
    public class EFCPURepository: ICPURepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<CPU> CPUs
        {
            get
            {
                return context.CPUs;
            }
        }

        public CPU DeleteCPU(Guid cpuId)
        {
            CPU dbEntry = context.CPUs.Find(cpuId);
            if (dbEntry != null)
            {
                context.CPUs.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveCPU(CPU cpu)
        {
            if (cpu.ProductGuid == null)
            {
                context.CPUs.Add(cpu);
            }
            else
            {
                CPU dbEntry = context.CPUs.Find(cpu.ProductGuid);
                if (dbEntry != null)
                {
                    dbEntry.Manufacture = cpu.Manufacture;
                    dbEntry.NumberOfCores = cpu.NumberOfCores;
                    dbEntry.NumberOfThreads = cpu.NumberOfThreads;
                    dbEntry.PBF = cpu.PBF;
                    dbEntry.Price = cpu.Price;
                    dbEntry.ProcessorNumber = cpu.ProcessorNumber;
                    dbEntry.TDP = cpu.TDP;
                    dbEntry.Cache = cpu.Cache;
                    dbEntry.AverageBench = cpu.AverageBench;
                }
            }
            context.SaveChanges();
        }
    }
}
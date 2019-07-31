using PCbuilder_ASP.MVC_.Domain.Abstract;
using PCbuilder_ASP.MVC_.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCbuilder_ASP.MVC_.Domain.Concrete
{
    public class EFGPURepository : IGPURepository
    {

        private EFDbContext context = new EFDbContext();

        public IQueryable<GPU> GPUs
        {
            get
            {
                return context.GPUs;
            }
        }

        public GPU DeleteGPU(Guid gpuId)
        {
            GPU dbEntry = context.GPUs.Find(gpuId);
            if (dbEntry != null)
            {
                context.GPUs.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveGPU(GPU gpu)
        {
            if (gpu.ProductGuid == null)
            {
                context.GPUs.Add(gpu);
            }
            else
            {
                GPU dbEntry = context.GPUs.Find(gpu.ProductGuid);
                if (dbEntry != null)
                {
                    dbEntry.Manufacture = gpu.Manufacture;
                    dbEntry.Architecture = gpu.Architecture;
                    dbEntry.AverageBench = gpu.AverageBench;
                    dbEntry.BoostClock = gpu.BoostClock;
                    dbEntry.FrameBuffer = gpu.FrameBuffer;
                    dbEntry.MemorySpeed = gpu.MemorySpeed;
                    dbEntry.Name = gpu.Name;
                    dbEntry.Price = gpu.Price;
                }
            }
            context.SaveChanges();
        }
    }
}
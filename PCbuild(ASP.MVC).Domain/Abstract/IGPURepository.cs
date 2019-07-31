using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCbuilder_ASP.MVC_.Domain.Entities;

namespace PCbuilder_ASP.MVC_.Domain.Abstract
{
    public interface IGPURepository
    {
        IQueryable<GPU> GPUs { get; }
        void SaveGPU(GPU gpu);
        GPU DeleteGPU(Guid gpuId);
    }
}

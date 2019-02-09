using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCbuild_ASP.MVC_.Domain.Entities;

namespace PCbuild_ASP.MVC_.Domain.Abstract
{
    public interface ICPURepository
    {
        IQueryable<CPU> CPUs { get; }
        void SaveCPU(CPU cpu);
        CPU DeleteCPU(int CPUid);
    }
}

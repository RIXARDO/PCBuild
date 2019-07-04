using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCbuild_ASP.MVC_.Services.DTO;

namespace PCbuild_ASP.MVC_.Services.Interfaces
{
    public interface ICPUService
    {
        IEnumerable<CPUdto> GetCPUs();
        CPUdto GetCPUByID(Guid guid);
        void SaveCPU(CPUdto cpu);
        void EditCPU(CPUdto cpu);
        void DeleteCPU(Guid guid);
        void Dispose();
    }
}

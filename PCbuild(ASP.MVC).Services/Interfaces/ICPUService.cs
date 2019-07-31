using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCbuilder_ASP.MVC_.Services.DTO;

namespace PCbuilder_ASP.MVC_.Services.Interfaces
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

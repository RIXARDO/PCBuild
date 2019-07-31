using PCbuilder_ASP.MVC_.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCbuilder_ASP.MVC_.Services.Interfaces
{
    public interface ICompareService
    {
        CPUdto FindCPUByID(Guid guid);
        GPUdto FindGPUByID(Guid guid);
    }
}

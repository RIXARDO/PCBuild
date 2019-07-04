using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCbuild_ASP.MVC_.Services.DTO;

namespace PCbuild_ASP.MVC_.Services.Interfaces
{
    public partial interface IBuildService
    {
        IEnumerable<BuildEntityDTO> GetBuilds();

        IEnumerable<CPUdto> GetCPUsByManufacture(string Manufacture);

        IEnumerable<GPUdto> GetGPUsByDeveloper(string Developer);

        BuildResultDTO Action(Guid cpu, Guid gpu, ResolutionDTO resolution);

        void EditBuild(BuildEntityDTO build);

        void DeleteBuild(Guid guid);

        void SaveBuild(BuildEntityDTO build);
        
        void Dispose();
    }
}

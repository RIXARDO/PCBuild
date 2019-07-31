using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCbuilder_ASP.MVC_.Services.DTO;

namespace PCbuilder_ASP.MVC_.Services.Interfaces
{
    public partial interface IBuildService
    {
        IEnumerable<BuildEntityDTO> GetBuilds(string UserGuid);

        IEnumerable<CPUItemDTO> GetCPUsByManufacture(string Manufacture);

        IEnumerable<GPUItemDTO> GetGPUsByDeveloper(string Developer);

        BuildResultDTO Action(Guid cpu, Guid gpu, ResolutionDTO resolution);

        void EditBuild(BuildEntityDTO build);

        void DeleteBuild(Guid guid);

        void SaveBuild(BuildEntityDTO build);

        BuildEntityDTO GetBuildById(Guid BuildGuid);

        void Dispose();
        FileDTO GetImage(Guid gameGuid);
    }
}

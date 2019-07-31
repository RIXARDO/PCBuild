using PCbuilder_ASP.MVC_.Domain.Abstract;
using PCbuilder_ASP.MVC_.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCbuilder_ASP.MVC_.Services.Interfaces
{
    public interface IShowService
    {
        int PageSize { get; set; }
        CPUListDTO ListCPU(int page = 1);
        GPUListDTO ListGPU(int page = 1);
        GameListDTO ListGame(int page = 1);
    }
}

using AutoMapper;
using PCbuilder_ASP.MVC_.Domain.Entities;
using PCbuilder_ASP.MVC_.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PCbuilder_ASP.MVC_.Services.Util
{
    public class AutoMapperServicesProfile: Profile
    {
        public AutoMapperServicesProfile()
        {
            CreateMap<CPU, CPUdto>().ReverseMap();
            CreateMap<GPU, GPUdto>().ReverseMap();
            CreateMap<Price, PriceDTO>().ReverseMap();
            CreateMap<Game, GameDTO>().ReverseMap();
            CreateMap<BuildEntity, BuildEntityDTO>().ReverseMap();
        }
    }
}

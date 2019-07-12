using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PCbuild_ASP.MVC_.Models.ViewModel;
using PCbuild_ASP.MVC_.Services.DTO;

namespace PCbuild_ASP.MVC_.Util
{
    public class AutoMapperPresentationProfile: Profile
    {
        public AutoMapperPresentationProfile()
        {
            CreateMap<CPUdto, CPUViewModel>().ReverseMap();
            CreateMap<GPUdto, GPUViewModel>().ReverseMap();
            CreateMap<GameDTO, GameViewModel>().ReverseMap();
            CreateMap<BuildEntityDTO, BuildEntityViewModel>().ReverseMap();
            CreateMap<PriceDTO, PriceViewModel>().ReverseMap();
        }
    }
}
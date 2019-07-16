using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PCbuild_ASP.MVC_.Models;
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
            CreateMap<BuildEntityDTO, BuildEntityViewModel>()
                .ForMember(x => x.CPU, opt => opt.MapFrom(src => src.CPU))
                .ForMember(x=>x.GPU, opt=>opt.MapFrom(src=>src.GPU))
                .ReverseMap();
            CreateMap<PriceDTO, PriceViewModel>().ReverseMap();
            CreateMap<ResolutionDTO, ResolutionEnum>().ReverseMap();
            CreateMap<BuildResultDTO, BuildResult>()
                .ForMember(x=>x.BuildEntity, opt=>opt.MapFrom(x=>x.Build))
                .ReverseMap();
            CreateMap<BuildGameDTO, BuildGame>()
                .ForMember(x => x.Game, opt => opt.MapFrom(src => src.GameDTO))
                .ReverseMap();
        }
    }
}
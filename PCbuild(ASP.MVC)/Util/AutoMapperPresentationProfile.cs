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
            CreateMap<CPUItemDTO, CPUDropDownListItem>()
                .ForMember(x => x.name, oct => oct.MapFrom(src => src.ProcessorNumber))
                .ForMember(x => x.value, oct => oct.MapFrom(scr => scr.ProductGuid))
                .ReverseMap();
            CreateMap<GPUItemDTO, GPUDropDownListItem>()
                .ForMember(x => x.name, oct => oct.MapFrom(src => src.Name))
                .ForMember(x => x.value, oct => oct.MapFrom(scr => scr.ProductGuid))
                .ReverseMap();
            CreateMap<PagingInfoDTO, PagingInfo>()
                .ForMember(x => x.CurrentPage, opt => opt.MapFrom(src => src.CurrentPage))
                .ForMember(x => x.ItemsPerPage, opt => opt.MapFrom(src => src.ItemsPerPage))
                .ForMember(x => x.TotalItems, opt => opt.MapFrom(src => src.TotalItems));
            CreateMap<CPUListDTO, CPUListViewModel>()
                .ForMember(x => x.CPUs, opt => opt.MapFrom(src => src.CPUList))
                .ForMember(x => x.PagingInfo, opt => opt.MapFrom(src => src.PagingInfo));
            CreateMap<GPUListDTO, GPUListViewModel>()
                .ForMember(x => x.GPUs, opt => opt.MapFrom(src => src.GPUList))
                .ForMember(x => x.PagingInfo, opt => opt.MapFrom(src => src.PagingInfo));
            CreateMap<GameListDTO, GameListViewModel>()
                .ForMember(x => x.Games, opt => opt.MapFrom(src => src.GameList))
                .ForMember(x => x.PagingInfo, opt => opt.MapFrom(src => src.PagingInfo));
        }
    }
}
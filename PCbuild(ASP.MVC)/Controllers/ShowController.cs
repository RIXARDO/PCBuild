using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using PCbuild_ASP.MVC_.Domain.Abstract;
using PCbuild_ASP.MVC_.Models;
using PCbuild_ASP.MVC_.Services.DTO;
using PCbuild_ASP.MVC_.Services.Interfaces;

namespace PCbuild_ASP.MVC_.Controllers
{
    public class ShowController : AsyncController
    {
        IShowService Service;
        IMapper Mapper;
        public int PageSize = 10;

        ICPURepository CPURepository;
        IGPURepository GPURepository;
        IGameRepository gameRepository;

        public ShowController(
            ICPURepository cPU, 
            IGPURepository gPU, 
            IGameRepository game, 
            IShowService service,
            IMapper mapper)
        {
            Service = service;
            Mapper = mapper;
            CPURepository = cPU;
            GPURepository = gPU;
            gameRepository = game;
        }

        // GET: Show
        public ViewResult ListCPU(int page=1)
        {
            var cpuListDTO = Service.ListCPU(page);
            var cpuList = Mapper.Map<CPUListDTO, CPUListViewModel>(cpuListDTO);

            
            return View(cpuList);
        }

        public ViewResult ListGPU(int page = 1)
        {
            var gpuListDTO = Service.ListGPU(page);
            var gpuList = Mapper.Map<GPUListDTO, GPUListViewModel>(gpuListDTO);

            return View(gpuList);
        }

        public ViewResult ListGame(int page = 1)
        {
            var gameListDTO = Service.ListGame(page);
            var gameList = Mapper.Map<GameListDTO, GameListViewModel>(gameListDTO);

            return View(gameList);
        }
    }
}
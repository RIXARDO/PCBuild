using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCbuild_ASP.MVC_.Domain.Abstract;
using PCbuild_ASP.MVC_.Models;
using PCbuild_ASP.MVC_.Services.Interfaces;

namespace PCbuild_ASP.MVC_.Controllers
{
    public class ShowController : AsyncController
    {
        ICPUService CPUs;
        IGPUService GPUs;
        IGameService Games;
        public int PageSize = 10;

        ICPURepository CPURepository;
        IGPURepository GPURepository;
        IGameRepository gameRepository;

        public ShowController(ICPURepository cPU, IGPURepository gPU, IGameRepository game)
        {
            CPURepository = cPU;
            GPURepository = gPU;
            gameRepository = game;
        }

        // GET: Show
        public ViewResult ListCPU(int page=1)
        {
            CPUListViewModel model = new CPUListViewModel
            {
                CPUs = CPURepository.CPUs
                .OrderBy(x => x.ProcessorNumber)
                .Skip((page - 1) * PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = CPURepository.CPUs.Count()
                }
            };
            return View(model);
        }

        public ViewResult ListGPU(int page = 1)
        {
            GPUListViewModel model = new GPUListViewModel
            {
                GPUs = GPURepository.GPUs
                .OrderBy(x=>x.Name)
                .Skip((page-1)*PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = GPURepository.GPUs.Count()
                }
            };
            return View(model);
        }

        public ViewResult ListGame(int page = 1)
        {
            GameListViewModel model = new GameListViewModel
            {
                Games = gameRepository.Games
                .OrderBy(x => x.Name)
                .Skip((page - 1) * PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = gameRepository.Games.Count()
                }
            };
            return View(model);
        }
    }
}
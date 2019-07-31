using AutoMapper;
using PCbuilder_ASP.MVC_.Domain.Abstract;
using PCbuilder_ASP.MVC_.Domain.Entities;
using PCbuilder_ASP.MVC_.Services.DTO;
using PCbuilder_ASP.MVC_.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCbuilder_ASP.MVC_.Services.Services
{
    public class ShowService : IShowService
    {
        public int PageSize
        {
            get { return 10; }
            set { PageSize = value; }
        }

        IMapper Mapper;
        IUnitOfWork uow;
        IGenericRepository<CPU> CPURepository;
        IGenericRepository<GPU> GPURepository;
        IGenericRepository<Game> GameRepository;

        public ShowService(
            IUnitOfWork unitOfWork,
            IGenericRepository<CPU> cpuRepository,
            IGenericRepository<GPU> gpuRepository,
            IGenericRepository<Game> gameRepository,
            IMapper mapper)
        {
            uow = unitOfWork;
            CPURepository = cpuRepository;
            GPURepository = gpuRepository;
            GameRepository = gameRepository;
            Mapper = mapper;
        }

        public CPUListDTO ListCPU(int page = 1)
        {
            var count = CPURepository.Get().Count();
            //var countToGet = count - ((page - 1) * PageSize);
            //var take = countToGet < PageSize ? countToGet : PageSize;
           
            var cpus = CPURepository
                .Get()
                .OrderBy(x => x.ProcessorNumber)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);
            var cpusdto = 
                Mapper.Map<IEnumerable<CPU>, IEnumerable<CPUdto>>(cpus.ToList());
            CPUListDTO cpuList = new CPUListDTO
            {
                CPUList = cpusdto,
                PagingInfo = new PagingInfoDTO{
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = CPURepository.Get().Count()
                }
            };

            return cpuList;
        }

        public GameListDTO ListGame(int page = 1)
        {
            var games = GameRepository
                .Get()
                .OrderBy(x => x.Name)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);
            var gamesDTO = 
                Mapper.Map<IEnumerable<Game>, IEnumerable<GameDTO>>(games.ToList());
            GameListDTO gameList = new GameListDTO
            {
                GameList = gamesDTO,
                PagingInfo = new PagingInfoDTO
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = GameRepository.Get().Count()
                }
            };

            return gameList;
        }

        public GPUListDTO ListGPU(int page = 1)
        {
            var gpus = GPURepository
                .Get()
                .OrderBy(x => x.Name)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);
            var gpusdto = Mapper.Map<IEnumerable<GPU>, IEnumerable<GPUdto>>(gpus.ToList());
            GPUListDTO cpuList = new GPUListDTO
            {
                GPUList = gpusdto,
                PagingInfo = new PagingInfoDTO
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = GPURepository.Get().Count()
                }
            };

            return cpuList;
        }
    }
}

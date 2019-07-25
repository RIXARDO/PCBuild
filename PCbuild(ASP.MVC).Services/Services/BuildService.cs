using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCbuild_ASP.MVC_.Services.DTO;
using PCbuild_ASP.MVC_.Services.Interfaces;
using PCbuild_ASP.MVC_.Domain.Abstract;
using PCbuild_ASP.MVC_.Domain.Entities;
using AutoMapper;

namespace PCbuild_ASP.MVC_.Services.Services
{
    public class BuildService : IBuildService
    {
        IGenericRepository<BuildEntity> BuildRepository;
        IGenericRepository<CPU> CPUs;
        IGenericRepository<Game> Games;
        IGenericRepository<GPU> GPUs;

        IMapper mapper;
        IUnitOfWork uow;


        public BuildService(IUnitOfWork unitOfWork, IGenericRepository<BuildEntity> buildRepository, IGenericRepository<GPU> gpus, IGenericRepository<CPU> cpus, IGenericRepository<Game> games)//IMapper mapper
        {
            uow = unitOfWork;
            BuildRepository = buildRepository;
            CPUs = cpus;
            GPUs = gpus;
            Games = games;

            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CPU, CPUdto>();
                cfg.CreateMap<GPU, GPUdto>();
                cfg.CreateMap<Game, GameDTO>();
                cfg.CreateMap<BuildEntity, BuildEntityDTO>();
                cfg.CreateMap<BuildEntityDTO, BuildEntity>();
            });

            mapper = new Mapper(configuration);
        }

        public BuildResultDTO Action(Guid cpu, Guid gpu, ResolutionDTO resolution)
        {
            try
            {

                if (cpu != null & gpu != null)
                {
                    BuildResultDTO buildResult = new BuildResultDTO
                    {
                        BuildGames = new List<BuildGameDTO>()
                    };
                    float CPUbench = CPUs.FindById(cpu).AverageBench / 100f;
                    float GPUbench = GPUs.FindById(gpu).AverageBench / 100f;
                    float ScreenRezConf =
                        (resolution == ResolutionDTO.res1080) ? 1 : ((resolution == ResolutionDTO.res1440) ? 0.75f : 0.5f);
                    float fp = 120 * CPUbench * GPUbench * ScreenRezConf;

                    foreach (Game game in Games.Get().ToList())
                    {
                        float fps = fp / (game.AverangeRequirements / 100f);
                        buildResult.BuildGames.Add(
                            new BuildGameDTO { GameDTO = mapper.Map<Game, GameDTO>(game), FPS = (int)fps });
                    }

                    buildResult.Build = new BuildEntityDTO
                    {
                        //Doubtfully
                        CPU = mapper.Map<CPU, CPUdto>(CPUs.FindById(cpu)),
                        GPU = mapper.Map<GPU, GPUdto>(GPUs.FindById(gpu))
                    };

                    return buildResult;
                }
                //doubtfully
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
                return null;
                //ex.Message;
            }
        }

        public void DeleteBuild(Guid guid)
        {
            try
            {
                var entry = BuildRepository.FindById(guid);
                BuildRepository.Delete(entry);
                uow.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EditBuild(BuildEntityDTO build)
        {
            try
            {
                var entity = mapper.Map<BuildEntityDTO, BuildEntity>(build);
                BuildRepository.Update(entity);
                uow.Save();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);//Add Log
            }
        }

        public IEnumerable<BuildEntityDTO> GetBuilds(string UserID)
        {
            try
            {
                var entry = mapper.Map<IEnumerable<BuildEntity>, IEnumerable<BuildEntityDTO>>(BuildRepository.Get(x => x.UserID == UserID));
                return entry;
            }
            catch (Exception ex)
            {
                throw ex;
                //Add Log
                return null;
            }
        }

        public IEnumerable<CPUItemDTO> GetCPUsByManufacture(string Manufacture)
        {
            try
            {
                var cpus = CPUs
                    .Get(x => x.Manufacture == Manufacture)
                    .Select(x => new CPUItemDTO { ProductGuid = x.ProductGuid, ProcessorNumber = x.ProcessorNumber });

                return cpus;
            }
            catch (Exception ex)
            {
                throw ex;
                //Add Log
                //return null;
            }
        }

        public IEnumerable<GPUItemDTO> GetGPUsByDeveloper(string Developer)
        {
            try
            {
                var gpus = GPUs
                    .Get(x => x.Developer == Developer)
                    .Select(x => new GPUItemDTO { ProductGuid = x.ProductGuid, Name = x.Name });
                return gpus;
            }
            catch (Exception ex)
            {
                throw ex;
                //   return null;
            }
        }

        public void SaveBuild(BuildEntityDTO build)
        {
            var entry = mapper.Map<BuildEntityDTO, BuildEntity>(build);
            BuildRepository.Create(entry);
            uow.Save();
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    uow.Save();
                    uow.Dispose();
                }
                disposed = true;
            }
        }

        public BuildEntityDTO GetBuildById(Guid BuildGuid)
        {
            var build = BuildRepository.FindById(BuildGuid);
            BuildEntityDTO buildDTO = mapper.Map<BuildEntity, BuildEntityDTO>(build);
            return buildDTO;
        }

        public FileDTO GetImage(Guid gameGuid)
        {
            Game game = Games.FindById(gameGuid);
            if (game != null & game.ImageMimeType64 != null)
            {
                return new FileDTO
                {
                    File = game.ImageData64,
                    FileType = game.ImageMimeType64
                };
            }
            else
            {
                return null;
            }
        }
    }
}

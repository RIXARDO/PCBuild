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
                    float ScreenRezConf = (resolution == ResolutionDTO.res1080) ? 1 : ((resolution == ResolutionDTO.res1440) ? 0.75f : 0.5f);
                    float fp = 120 * CPUbench * GPUbench * ScreenRezConf;

                    foreach (Game game in Games.Get())
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
            }catch(Exception ex)
            {
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
            }catch(Exception ex)
            {
                
            }
        }

        public void EditBuild(BuildEntityDTO build)
        {
            try
            {
                var entity = mapper.Map<BuildEntityDTO, BuildEntity>(build);
                BuildRepository.Update(entity);
            }
            catch(Exception ex)
            {
                System.Console.WriteLine(ex.Message);//Add Log
            }
        }

        public IEnumerable<BuildEntityDTO> GetBuilds()
        {
            try
            {
                var entry = mapper.Map<IEnumerable<BuildEntity>, IEnumerable<BuildEntityDTO>>(BuildRepository.Get());
                return entry;
            }
            catch(Exception ex)
            {
                //Add Log
                return null;
            }
        }

        public IEnumerable<CPUdto> GetCPUsByManufacture(string Manufacture)
        {
            try
            {
                var cpus = mapper.Map<IEnumerable<CPU>, IEnumerable<CPUdto>>(CPUs.Get(x => x.Manufacture == Manufacture));
                return cpus;
            }
            catch(Exception ex)
            {
                //Add Log
                return null;
            }
        }

        public IEnumerable<GPUdto> GetGPUsByDeveloper(string Developer)
        {
            try
            {
                var gpus = mapper.Map<IEnumerable<GPU>, IEnumerable<GPUdto>>(GPUs.Get(x => x.Developer == Developer));
                return gpus;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public void SaveBuild(BuildEntityDTO build)
        {
                var entry = mapper.Map<BuildEntityDTO, BuildEntity>(build);
                BuildRepository.Create(entry);   
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
    }
}

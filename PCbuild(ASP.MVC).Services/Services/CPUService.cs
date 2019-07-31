using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCbuilder_ASP.MVC_.Services.DTO;
using PCbuilder_ASP.MVC_.Services.Interfaces;
using PCbuilder_ASP.MVC_.Domain.Abstract;
using PCbuilder_ASP.MVC_.Domain.Entities;
using AutoMapper;

namespace PCbuilder_ASP.MVC_.Services.Services
{
    public class CPUService : ICPUService, IDisposable
    {
        private IGenericRepository<CPU> CPUs { get; set; }
        private IUnitOfWork uow { get; set; }
        private IMapper mapper;

        public CPUService(IUnitOfWork unitOfWork, IGenericRepository<CPU> repository)
            //IMapper mapper
        {
            uow = unitOfWork;
            CPUs = repository;
            mapper = new Mapper(new MapperConfiguration(cfg => {
                cfg.CreateMap<CPUdto, CPU>();
                cfg.CreateMap<CPU, CPUdto>();
                }));
        }

        public void DeleteCPU(Guid guid)
        {
            var cpu = CPUs.FindById(guid);
            CPUs.Delete(cpu);
        }

        public void EditCPU(CPUdto cpudto)
        {
            
            CPU cpu = mapper.Map<CPUdto, CPU>(cpudto);
            CPUs.Update(cpu);
        }

        public CPUdto GetCPUByID(Guid guid)
        {
            CPU cpu = CPUs.FindById(guid);
            CPUdto cpudto = mapper.Map<CPU, CPUdto>(cpu);
            return cpudto;
        }

        public IEnumerable<CPUdto> GetCPUs()
        {
            return mapper.Map<IEnumerable<CPU>, IEnumerable<CPUdto>>(CPUs.Get().ToList());
        }

        public void SaveCPU(CPUdto cpudto)
        {
            CPU cpu = mapper.Map<CPUdto, CPU>(cpudto);
            CPUs.Create(cpu);
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

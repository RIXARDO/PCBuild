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
    public class GPUService : IGPUService, IDisposable
    {

        private IUnitOfWork uow { get; set; }
        IGenericRepository<GPU> GPUs { get; set; }
        private IMapper mapper;

        public GPUService(IUnitOfWork unitOfWork, IGenericRepository<GPU> repository) 
        {
            uow = unitOfWork;
            GPUs = repository;
            mapper = new Mapper(new MapperConfiguration( cfg => {
                cfg.CreateMap<GPU, GPUdto>();
                cfg.CreateMap<GPUdto, GPU>();
            }));
        }

        public void DeleteGPU(Guid guid)
        {
            GPU gpu = GPUs.FindById(guid);
            GPUs.Delete(gpu);
        }

        public void EditGPU(GPUdto gpudto)
        {
            GPU gpu = mapper.Map<GPUdto, GPU>(gpudto);
            GPUs.Update(gpu);
        }

        public GPUdto GetGPUByID(Guid guid)
        {
            GPU gpu = GPUs.FindById(guid);
            GPUdto gpudto = mapper.Map<GPU, GPUdto>(gpu);
            return gpudto;
        }

        public IEnumerable<GPUdto> GetGPUs()
        {
            IEnumerable<GPU> gpus = GPUs.Get();
            IEnumerable<GPUdto> gpudtos = 
                mapper.Map<IEnumerable<GPU>, IEnumerable<GPUdto>>(gpus);
            return gpudtos;
        }

        public void SaveGPU(GPUdto gpudto)
        {
            GPU gpu = mapper.Map<GPUdto, GPU>(gpudto);
            GPUs.Create(gpu);
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

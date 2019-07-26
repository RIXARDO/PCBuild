using AutoMapper;
using PCbuild_ASP.MVC_.Domain.Abstract;
using PCbuild_ASP.MVC_.Domain.Entities;
using PCbuild_ASP.MVC_.Services.DTO;
using PCbuild_ASP.MVC_.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCbuild_ASP.MVC_.Services.Services
{
    public class CompareService : ICompareService
    {
        IUnitOfWork uow;
        IGenericRepository<CPU> CPURepositroy;
        IGenericRepository<GPU> GPURepository;
        IMapper Mapper; 

        public CompareService(IUnitOfWork unitOfWork, IGenericRepository<CPU> cpuRepository, IGenericRepository<GPU> gpuRepository, IMapper mapper)
        {
            uow = unitOfWork;
            CPURepositroy = cpuRepository;
            GPURepository = gpuRepository;
            Mapper = mapper;
        }

        public CPUdto FindCPUByID(Guid guid)
        {
            var cpu = CPURepositroy.FindById(guid);
            var cpudto = Mapper.Map<CPU, CPUdto>(cpu);
            return cpudto;
        }

        public GPUdto FindGPUByID(Guid guid)
        {
            var gpu = GPURepository.FindById(guid);
            var gpudto = Mapper.Map<GPU, GPUdto>(gpu);
            return gpudto;
        }
    }
}

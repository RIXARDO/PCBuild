using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;
using PCbuild_ASP.MVC_.Domain.Entities;
using PCbuild_ASP.MVC_.Services.DTO;

namespace PCbuild_ASP.MVC_.Tests
{
    [TestClass]
    public class AutoMapperTest
    {
        IMapper mapper;

        [TestInitialize]
        public void Init()
        {
            mapper = new Mapper(new MapperConfiguration(cfg => {
                cfg.CreateMap<CPU, CPUdto>().ReverseMap();
                cfg.CreateMap<GPU, GPUdto>().ReverseMap();
                cfg.CreateMap<Price,PriceDTO>().ReverseMap();
            }));
        }

        [TestMethod]
        public void HierarchyTest()
        {
            //Arrange
            Guid guid = Guid.NewGuid();

            CPU cpu = new CPU() { ProductGuid = guid, Status="OK", Price = new Price { Amount = 1000 } };
            GPU gpu = new GPU() { ProductGuid = guid, Status = "OK", Price = new Price { Amount = 1000 } };

            //Act
            //DTO for CPU
            CPUdto dto = mapper.Map<CPU, CPUdto>(cpu);
            //DTO for GPU
            GPUdto gdto = mapper.Map<GPU, GPUdto>(gpu);  

            //Assert
            Assert.AreEqual(dto.Price.Amount, 1000, "wrong Price!");
            Assert.AreEqual(dto.ProductGuid, guid, "wrong Product Guid!");
            Assert.AreEqual(dto.Status, "OK", "wrong Status");

            Assert.AreEqual(gdto.Price.Amount, 1000, "wrong Price!");
            Assert.AreEqual(gdto.ProductGuid, guid, "wrong Product Guid!");
            Assert.AreEqual(gdto.Status, "OK", "wrong Status");
        }

        [TestMethod]
        public void ReversMapTest()
        {
            //Arrange
            Guid guid = Guid.NewGuid();

            CPU cpu = new CPU() { ProductGuid = guid, AverageBench = 1, Status = "OK", Price = new Price { Amount = 1000 } };
            

            //Act
            //DTO for CPU
            CPUdto dto = mapper.Map<CPU, CPUdto>(cpu);
            dto.AverageBench = 100;
            dto.Price.Amount = 100;
            mapper.Map(dto, cpu);

            //Assert
            Assert.AreEqual(cpu.AverageBench,100);

            Assert.AreEqual(cpu.Price.Amount, 100);
        }
    }
}

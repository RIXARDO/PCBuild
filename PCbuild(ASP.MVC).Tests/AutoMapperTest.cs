using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;
using PCbuild_ASP.MVC_.Domain.Entities;
using PCbuild_ASP.MVC_.Services.DTO;
using PCbuild_ASP.MVC_.Services.Util;
using PCbuild_ASP.MVC_.Util;
using System.Collections.Generic;
using PCbuild_ASP.MVC_.Models;

namespace PCbuild_ASP.MVC_.Tests
{
    [TestClass]
    public class AutoMapperTest
    {
        IMapper mapper;

        [TestInitialize]
        public void Init()
        {
            mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperServicesProfile>();
                cfg.AddProfile<AutoMapperPresentationProfile>();
            }));
        }

        [TestMethod]
        public void HierarchyTest()
        {
            //Arrange
            Guid guid = Guid.NewGuid();

            CPU cpu = new CPU() { ProductGuid = guid, Status = "OK", Price = new Price { Amount = 1000 } };
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
            Assert.AreEqual(cpu.AverageBench, 100);

            Assert.AreEqual(cpu.Price.Amount, 100);
        }

        [TestMethod]
        public void Mapping_MapBuildResultDTOtoBuildResult_PropertesOfEntitesSame()
        {
            //Arrange
            Guid guid = Guid.NewGuid();
            Guid CPUguid = Guid.NewGuid();
            Guid GPUguid = Guid.NewGuid();
            Guid GameGuid1 = Guid.NewGuid();
            Guid GameGuid2 = Guid.NewGuid();

            CPUdto cpudto =
                new CPUdto { ProductGuid = CPUguid, AverageBench = 100, ProcessorNumber = "NameCPU" };
            GPUdto gpudto =
                new GPUdto { ProductGuid = GPUguid, AverageBench = 101, Name = "NameGPU" };
            BuildEntityDTO build = new BuildEntityDTO { BuildEntityGuid = guid, CPU = cpudto, GPU = gpudto };

            GameDTO gameDTO1 =
                new GameDTO { GameGuid = GameGuid1, AverangeRequirements = 100, Name = "NameGame1" };
            GameDTO gameDTO2 =
                new GameDTO { GameGuid = GameGuid2, AverangeRequirements = 50, Name = "NameGame2" };
            List<BuildGameDTO> buildGameDTOs = new List<BuildGameDTO>
            {
                new BuildGameDTO{ FPS=100, GameDTO = gameDTO1 },
                new BuildGameDTO{ FPS=100, GameDTO = gameDTO2 }
            };

            BuildResultDTO resultDTO = new BuildResultDTO { Build = build, BuildGames = buildGameDTOs };
            //Act
            var result = mapper.Map<BuildResultDTO, BuildResult>(resultDTO);
            //Assert
            Assert.IsNotNull(
                result.BuildGames.Find(x => x.Game.GameGuid == GameGuid1), "Game guid1 not found");
            Assert.IsNotNull(
                result.BuildGames.Find(x => x.Game.GameGuid == GameGuid2), "Game guid2 not found");

            Assert.IsNotNull(
                result.BuildGames.Find(x => x.Game.Name == gameDTO1.Name), "Game Name1 not found");
            Assert.IsNotNull(
                result.BuildGames.Find(x => x.Game.Name == gameDTO2.Name), "Game Name2 not found");
            Assert.IsNotNull(
                result.BuildGames.Find(
                    x => x.Game.AverangeRequirements == gameDTO1.AverangeRequirements),
                "Game AverangeReq1 not found");
            Assert.IsNotNull(
                result.BuildGames.Find(
                    x => x.Game.AverangeRequirements == gameDTO2.AverangeRequirements),
                "Game AverangeReq2 not found");

            Assert.IsNotNull(result.BuildEntity, "BuildEntity is null");

            Assert.IsNotNull(result.BuildEntity.CPU, "BuildEntity CPU is null");
            Assert.IsNotNull(result.BuildEntity.GPU, "BuildEntity GPU is null");
        }
    }
}

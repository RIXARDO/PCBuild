using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PCbuilder_ASP.MVC_.Services.Services;
using PCbuilder_ASP.MVC_.Domain.Concrete;
using PCbuilder_ASP.MVC_.Domain.Abstract;
using PCbuilder_ASP.MVC_.Domain.Entities;
using PCbuilder_ASP.MVC_.Services.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;

namespace PCbuilder_ASP.MVC_.Tests.Services
{
    [TestClass]
    public class BuildServiceTest
    {
        Mock<EFUnitOfWork> uow = new Mock<EFUnitOfWork>();
        Mock<IGenericRepository<BuildEntity>> BuildRepository = 
            new Mock<IGenericRepository<BuildEntity>>();
        Mock<IGenericRepository<GPU>> GPUs = new Mock<IGenericRepository<GPU>>();
        Mock<IGenericRepository<CPU>> CPUs = new Mock<IGenericRepository<CPU>>();
        Mock<IGenericRepository<Game>> Games = new Mock<IGenericRepository<Game>>();
        BuildService buildService;



        [TestInitialize]
        public void Init()
        {
            Mapper.Reset();
            buildService = new BuildService(
                uow.Object, BuildRepository.Object, GPUs.Object, CPUs.Object, Games.Object);
        }
        [TestMethod]
        public void Action_CombineInputData_OutputRightResult()
        {
            //Arrange
            Guid cpuGuid = new Guid();
            Guid gpuGuid = new Guid();
            Guid gameGuid = new Guid();
            ResolutionDTO resolution = ResolutionDTO.res1080;

            GPUs.Setup(x => x.FindById(cpuGuid)).Returns(new GPU { AverageBench=100 });
            CPUs.Setup(x => x.FindById(gpuGuid)).Returns(new CPU { AverageBench=100});
            Games.Setup(x => x.FindById(gameGuid))
                .Returns(new Game { AverangeRequirements = 100 });
            Games.Setup(x=>x.Get()).Returns(new List<Game> {new Game { AverangeRequirements=100}}.AsQueryable());
            
            //Act
            var result = buildService.Action(cpuGuid,gpuGuid,resolution);

            //Assert
            Assert.AreEqual(result.BuildGames[0].FPS, 120);
        }

        [TestMethod]
        public void DeleteBuild_DeleteBuild_RepositoryMehtodWasCalled()
        {
            //Arrange
            BuildEntity entity = new BuildEntity();
            BuildRepository.Setup(x => x.FindById(It.IsAny<Guid>())).Returns(entity);
            //Act
            buildService.DeleteBuild(new Guid());
            //Assert
            BuildRepository.Verify(x => x.Delete(entity));
        }

        [TestMethod]
        public void EditBuild_EditBuildEntity_RepositoryMethodWasCalled()
        {
            //Arrange
            BuildEntityDTO entityDTO = new BuildEntityDTO();

            //Act
            buildService.EditBuild(entityDTO);

            //Assert
            BuildRepository.Verify(x => x.Update(It.IsAny<BuildEntity>()), "Not called");
        }

        [TestMethod]
        public void CreateBuild_CreateBuildEnity_RepositoryMethodWasCalled()
        {
            //Arrange
            BuildEntityDTO entityDTO = new BuildEntityDTO();

            //Act
            buildService.SaveBuild(entityDTO);

            //Assert
            BuildRepository.Verify(x => x.Create(It.IsAny<BuildEntity>()), "Not called");
        }

        [TestMethod]
        public void GetCPUsByManufacture_GetCPUsFromRepository_RightEntitysReturned()
        {
            //Arrange
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();
            List<CPU> cpuList = new List<CPU> {
                new CPU { ProductGuid=guid1, ProcessorNumber="ProcNum1"},
            new CPU { ProductGuid=guid2, ProcessorNumber="ProcNum2"},
            new CPU { ProductGuid=guid3, ProcessorNumber="ProcNum3"}};

            CPUs.Setup(x => x.Get(It.IsAny<Expression<Func<CPU,bool>>>()))
                .Returns(cpuList.AsQueryable());

            //Act
            var result = buildService.GetCPUsByManufacture("Manuf1");
            //Assert
            Assert.IsNotNull(result.Where(x=>x.ProductGuid==guid1), "guid1 is missing");
            Assert.IsNotNull(result.Where(x => x.ProductGuid == guid2), "guid2 is missing");
            Assert.IsNotNull(result.Where(x => x.ProductGuid == guid3), "guid3 is missing");

            Assert.IsNotNull(result
                .Where(x => x.ProcessorNumber == cpuList[0].ProcessorNumber),
                cpuList[0].ProcessorNumber+" is missing");
            Assert.IsNotNull(result
                .Where(x => x.ProcessorNumber == cpuList[1].ProcessorNumber),
                cpuList[0].ProcessorNumber+" is missing");
            Assert.IsNotNull(result
                .Where(x => x.ProcessorNumber == cpuList[2].ProcessorNumber),
                cpuList[0].ProcessorNumber+" is missing");
        }

        [TestMethod]
        public void GetGPUsByManufacture_GetGPUsFromRepository_RightEntitysReturned()
        {
            //Arrange
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();
            List<GPU> gpuList = new List<GPU> {
                new GPU { ProductGuid=guid1, Name="Name1"},
            new GPU { ProductGuid=guid2, Name="Name2"},
            new GPU { ProductGuid=guid3, Name="Name3"}};

            GPUs.Setup(x => x.Get(It.IsAny<Expression<Func<GPU, bool>>>()))
                .Returns(gpuList.AsQueryable());

            //Act
            var result = buildService.GetGPUsByDeveloper("Developer1");
            //Assert
            Assert.IsNotNull(result.Where(x => x.ProductGuid == guid1), "guid1 is missing");
            Assert.IsNotNull(result.Where(x => x.ProductGuid == guid2), "guid2 is missing");
            Assert.IsNotNull(result.Where(x => x.ProductGuid == guid3), "guid3 is missing");

            Assert.IsNotNull(result
                .Where(x => x.Name == gpuList[0].Name), gpuList[0].Name + "is missing");
            Assert.IsNotNull(result
                .Where(x => x.Name == gpuList[1].Name), gpuList[1].Name + " is missing");
            Assert.IsNotNull(result
                .Where(x => x.Name == gpuList[2].Name), gpuList[2].Name+" is missing");
        }
    }
}

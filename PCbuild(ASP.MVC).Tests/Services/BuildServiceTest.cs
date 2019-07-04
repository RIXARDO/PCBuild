using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PCbuild_ASP.MVC_.Services.Services;
using PCbuild_ASP.MVC_.Domain.Concrete;
using PCbuild_ASP.MVC_.Domain.Abstract;
using PCbuild_ASP.MVC_.Domain.Entities;
using PCbuild_ASP.MVC_.Services.DTO;
using System.Collections.Generic;
using AutoMapper;

namespace PCbuild_ASP.MVC_.Tests.Services
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
        public void ActionTest()
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
            Games.Setup(x=>x.Get()).Returns(new List<Game> {new Game { AverangeRequirements=100}});
            

            //Act
            var result = buildService.Action(cpuGuid,gpuGuid,resolution);

            //Assert
            Assert.AreEqual(result.BuildGames[0].FPS, 120);
        }

        [TestMethod]
        public void DeleteBuildTest()
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
        public void EditBuildTest()
        {
            //Arrange
            BuildEntityDTO entityDTO = new BuildEntityDTO();

            //Act
            buildService.EditBuild(entityDTO);

            //Assert
            BuildRepository.Verify(x => x.Update(It.IsAny<BuildEntity>()), "Not called");
        }

        [TestMethod]
        public void CreateBuildTest()
        {
            //Arrange
            BuildEntityDTO entityDTO = new BuildEntityDTO();

            //Act
            buildService.SaveBuild(entityDTO);

            //Assert
            BuildRepository.Verify(x => x.Create(It.IsAny<BuildEntity>()), "Not called");
        }
    }
}

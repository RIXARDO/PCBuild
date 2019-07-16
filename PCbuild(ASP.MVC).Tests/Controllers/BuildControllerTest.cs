using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;
using PCbuild_ASP.MVC_.Services.Interfaces;
using PCbuild_ASP.MVC_.Util;
using Moq;
using PCbuild_ASP.MVC_.Controllers;
using System.Web.Mvc;
using PCbuild_ASP.MVC_.Services.DTO;
using PCbuild_ASP.MVC_.Models;
using PCbuild_ASP.MVC_.Models.ViewModel;

namespace PCbuild_ASP.MVC_.Tests.Controllers
{
    /// <summary>
    /// Summary description for BuildControllerTest
    /// </summary>
    [TestClass]
    public class BuildControllerTest
    {
        IMapper Mapper;
        Mock<IBuildService> Service= new Mock<IBuildService>();

        public BuildControllerTest()
        {
            Mapper = new Mapper(
                new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperPresentationProfile>()));
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Index_Output_RightOutput()
        {
            //Arrange
            Guid guid = Guid.NewGuid();

            BuildController controller = new BuildController(Service.Object, Mapper);
            //Act
            var result = controller.Index() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Action_OutputPartialView_RightOutput()
        {
            //Arrange
            ResolutionDTO resolution = ResolutionDTO.res1080;
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

            Service.Setup(x => x.Action(CPUguid, GPUguid, resolution)).Returns(resultDTO);

            BuildController controller = new BuildController(Service.Object, Mapper);
            //Act
            var result = 
                controller.Action((ResolutionEnum)resolution, 
                CPUguid, GPUguid) as PartialViewResult;
            //Assert
            BuildResult buildResult = (BuildResult) result.Model;

            Assert.IsNotNull(
                buildResult.BuildGames.Find(x => x.Game.GameGuid == GameGuid1), "Game guid1 not found");
            Assert.IsNotNull(
                buildResult.BuildGames.Find(x => x.Game.GameGuid == GameGuid2), "Game guid2 not found");

            Assert.IsNotNull(
                buildResult.BuildGames.Find(x => x.Game.Name == gameDTO1.Name), "Game Name1 not found");
            Assert.IsNotNull(
                buildResult.BuildGames.Find(x => x.Game.Name == gameDTO2.Name), "Game Name2 not found");
            Assert.IsNotNull(
                buildResult.BuildGames.Find(
                    x => x.Game.AverangeRequirements == gameDTO1.AverangeRequirements),
                "Game AverangeReq1 not found");
            Assert.IsNotNull(
                buildResult.BuildGames.Find(
                    x => x.Game.AverangeRequirements == gameDTO2.AverangeRequirements),
                "Game AverangeReq2 not found");

            Assert.IsNotNull(buildResult.BuildEntity, "BuildEntity is null");

            Assert.IsNotNull(buildResult.BuildEntity.CPU, "BuildEntity CPU is null");
            Assert.IsNotNull(buildResult.BuildEntity.GPU, "BuildEntity GPU is null");
        }

        [TestMethod]
        public void Builds_DisplayAllBuilds_RightDisplay()
        {
            Guid UserID = Guid.NewGuid();

            Guid BuildGuid1 = Guid.NewGuid();
            Guid BuildGuid2 = Guid.NewGuid();
            Guid BuildGuid3 = Guid.NewGuid();

            Guid CPUGuid1 = Guid.NewGuid();
            Guid CPUGuid2 = Guid.NewGuid();
            Guid CPUGuid3 = Guid.NewGuid();

            Guid GPUGuid1 = Guid.NewGuid();
            Guid GPUGuid2 = Guid.NewGuid();
            Guid GPUGuid3 = Guid.NewGuid();

            CPUdto cpudto1 = new CPUdto { ProductGuid = CPUGuid1, ProcessorNumber = "CPUName1" };
            CPUdto cpudto2 = new CPUdto { ProductGuid = CPUGuid2, ProcessorNumber = "CPUName2" };
            CPUdto cpudto3 = new CPUdto { ProductGuid = CPUGuid3, ProcessorNumber = "CPUName3" };

            GPUdto gpudto1 = new GPUdto { ProductGuid = GPUGuid1, Name = "GPUName1" };
            GPUdto gpudto2 = new GPUdto { ProductGuid = GPUGuid2, Name = "GPUName2" };
            GPUdto gpudto3 = new GPUdto { ProductGuid = GPUGuid3, Name = "GPUName3" };

            List<BuildEntityDTO> buildEntityList = new List<BuildEntityDTO>
            {
                new BuildEntityDTO{ BuildEntityGuid = BuildGuid1, CPU = cpudto1, GPU = gpudto1},
                new BuildEntityDTO{BuildEntityGuid = BuildGuid2, CPU = cpudto2, GPU =  gpudto2},
                new BuildEntityDTO{BuildEntityGuid = BuildGuid3, CPU = cpudto3, GPU = gpudto3}
            };

            //Arrange
            Service.Setup(x => x.GetBuilds(null)).Returns(buildEntityList);
            BuildController controller = new BuildController(Service.Object, Mapper);

            //Act
            var result = controller.Builds() as ViewResult;
            //Assert
            List<BuildEntityViewModel> builds = (List<BuildEntityViewModel>)result.Model;

            Assert.IsNotNull(builds.Find(x => x.BuildEntityGuid == BuildGuid1),"Build guid 1");
            Assert.IsNotNull(builds.Find(x => x.BuildEntityGuid == BuildGuid2), "Build guid 2");
            Assert.IsNotNull(builds.Find(x => x.BuildEntityGuid == BuildGuid3), "Build guid 3");

            Assert.IsNotNull(builds.Find(x => x.CPU.ProductGuid == CPUGuid1), "CPU guid 1");
            Assert.IsNotNull(builds.Find(x => x.CPU.ProductGuid == CPUGuid2), "CPU guid 2");
            Assert.IsNotNull(builds.Find(x => x.CPU.ProductGuid == CPUGuid3), "CPU guid 3");

            Assert.IsNotNull(builds.Find(x => x.GPU.ProductGuid == GPUGuid1), "GPU guid 1");
            Assert.IsNotNull(builds.Find(x => x.GPU.ProductGuid == GPUGuid2), "GPU guid 2");
            Assert.IsNotNull(builds.Find(x => x.GPU.ProductGuid == GPUGuid3), "GPU guid 3");

            Assert.IsNotNull(builds.Find(x => x.CPU.ProcessorNumber == cpudto1.ProcessorNumber),
                 "CPU name 1");
            Assert.IsNotNull(builds.Find(x => x.CPU.ProcessorNumber == cpudto2.ProcessorNumber),
                "CPU name 2");
            Assert.IsNotNull(builds.Find(x => x.CPU.ProcessorNumber == cpudto3.ProcessorNumber),
                "CPU name 3");

            Assert.IsNotNull(builds.Find(x => x.GPU.Name == gpudto1.Name), "GPU name 1");
            Assert.IsNotNull(builds.Find(x => x.GPU.Name == gpudto2.Name), "GPU name 2");
            Assert.IsNotNull(builds.Find(x => x.GPU.Name == gpudto3.Name), "GPU name 3");
        }
    }
}

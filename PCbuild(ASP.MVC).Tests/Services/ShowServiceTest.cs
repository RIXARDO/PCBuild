using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PCbuild_ASP.MVC_.Domain.Abstract;
using Moq;
using PCbuild_ASP.MVC_.Domain.Entities;
using AutoMapper;
using PCbuild_ASP.MVC_.Services.Util;
using PCbuild_ASP.MVC_.Services.Services;
using System.Linq;
using System.Linq.Expressions;

namespace PCbuild_ASP.MVC_.Tests.Services
{
    /// <summary>
    /// Summary description for ShowServiceTest
    /// </summary>
    [TestClass]
    public class ShowServiceTest
    {
        Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
        Mock<IGenericRepository<CPU>> CPUs = new Mock<IGenericRepository<CPU>>();
        Mock<IGenericRepository<GPU>> GPUs = new Mock<IGenericRepository<GPU>>();
        Mock<IGenericRepository<Game>> Games = new Mock<IGenericRepository<Game>>();
        IMapper Mapper;

        public ShowServiceTest()
        {
            Mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperServicesProfile>();
            }));

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
        public void CPUList_ReturnCPUListWithPagingInfo_ReturnRightCountOfList()
        {
            //Arrange
            ShowService service = 
                new ShowService(
                    uow.Object, 
                    CPUs.Object, 
                    GPUs.Object, 
                    Games.Object, 
                    Mapper);
            var cpuList = new List<CPU>();
            for(var i = 0; i < 16; i++)
            {
                cpuList.Add(
                    new CPU {ProductGuid=Guid.NewGuid(), ProcessorNumber=i.ToString() });
            }

            CPUs.Setup(x => x.Get()).Returns(cpuList.AsQueryable());
            //Act
            var result = service.ListCPU(2);
            //Assert
            Assert.AreEqual(6, result.CPUList.Count());
        }

        [TestMethod]
        public void CPUList_ReturnCPUListWhenCountLessThanPageSize_ReturnRightCountOfList()
        {
            //Arrange
            ShowService service =
                new ShowService(
                    uow.Object,
                    CPUs.Object,
                    GPUs.Object,
                    Games.Object,
                    Mapper);
            var cpuList = new List<CPU>();
            for (var i = 0; i < 6; i++)
            {
                cpuList.Add(
                    new CPU {
                        ProductGuid = Guid.NewGuid(),
                        ProcessorNumber = i.ToString() });
            }

            CPUs.Setup(x => x.Get()).Returns(cpuList.AsQueryable());
            //Act
            var result = service.ListCPU(1);
            //Assert
            Assert.AreEqual(6, result.CPUList.Count());
        }

        [TestMethod]
        public void CPUList_ReturnCPUListWhenQuanityMoreThanPageSize_RightReturn()
        {
            //Arrange
            ShowService service =
                new ShowService(
                    uow.Object,
                    CPUs.Object,
                    GPUs.Object,
                    Games.Object,
                    Mapper);
            var cpuList = new List<CPU>();
            for (var i = 0; i < 16; i++)
            {
                cpuList.Add(
                    new CPU {
                        ProductGuid = Guid.NewGuid(),
                        ProcessorNumber = i.ToString() });
            }
            var f = cpuList.Take(100);
            CPUs.Setup(x => x.Get()).Returns(cpuList.AsQueryable());
            //Act
            var result = service.ListCPU(2);
            //Assert
            Assert.AreEqual(6, result.CPUList.Count());
        }


        [TestMethod]
        public void GPUList_ReturnGPUListWithPagingInfo_ReturnRightCountOfList()
        {
            //Arrange
            ShowService service =
                new ShowService(
                    uow.Object,
                    CPUs.Object,
                    GPUs.Object,
                    Games.Object,
                    Mapper);
            var gpuList = new List<GPU>();
            for (var i = 0; i < 16; i++)
            {
                gpuList.Add(
                    new GPU { ProductGuid = Guid.NewGuid(), Name = i.ToString() });
            }

            GPUs.Setup(x => x.Get()).Returns(gpuList.AsQueryable());
            //Act
            var result = service.ListGPU(2);
            //Assert
            Assert.AreEqual(6, result.GPUList.Count());
        }

        [TestMethod]
        public void GPUList_ReturnGPUListWhenCountLessThanPageSize_ReturnRightCountOfList()
        {
            //Arrange
            ShowService service =
                new ShowService(
                    uow.Object,
                    CPUs.Object,
                    GPUs.Object,
                    Games.Object,
                    Mapper);
            var gpuList = new List<GPU>();
            for (var i = 0; i < 6; i++)
            {
                gpuList.Add(
                    new GPU
                    {
                        ProductGuid = Guid.NewGuid(),
                        Name = i.ToString()
                    });
            }

            GPUs.Setup(x => x.Get()).Returns(gpuList.AsQueryable());
            //Act
            var result = service.ListGPU(1);
            //Assert
            Assert.AreEqual(6, result.GPUList.Count());
        }

        [TestMethod]
        public void GPUList_ReturnGPUListWhenQuanityMoreThanPageSize_RightReturn()
        {
            //Arrange
            ShowService service =
                new ShowService(
                    uow.Object,
                    CPUs.Object,
                    GPUs.Object,
                    Games.Object,
                    Mapper);
            var gpuList = new List<GPU>();
            for (var i = 0; i < 100; i++)
            {
                gpuList.Add(
                    new GPU
                    {
                        ProductGuid = Guid.NewGuid(),
                        Name = i.ToString()
                    });
            }

            GPUs.Setup(x => x.Get()).Returns(gpuList.AsQueryable());
            //Act
            var result = service.ListGPU(2);
            //Assert
            Assert.AreEqual(10, result.GPUList.Count());
        }

        [TestMethod]
        public void GameList_ReturnGameListWithPagingInfo_ReturnRightCountOfList()
        {
            //Arrange
            ShowService service =
                new ShowService(
                    uow.Object,
                    CPUs.Object,
                    GPUs.Object,
                    Games.Object,
                    Mapper);
            var gameList = new List<Game>();
            for (var i = 0; i < 16; i++)
            {
                gameList.Add(
                    new Game { GameGuid = Guid.NewGuid(), Name = i.ToString() });
            }

            Games.Setup(x => x.Get()).Returns(gameList.AsQueryable());
            //Act
            var result = service.ListGame(2);
            //Assert
            Assert.AreEqual(6, result.GameList.Count());
        }

        [TestMethod]
        public void GameList_ReturnGameListWhenCountLessThanPageSize_ReturnRightCountOfList()
        {
            //Arrange
            ShowService service =
                new ShowService(
                    uow.Object,
                    CPUs.Object,
                    GPUs.Object,
                    Games.Object,
                    Mapper);
            var gameList = new List<Game>();
            for (var i = 0; i < 6; i++)
            {
                gameList.Add(
                    new Game
                    {
                        GameGuid = Guid.NewGuid(),
                        Name = i.ToString()
                    });
            }

            Games.Setup(x => x.Get()).Returns(gameList.AsQueryable());
            //Act
            var result = service.ListGame(1);
            //Assert
            Assert.AreEqual(6, result.GameList.Count());
        }

        [TestMethod]
        public void GameList_ReturnGameListWhenQuanityMoreThanPageSize_RightReturn()
        {
            //Arrange
            ShowService service =
                new ShowService(
                    uow.Object,
                    CPUs.Object,
                    GPUs.Object,
                    Games.Object,
                    Mapper);
            var gameList = new List<Game>();
            for (var i = 0; i < 100; i++)
            {
                gameList.Add(
                    new Game
                    {
                        GameGuid = Guid.NewGuid(),
                        Name = i.ToString()
                    });
            }

            Games.Setup(x => x.Get()).Returns(gameList.AsQueryable());
            //Act
            var result = service.ListGame(2);
            //Assert
            Assert.AreEqual(10, result.GameList.Count());
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PCbuild_ASP.MVC_.Domain.Abstract;
using PCbuild_ASP.MVC_.Domain.Entities;
using AutoMapper;
using PCbuild_ASP.MVC_.Controllers;
using PCbuild_ASP.MVC_.Services.Services;
using PCbuild_ASP.MVC_.Services.Util;

namespace PCbuild_ASP.MVC_.Tests.Services
{
    /// <summary>
    /// Summary description for CompareSreviceTest
    /// </summary>
    [TestClass]
    public class CompareSreviceTest
    {
        Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
        Mock<IGenericRepository<CPU>> CPUs = new Mock<IGenericRepository<CPU>>();
        Mock<IGenericRepository<GPU>> GPUs = new Mock<IGenericRepository<GPU>>();
        Mock<IGenericRepository<Game>> Games = new Mock<IGenericRepository<Game>>();

        IMapper Mapper;
        CompareService Service;

        public CompareSreviceTest()
        {
            Mapper = 
                new Mapper(
                new MapperConfiguration(
                    cfg => cfg.AddProfile<AutoMapperServicesProfile>()));
        }

        [TestInitialize]
        public void TestInit()
        {
            Service = new CompareService(uow.Object, CPUs.Object, GPUs.Object, Mapper);
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
        public void FindCPUByID_GetCPUEntity_ReturnRightCPU()
        {
            //Arrange
            Guid guid = Guid.NewGuid();

            CPU cpu = new CPU
            {
                ProductGuid = guid,
                ProcessorNumber = "ProcNumb",
                Manufacture = "Manufacture1"
            };

            CPUs.Setup(x => x.FindById(guid)).Returns(cpu);

            //Act
            var result = Service.FindCPUByID(guid);
            //Assert
            Assert.AreEqual(cpu.ProductGuid, result.ProductGuid);
            Assert.AreEqual(cpu.ProcessorNumber, result.ProcessorNumber);
            Assert.AreEqual(cpu.Manufacture, result.Manufacture);
        }

        [TestMethod]
        public void FindGPUByID_GetGPUEntity_ReturnRightGPU()
        {
            //Arrange
            Guid guid = Guid.NewGuid();

            GPU gpu = new GPU
            {
                ProductGuid = guid,
                Name = "Name1",
                Manufacture = "Manufacture1",
                Developer = "Developer1"
            };

            GPUs.Setup(x => x.FindById(guid)).Returns(gpu);

            //Act
            var result = Service.FindGPUByID(guid);
            //Assert
            Assert.AreEqual(gpu.ProductGuid, result.ProductGuid);
            Assert.AreEqual(gpu.Name, result.Name);
            Assert.AreEqual(gpu.Manufacture, result.Manufacture);
            Assert.AreEqual(gpu.Developer, result.Developer);
        }
    }
}

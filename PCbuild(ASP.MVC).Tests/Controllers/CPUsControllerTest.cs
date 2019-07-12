using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PCbuild_ASP.MVC_.Controllers;
using PCbuild_ASP.MVC_.Services.Interfaces;
using AutoMapper;
using Moq;
using PCbuild_ASP.MVC_.Services.DTO;
using Ninject;
using Ninject.Modules;
using PCbuild_ASP.MVC_.Services.Util;
using PCbuild_ASP.MVC_.Util;
using System.Web.Mvc;
using PCbuild_ASP.MVC_.Models.ViewModel;
using System.Linq;

namespace PCbuild_ASP.MVC_.Tests.Controllers
{
    /// <summary>
    /// Summary description for CPUControllerTest
    /// </summary>
    [TestClass]
    public class CPUsControllerTest
    {
        
        Mock<ICPUService> Service = new Mock<ICPUService>();
        IMapper Mapper;

        public CPUsControllerTest()
        {
            //NinjectModule servicesModule = new AutoMapperNinjectModule();
            //NinjectModule presentationModule = new NinjectRegistration();
            
            //var kernel = new StandardKernel(servicesModule, presentationModule);
            //controller = kernel.Get<CPUsController>();
            
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

        [TestInitialize]
        public void TestInit()
        {
            Mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(typeof(AutoMapperPresentationProfile))));
        }

        [TestMethod]
        public void Index_Action_RightResult()
        {
            //Arrange
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();
            Guid guid4 = Guid.NewGuid();

            string ProcNumb1 = "Proc1";
            string ProcNumb2 = "Proc2";
            string ProcNumb3 = "Proc3";
            string ProcNumb4 = "Proc4";

            IEnumerable<CPUdto> cpus = new List<CPUdto>
            {
                new CPUdto{ProductGuid = guid1, ProcessorNumber = ProcNumb1},
                new CPUdto{ProductGuid = guid2, ProcessorNumber = ProcNumb2},
                new CPUdto{ProductGuid = guid3, ProcessorNumber = ProcNumb3},
                new CPUdto{ProductGuid = guid4, ProcessorNumber = ProcNumb4}
            };
            Service.Setup(x => x.GetCPUs()).Returns(cpus);

            CPUsController controller = new CPUsController(Service.Object, Mapper);
            //Act
            var result = controller.Index() as ViewResult;
            //Assert
            Assert.IsInstanceOfType(result.Model, typeof(List<CPUViewModel>));
            List<CPUViewModel> resultCPUs = (List<CPUViewModel>)result.Model;

            Assert.IsNotNull(resultCPUs.
                Find(x=>x.ProcessorNumber==ProcNumb1));
            Assert.IsNotNull(resultCPUs.
                Find(x => x.ProcessorNumber == ProcNumb2));
            Assert.IsNotNull(resultCPUs.
                Find(x => x.ProcessorNumber == ProcNumb3));
            Assert.IsNotNull(resultCPUs.
                Find(x => x.ProcessorNumber == ProcNumb4));
            Assert.IsNotNull(resultCPUs.
                Find(x => x.ProductGuid == guid1));
            Assert.IsNotNull(resultCPUs.
                Find(x => x.ProductGuid == guid2));
            Assert.IsNotNull(resultCPUs.
                Find(x => x.ProductGuid == guid3));
            Assert.IsNotNull(resultCPUs.
                Find(x => x.ProductGuid == guid4));
        }

        [TestMethod]
        public void Create_AddCPU_ReturneRightModel()
        {
            //Arrange
            Guid guid1 = Guid.NewGuid();
            string ProcNumb1 = "Proc1";

            CPUViewModel cpu = new CPUViewModel { ProductGuid = guid1, ProcessorNumber = ProcNumb1 };
                
            //Service.Setup(x => x.GetCPUs()).Returns(cpu);

            CPUsController controller = new CPUsController(Service.Object, Mapper);
            //Act
            var result = controller.Create(cpu) as ViewResult;
            //Assert
            Service.Verify(x => x.SaveCPU(It.IsAny<CPUdto>()));
        }

        [TestMethod]
        public void Edit_UpdateCPU_ServiceMethodWereCalled()
        {
            //Arrange
            Guid guid1 = Guid.NewGuid();
            string ProcNumb1 = "Proc1";

            CPUViewModel cpu = new CPUViewModel { ProductGuid = guid1, ProcessorNumber = ProcNumb1 };

            //Service.Setup(x => x.GetCPUs()).Returns(cpu);

            CPUsController controller = new CPUsController(Service.Object, Mapper);
            //Act
            var result = controller.Edit(cpu) as ViewResult;
            //Assert
            Service.Verify(x => x.EditCPU(It.IsAny<CPUdto>()));
        }

        [TestMethod]
        public void Delete_DeleteCPU_ServiceMethodWereCalled()
        {
            //Arrange
            Guid guid1 = Guid.NewGuid();
            string ProcNumb1 = "Proc1";

            CPUViewModel cpu = new CPUViewModel { ProductGuid = guid1, ProcessorNumber = ProcNumb1 };

            //Service.Setup(x => x.GetCPUs()).Returns(cpu);

            CPUsController controller = new CPUsController(Service.Object, Mapper);
            //Act
            var result = controller.DeleteConfirmed(guid1) as ViewResult;
            //Assert
            Service.Verify(x => x.DeleteCPU(guid1));
        }
    }
}

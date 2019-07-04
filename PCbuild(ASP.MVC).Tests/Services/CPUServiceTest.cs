using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PCbuild_ASP.MVC_.Domain.Abstract;
using PCbuild_ASP.MVC_.Domain.Entities;
using PCbuild_ASP.MVC_.Services.DTO;
using PCbuild_ASP.MVC_.Services.Services;
using System.Linq;

namespace PCbuild_ASP.MVC_.Tests.Services
{
    [TestClass]
    public class CPUServiceTest
    {
        Mock<IGenericRepository<CPU>> CPUs = new Mock<IGenericRepository<CPU>>();
        Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
        CPUService cpuService;

        [TestInitialize]
        public void TestInitialize()
        {
            cpuService = new CPUService(uow.Object, CPUs.Object);
        }

        [TestMethod]
        public void DeleteCPU_CPUDelete_RepositoryMethodsWereCalled()
        {
            //Arrange
            Guid guid = Guid.NewGuid();
            CPU cpu = new CPU { ProductGuid = guid, ProcessorNumber = "CPU1" };
            CPUs.Setup(x => x.FindById(guid)).Returns(cpu);

            //Act
            cpuService.DeleteCPU(guid);
            //Assert
            CPUs.Verify(x => x.FindById(guid));
            CPUs.Verify(x => x.Delete(cpu));
        }

        [TestMethod]
        public void EditCPU_UpdateCPU_RepositoryMethodsWereCalled()
        {
            //Arrange
            Guid guid = Guid.NewGuid();
            CPUdto cpu = 
                new CPUdto { ProductGuid = guid, ProcessorNumber = "CPU1" };
           
            //Act
            cpuService.EditCPU(cpu);
            //Assert
            CPUs.Verify(x => x.Update(It.IsAny<CPU>()));
        }

        [TestMethod]
        public void GetCPUByID_ReadCPU_RightCPUReturned()
        {
            //Arrange
            Guid guid = Guid.NewGuid();
            string procNumber = "CPU";
            CPU cpu = 
                new CPU { ProductGuid = guid, ProcessorNumber = procNumber };

            CPUs.Setup(x => x.FindById(guid)).Returns(cpu);
            //Act
            var result = cpuService.GetCPUByID(guid);
            //Assert
            CPUs.Verify(x => x.FindById(guid));
            Assert.AreEqual(procNumber, result.ProcessorNumber);
            Assert.AreEqual(guid, result.ProductGuid);
        }

        [TestMethod]
        public void GetCPUs_ReadListCPU_ListCPUReturned()
        {
            //Arrange
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();

            string procNumber1 = "CPU1";
            string procNumber2 = "CPU2";
            string procNumber3 = "CPU3";

            List<CPU> CPUList = new List<CPU>
            {
                new CPU{ProductGuid = guid1, ProcessorNumber = procNumber1},
                new CPU{ProductGuid = guid2, ProcessorNumber = procNumber2},
                new CPU{ProductGuid = guid3, ProcessorNumber = procNumber3}
            };

            CPUs.Setup(x => x.Get()).Returns(CPUList);
            //Act
            var result = cpuService.GetCPUs();
            //Assert
            Assert.IsNotNull(
                result.Where(x => x.ProcessorNumber == procNumber1));
            Assert.IsNotNull(
                 result.Where(x => x.ProcessorNumber == procNumber2));
            Assert.IsNotNull(
                 result.Where(x => x.ProcessorNumber == procNumber3));
            Assert.IsNotNull(
                result.Where(x => x.ProductGuid == guid1));
            Assert.IsNotNull(
                result.Where(x => x.ProductGuid == guid2));
            Assert.IsNotNull(
                result.Where(x => x.ProductGuid == guid3));
            Assert.AreEqual(CPUList.Count, result.Count());
        }

        [TestMethod]
        public void SaveCPU_AddCPUEntity_RepositoryMethodsWereCalled()
        {
            //Arrange
            Guid guid = Guid.NewGuid();
            string procNumber = "CPU";
            CPUdto cpudto = 
                new CPUdto {
                    ProductGuid = guid,
                    ProcessorNumber = procNumber };

            //Act
            cpuService.SaveCPU(cpudto);
            //Assert
            CPUs.Verify(x => x.Create(It.IsAny<CPU>()));
        }
    }
}

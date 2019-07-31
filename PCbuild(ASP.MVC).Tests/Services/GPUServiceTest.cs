using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PCbuilder_ASP.MVC_.Domain.Abstract;
using PCbuilder_ASP.MVC_.Domain.Concrete;
using PCbuilder_ASP.MVC_.Domain.Entities;
using PCbuilder_ASP.MVC_.Services.DTO;
using PCbuilder_ASP.MVC_.Services.Interfaces;
using PCbuilder_ASP.MVC_.Services.Services;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace PCbuilder_ASP.MVC_.Tests.Services
{
    [TestClass]
    public class GPUServiceTest
    {
        Mock<IGenericRepository<GPU>> GPUs = new Mock<IGenericRepository<GPU>>();
        GPUService gpuService;
        Mock<EFUnitOfWork> uow = new Mock<EFUnitOfWork>();
        
        [TestInitialize]
        public void TestInitialize()
        {
            gpuService = new GPUService(uow.Object, GPUs.Object);
        }

        [TestMethod]
        public void DeleteGPU_DeleteGPU_ResoistoryMethodsWereCalled()
        {
            //Arrange
            Guid guid = Guid.NewGuid();
            GPU gpu = new GPU { ProductGuid = guid, Name = "GTX2070Ti"};
            GPUs.Setup(x => x.FindById(guid)).Returns(gpu);
            //Act
            gpuService.DeleteGPU(guid);
            //Assert
            GPUs.Verify(x => x.FindById(guid));
            GPUs.Verify(x => x.Delete(gpu));
        }

        [TestMethod]
        public void EditGPU_UpdateGPU_ReposirotyMethodsWereCalled()
        {
            //Arrange
            Guid guid = Guid.NewGuid();
            GPUdto gpudto = new GPUdto { ProductGuid = guid, Name = "Edited" };
            //Act
            gpuService.EditGPU(gpudto);
            //Assert
            GPUs.Verify(x => x.Update(It.IsAny<GPU>()));
        }

        [TestMethod]
        public void SaveGPU_AddGPUEntity_RepositoryMethodsWereCalled()
        {
            //Arrange
            Guid guid = Guid.NewGuid();
            GPUdto gpudto = new GPUdto { ProductGuid = guid, Name = "Added" };
            //Act
            gpuService.SaveGPU(gpudto);
            //Assert
            GPUs.Verify(x => x.Create(It.IsAny<GPU>()));
        }

        [TestMethod]
        public void GetGPUByID_ReadGPU_RightGPUReturned()
        {
            //Arrange
            Guid guid = Guid.NewGuid();
            GPU gpu = new GPU { ProductGuid = guid, Name = "Read" };

            GPUs.Setup(x => x.FindById(guid)).Returns(gpu);
            //Act
            GPUdto result = gpuService.GetGPUByID(guid);
            //Assert
            Assert.AreEqual("Read", result.Name);
            GPUs.Verify(x => x.FindById(guid));
        }

        [TestMethod]
        public void GetGPUs_ReadListGPU_ListGPUReturned()
        {
            //Arrange
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();
            List<GPU> gpus = new List<GPU> {
                new GPU {ProductGuid = guid1, Name="GPU1"},
                new GPU {ProductGuid = guid2, Name="GPU2"},
                new GPU {ProductGuid = guid3, Name="GPU3"}
            };

            GPUs.Setup(x => x.Get()).Returns(gpus.AsQueryable());
            //Act
            var result = gpuService.GetGPUs();
            //Assert
            GPUs.Verify(x => x.Get());
            Assert.IsNotNull(result.Where(x => x.ProductGuid == guid1));
            Assert.IsNotNull(result.Where(x => x.ProductGuid == guid2));
            Assert.IsNotNull(result.Where(x => x.ProductGuid == guid3));
            Assert.IsNotNull(result.Where(x => x.Name == "GPU1"));
            Assert.IsNotNull(result.Where(x => x.Name == "GPU2"));
            Assert.IsNotNull(result.Where(x => x.Name == "GPU3"));
        }
    }
}

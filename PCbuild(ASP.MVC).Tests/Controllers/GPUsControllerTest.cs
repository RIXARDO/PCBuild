using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PCbuild_ASP.MVC_.Controllers;
using PCbuild_ASP.MVC_.Models.ViewModel;
using PCbuild_ASP.MVC_.Services.DTO;
using PCbuild_ASP.MVC_.Services.Interfaces;
using PCbuild_ASP.MVC_.Util;

namespace PCbuild_ASP.MVC_.Tests.Controllers
{
    [TestClass]
    public class GPUsControllerTest
    {
        Mock<IGPUService> Service = new Mock<IGPUService>();
        IMapper Mapper;

        public GPUsControllerTest()
        {
            Mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperPresentationProfile>()));
        }


        [TestMethod]
        public void Index_Action_RightResult()
        {
            //Arrange
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();
            Guid guid4 = Guid.NewGuid();

            string GPU1 = "GPU1";
            string GPU2 = "GPU2";
            string GPU3 = "GPU3";
            string GPU4 = "GPU4";

            IEnumerable<GPUdto> GPUs = new List<GPUdto>
            {
                new GPUdto{ProductGuid = guid1, Name = GPU1},
                new GPUdto{ProductGuid = guid2, Name = GPU2},
                new GPUdto{ProductGuid = guid3, Name = GPU3},
                new GPUdto{ProductGuid = guid4, Name = GPU4}
            };
            Service.Setup(x => x.GetGPUs()).Returns(GPUs);

            GPUsController controller = new GPUsController(Service.Object, Mapper);
            //Act
            var result = controller.Index() as ViewResult;
            //Assert
            Assert.IsInstanceOfType(result.Model, typeof(List<GPUViewModel>));
            List<GPUViewModel> resultGPUs = (List<GPUViewModel>)result.Model;

            Assert.IsNotNull(resultGPUs.
                Find(x => x.Name == GPU1));
            Assert.IsNotNull(resultGPUs.
                Find(x => x.Name == GPU2));
            Assert.IsNotNull(resultGPUs.
                Find(x => x.Name == GPU3));
            Assert.IsNotNull(resultGPUs.
                Find(x => x.Name == GPU4));
            Assert.IsNotNull(resultGPUs.
                Find(x => x.ProductGuid == guid1));
            Assert.IsNotNull(resultGPUs.
                Find(x => x.ProductGuid == guid2));
            Assert.IsNotNull(resultGPUs.
                Find(x => x.ProductGuid == guid3));
            Assert.IsNotNull(resultGPUs.
                Find(x => x.ProductGuid == guid4));
        }

        [TestMethod]
        public void Create_AddGPU_ReturneRightModel()
        {
            //Arrange
            Guid guid1 = Guid.NewGuid();
            string GPU1 = "GPU1";

            GPUViewModel GPU = new GPUViewModel { ProductGuid = guid1, Name = GPU1 };

            //Service.Setup(x => x.GetGPUs()).Returns(GPU);

            GPUsController controller = new GPUsController(Service.Object, Mapper);
            //Act
            var result = controller.Create(GPU) as ViewResult;
            //Assert
            Service.Verify(x => x.SaveGPU(It.IsAny<GPUdto>()));
        }

        [TestMethod]
        public void Edit_UpdateGPU_ServiceMethodWereCalled()
        {
            //Arrange
            Guid guid1 = Guid.NewGuid();
            string GPU1 = "GPU1";

            GPUViewModel GPU = new GPUViewModel { ProductGuid = guid1, Name = GPU1 };

            //Service.Setup(x=>x.EditGPU())

            GPUsController controller = new GPUsController(Service.Object, Mapper);
            //Act
            var result = controller.Edit(GPU) as ViewResult;
            //Assert
            Service.Verify(x => x.EditGPU(It.IsAny<GPUdto>()));
        }

        [TestMethod]
        public void Delete_DeleteGPU_ServiceMethodWereCalled()
        {
            //Arrange
            Guid guid1 = Guid.NewGuid();
            string GPU1 = "GPU1";

            GPUViewModel GPU = new GPUViewModel { ProductGuid = guid1, Name = GPU1 };

            //Service.Setup(x => x.GetGPUs()).Returns(GPU);

            GPUsController controller = new GPUsController(Service.Object, Mapper);
            //Act
            var result = controller.Delete(guid1) as ViewResult;
            //Assert
            Service.Verify(x => x.DeleteGPU(guid1));
        }
    }
}

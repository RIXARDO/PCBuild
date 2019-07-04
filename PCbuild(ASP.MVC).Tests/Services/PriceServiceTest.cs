using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using AutoMapper;
using PCbuild_ASP.MVC_.Domain.Abstract;
using PCbuild_ASP.MVC_.Domain.Entities;
using PCbuild_ASP.MVC_.Services.Services;
using PCbuild_ASP.MVC_.Services.DTO;
using System.Collections.Generic;

namespace PCbuild_ASP.MVC_.Tests.Services
{
    [TestClass]
    public class PriceServiceTest
    {
        Mock<IGenericRepository<Price>> Prices
            = new Mock<IGenericRepository<Price>>();
        Mock<IUnitOfWork> uow = 
            new Mock<IUnitOfWork>();
        PriceService priceService;

        [TestInitialize]
        public void TestInitialize()
        {
            priceService = new PriceService(uow.Object, Prices.Object);
        }

        [TestMethod]
        public void DeletePrice_PriceEntityDelete_RepositoryMethodWereCalled()
        {
            //Arrange
            Guid guid = Guid.NewGuid();

            //Act
            priceService.DeletePrice(guid);
            //Assert
            Prices.Verify(
                x => x.Delete(It.IsAny<Price>()));
        }

        [TestMethod]
        public void EditPrice_PriceEntityUpdate_RepositoryMethodWereCalled()
        {
            //Arrange
            Guid guid = Guid.NewGuid();

            PriceDTO priceDTO = new PriceDTO { PriceGuid = guid, Vendor = "Ven1" };
            //Act
            priceService.EditPrice(priceDTO);
            //Assert
            Prices.Verify(x => x.Update(It.IsAny<Price>()));
        }

        [TestMethod]
        public void GetPriceByID_ReadPrice_RightPriceReturned()
        {
            //Arrange
            Guid guid = Guid.NewGuid();
            string VendorName = "Vendor1";
            Price price =
                new Price { PriceGuid = guid, Vendor = VendorName };

            Prices.Setup(x => x.FindById(guid)).Returns(price);
            //Act
            var result = priceService.GetPriceByID(guid);
            //Assert
            Prices.Verify(x => x.FindById(guid));
            Assert.AreEqual(VendorName, result.Vendor);
            Assert.AreEqual(guid, result.PriceGuid);
        }

        [TestMethod]
        public void GetCPUs_ReadListCPU_ListCPUReturned()
        {
            //Arrange
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();

            string vendorName1 = "Vendor1";
            string vendorName3 = "Vendor2";
            string vendorName2 = "Vendor3";

            List<Price> PriceList = new List<Price>
            {
                new Price{PriceGuid = guid1, Vendor = vendorName1},
                new Price{PriceGuid = guid2, Vendor = vendorName2},
                new Price{PriceGuid= guid3, Vendor = vendorName3}
            };

            Prices.Setup(x => x.Get()).Returns(PriceList);
            //Act
            var result = priceService.GetPrices();
            //Assert
            Assert.IsNotNull(
                result.Where(x => x.Vendor == vendorName1));
            Assert.IsNotNull(
                 result.Where(x => x.Vendor == vendorName2));
            Assert.IsNotNull(
                 result.Where(x => x.Vendor == vendorName3));
            Assert.IsNotNull(
                result.Where(x => x.PriceGuid == guid1));
            Assert.IsNotNull(
                result.Where(x => x.PriceGuid == guid2));
            Assert.IsNotNull(
                result.Where(x => x.PriceGuid == guid3));
            Assert.AreEqual(PriceList.Count, result.Count());
        }

        [TestMethod]
        public void SavePrice_AddPriceEntity_RepositoryMethodsWereCalled()
        {
            //Arrange
            Guid guid = Guid.NewGuid();
            string VendorName = "Vend1";
            PriceDTO priceDTO =
                new PriceDTO
                {
                    PriceGuid = guid,
                    Vendor = VendorName
                };

            //Act
            priceService.SavePrice(priceDTO);
            //Assert
            Prices.Verify(x => x.Create(It.IsAny<Price>()));
        }

    }
}

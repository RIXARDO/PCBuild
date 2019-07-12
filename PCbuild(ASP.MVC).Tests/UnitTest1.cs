using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PCbuild_ASP.MVC_.Controllers;
using PCbuild_ASP.MVC_.Domain.Concrete;
using PCbuild_ASP.MVC_.Domain.Entities;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using PCbuild_ASP.MVC_.Domain.Abstract;

namespace PCbuild_ASP.MVC_.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GenericMetodsTest()
        {
            TestModel testModel = new TestModel();

            Mock<EFDbContext> context = new Mock<EFDbContext>();
            Mock<DbSet<TestModel>> dbset = new Mock<DbSet<TestModel>>();
            context.Setup(x => x.Set<TestModel>()).Returns(dbset.Object);
            dbset.Setup(x => x.Add(It.IsAny<TestModel>())).Returns(testModel);
            Mock<IUnitOfWork> mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(x => x.GetSource()).Returns(context.Object);

            EFRepository<TestModel> repository = new EFRepository<TestModel>(mockUow.Object, null);

            //act
            repository.Create(testModel);

            //assert

            context.Verify(x => x.Set<TestModel>());
            dbset.Verify(x => x.Add(It.Is<TestModel>(y => y == testModel)));
        }


    }
}

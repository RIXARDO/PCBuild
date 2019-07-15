using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;
using PCbuild_ASP.MVC_.Services.Interfaces;
using Moq;
using PCbuild_ASP.MVC_.Services.DTO;
using PCbuild_ASP.MVC_.Controllers;
using System.Web.Mvc;
using PCbuild_ASP.MVC_.Models.ViewModel;
using PCbuild_ASP.MVC_.Util;

namespace PCbuild_ASP.MVC_.Tests.Controllers
{
    /// <summary>
    /// Summary description for GamesControllerTest
    /// </summary>
    [TestClass]
    public class GamesControllerTest
    {
        IMapper Mapper;
        Mock<IGameService> Service= new Mock<IGameService>();


        public GamesControllerTest()
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
        public void Index_Action_RightResult()
        {
            //Arrange
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();
            Guid guid4 = Guid.NewGuid();

            string GameName1 = "Name1";
            string GameName2 = "Name2";
            string GameName3 = "Name3";
            string GameName4 = "Name4";

            IEnumerable<GameDTO> Games = new List<GameDTO>
            {
                new GameDTO{GameGuid = guid1, Name = GameName1},
                new GameDTO{GameGuid = guid2, Name = GameName2},
                new GameDTO{GameGuid = guid3, Name = GameName3},
                new GameDTO{GameGuid = guid4, Name = GameName4}
            };
            Service.Setup(x => x.GetGames()).Returns(Games);

            GamesController controller = new GamesController(Service.Object, Mapper);
            //Act
            var result = controller.Index() as ViewResult;
            //Assert
            Assert.IsInstanceOfType(result.Model, typeof(List<GameViewModel>));
            List<GameViewModel> resultGames = (List<GameViewModel>)result.Model;

            Assert.IsNotNull(resultGames.
                Find(x => x.Name == GameName1));
            Assert.IsNotNull(resultGames.
                Find(x => x.Name == GameName2));
            Assert.IsNotNull(resultGames.
                Find(x => x.Name == GameName3));
            Assert.IsNotNull(resultGames.
                Find(x => x.Name == GameName4));
            Assert.IsNotNull(resultGames.
                Find(x => x.GameGuid == guid1));
            Assert.IsNotNull(resultGames.
                Find(x => x.GameGuid == guid2));
            Assert.IsNotNull(resultGames.
                Find(x => x.GameGuid == guid3));
            Assert.IsNotNull(resultGames.
                Find(x => x.GameGuid == guid4));
        }

        [TestMethod]
        public void Create_AddGame_ReturneRightModel()
        {
            //Arrange
            Guid guid1 = Guid.NewGuid();
            string GameName1 = "Name1";

            GameViewModel Game = new GameViewModel { GameGuid = guid1, Name = GameName1 };

            //Service.Setup(x => x.GetGames()).Returns(Game);

            GamesController controller = new GamesController(Service.Object, Mapper);
            //Act
            var result = controller.Create(Game) as ViewResult;
            //Assert
            Service.Verify(x => x.SaveGame(It.IsAny<GameDTO>()));
        }

        [TestMethod]
        public void Edit_UpdateGame_ServiceMethodWereCalled()
        {
            //Arrange
            Guid guid1 = Guid.NewGuid();
            string GameName1 = "Name1";

            GameViewModel Game = new GameViewModel { GameGuid = guid1, Name = GameName1 };

            //Service.Setup(x => x.GetGames()).Returns(Game);

            GamesController controller = new GamesController(Service.Object, Mapper);
            //Act
            var result = controller.Edit(Game, null, null) as ViewResult;
            //Assert
            Service.Verify(x => x.EditGame(It.IsAny<GameDTO>()));
        }

        [TestMethod]
        public void Delete_DeleteGame_ServiceMethodWereCalled()
        {
            //Arrange
            Guid guid1 = Guid.NewGuid();
            string GameName1 = "Name1";

            GameViewModel Game = new GameViewModel { GameGuid = guid1, Name = GameName1 };

            //Service.Setup(x => x.GetGames()).Returns(Game);

            GamesController controller = new GamesController(Service.Object, Mapper);
            //Act
            var result = controller.Delete(guid1) as ViewResult;
            //Assert
            Service.Verify(x => x.DeleteGame(guid1));
        }
    }
}

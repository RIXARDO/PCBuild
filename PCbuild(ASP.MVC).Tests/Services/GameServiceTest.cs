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
using PCbuild_ASP.MVC_.Domain.Concrete;

namespace PCbuild_ASP.MVC_.Tests.Services
{
    [TestClass]
    public class GameServiceTest
    {
        Mock<EFUnitOfWork> uow = new Mock<EFUnitOfWork>();
        Mock<IGenericRepository<Game>> Games = new Mock<IGenericRepository<Game>>();
        GameService gameService;

        [TestInitialize]
        public void TestInitialize()
        {
            gameService = new GameService(uow.Object, Games.Object);
        }

        [TestMethod]
        public void SaveGame_AddGameEntity_GameRepositoryCreateMethodWereCalled()
        {
            //Arrange
            Guid guid = Guid.NewGuid();
            GameDTO gamedto = new GameDTO { GameGuid = guid, Name = "Game1" };

            //Act
            gameService.SaveGame(gamedto);

            //Assert
            Games.Verify(x => x.Create(It.IsAny<Game>()));
        }

        [TestMethod]
        public void EditGame_EditGameEntity_GameRepositoryEditMethodWereCalled()
        {
            //Arrange
            Guid guid = Guid.NewGuid();
            GameDTO gamedto = new GameDTO { GameGuid = guid, Name = "Game1" };

            //Act
            gameService.EditGame(gamedto);

            //Assert
            Games.Verify(x => x.Update(It.IsAny<Game>()));
        }

        [TestMethod]
        public void DeleteGame_DeleteGameEntity_GameRepositoryDeleteMethodWereCalled()
        {
            //Arrange
            Guid guid = Guid.NewGuid();

            //Act
            gameService.DeleteGame(guid);

            //Assert
            Games.Verify(x => x.Delete(It.IsAny<Game>()));
        }

        [TestMethod]
        public void GetGameByID_ReadGameEntity_GameRepositoryGetMethodWereCalled()
        {
            //Arrange
            Guid guid = Guid.NewGuid();
            Game game = new Game { GameGuid = guid, Name = "Game1" };

            Games.Setup(x => x.FindById(guid)).Returns(game);
            //Act
            gameService.GetGameByID(guid);

            //Assert
            Games.Verify(x => x.FindById(guid));
        }

        [TestMethod]
        public void GetGames_ReadListGame_GameRepositoryGetMethodWereCalled()
        {
            //Arrange
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();

            List<Game> gameList = new List<Game>
            {
                new Game {GameGuid = guid1, Name = "Game1"},
                new Game {GameGuid = guid2, Name = "Game2"},
                new Game {GameGuid = guid3, Name = "Game3"}
            };

            Games.Setup(x => x.Get()).Returns(gameList.AsQueryable());
            //Act
            var result = gameService.GetGames();

            //Assert
            Games.Verify(x => x.Get());
            Assert.IsNotNull(result.Where(x => x.GameGuid == guid1));
            Assert.IsNotNull(result.Where(x => x.GameGuid == guid2));
            Assert.IsNotNull(result.Where(x => x.GameGuid == guid3));

            Assert.IsNotNull(result.Where(x => x.Name == "Game1"));
            Assert.IsNotNull(result.Where(x => x.Name == "Game2"));
            Assert.IsNotNull(result.Where(x => x.Name == "Game3"));
        }
    }
}

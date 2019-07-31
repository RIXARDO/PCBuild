using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCbuilder_ASP.MVC_.Domain.Abstract;
using PCbuilder_ASP.MVC_.Domain.Entities;
using System.Drawing;
using PCbuilder_ASP.MVC_.Services.Interfaces;
using AutoMapper;
using PCbuilder_ASP.MVC_.Models.ViewModel;
using PCbuilder_ASP.MVC_.Services.DTO;

namespace PCbuilder_ASP.MVC_.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GamesController : Controller
    {
        IGameService Service;
        IMapper Mapper;

        public GamesController(IGameService service, IMapper mapper)
        {
            Service = service;
            Mapper = mapper;
        }

        // GET: Games
        public ActionResult Index()
        {
            var gamesdto = Service.GetGames();
            var games =
                Mapper.Map<IEnumerable<GameDTO>, IEnumerable<GameViewModel>>(gamesdto);
            return View(games);
        }

        public ViewResult Edit(Guid GameGuid)
        {
            GameDTO gamedto = Service.GetGameByID(GameGuid);
            var game = Mapper.Map<GameDTO, GameViewModel>(gamedto);
            return View(game);
        }

        [HttpPost]
        public ActionResult Edit(GameViewModel game, HttpPostedFileBase Image32, HttpPostedFileBase Image64)
        {
            if (ModelState.IsValid)
            {
                if (Image32 != null)
                {
                    game.ImageMimeType32 = Image32.ContentType;
                    game.ImageData32 = new byte[Image32.ContentLength];
                    Image32.InputStream.Read(game.ImageData32, 0, Image32.ContentLength);
                }
                if (Image64 != null)
                {
                    game.ImageMimeType64 = Image64.ContentType;
                    game.ImageData64 = new byte[Image64.ContentLength];
                    Image64.InputStream.Read(game.ImageData64, 0, Image64.ContentLength);
                }
                var gamedto = Mapper.Map<GameViewModel, GameDTO>(game);
                Service.EditGame(gamedto);

                TempData["message"] =
                    string.Format("{0} has been saved", game.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(game);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new GameViewModel());
        }

        public ActionResult Create(GameViewModel game)
        {
            if (ModelState.IsValid)
            {
                var gamedto = Mapper.Map<GameViewModel, GameDTO>(game);
                Service.SaveGame(gamedto);

                TempData["message"] =
                    string.Format("{0} has been created", game.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(game);
            }
        }

        [HttpDelete]
        public ActionResult Delete(Guid GameID)
        {
            GameDTO deletedGame = Service.GetGameByID(GameID);
            Service.DeleteGame(GameID);
            if (deletedGame != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedGame.Name);
            }
            return RedirectToAction("Index");
        }
    }
}

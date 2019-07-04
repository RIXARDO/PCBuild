using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PCbuild_ASP.MVC_.Domain.Abstract;
using PCbuild_ASP.MVC_.Domain.Entities;
using System.Drawing;

namespace PCbuild_ASP.MVC_.Controllers
{
    [Authorize(Roles ="Admin")]
    public class GamesController : Controller
    {
        IGameRepository repo;

        public GamesController(IGameRepository repository)
        {
            repo = repository;
        }

        // GET: Games
        public ActionResult Index()
        {

            return View(repo.Games);
        }

        public ViewResult Edit(Guid GameID)
        {
            Game game = repo.Games.FirstOrDefault(x => x.GameGuid == GameID);
            return View(game);

        }

        [HttpPost]
        public ActionResult Edit(Game game, HttpPostedFileBase Image32, HttpPostedFileBase Image64)
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
                repo.SaveGame(game);
                
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
            return View("Edit", new Game());
        }

        [HttpDelete]
        public ActionResult Delete(int GameID)
        {
            Game deletedGame = repo.DeleteGame(GameID);
                if (deletedGame != null)
                {
                    TempData["message"] = string.Format("{0} was deleted", deletedGame.Name);
                }
            return RedirectToAction("Index");
            }
        }
    }

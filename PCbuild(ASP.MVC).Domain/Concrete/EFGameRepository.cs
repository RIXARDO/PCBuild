using PCbuilder_ASP.MVC_.Domain.Abstract;
using PCbuilder_ASP.MVC_.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCbuilder_ASP.MVC_.Domain.Concrete
{
    public class EFGameRepository : IGameRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Game> Games { get { return context.Games; } }

        public Game DeleteGame(int GameId)
        {
            Game dbEntry = context.Games.Find(GameId);
            if (dbEntry != null)
            {
                context.Games.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveGame(Game game)
        {
            if (game.GameGuid == null)
            {
                context.Games.Add(game);
            }
            else
            {
                Game dbEntry = context.Games.Find(game.GameGuid);
                
                if (dbEntry != null)
                {
                    dbEntry.Name = game.Name;
                    dbEntry.AverangeRequirements = dbEntry.AverangeRequirements;
                    dbEntry.ImageData32 = game.ImageData32;
                    dbEntry.ImageMimeType32 = game.ImageMimeType32;
                    dbEntry.ImageData64 = game.ImageData64;
                    dbEntry.ImageMimeType64 = game.ImageMimeType64;
                }

            }
            context.SaveChanges();
        }
    }
}
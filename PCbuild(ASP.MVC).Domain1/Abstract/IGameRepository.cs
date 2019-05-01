using PCbuild_ASP.MVC_.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PCbuild_ASP.MVC_.Domain.Abstract
{
    public interface IGameRepository
    {
        IQueryable<Game> Games { get; }
        void SaveGame(Game game);
        Game DeleteGame(int GameId);
    }
}

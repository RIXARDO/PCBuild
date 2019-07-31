using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCbuilder_ASP.MVC_.Services.DTO;

namespace PCbuilder_ASP.MVC_.Services.Interfaces
{
    public interface IGameService
    {
        IEnumerable<GameDTO> GetGames();
        GameDTO GetGameByID(Guid guid);
        void EditGame(GameDTO game);
        void SaveGame(GameDTO game);
        void DeleteGame(Guid guid);
        FileDTO GetImage(Guid guid);
    }
}

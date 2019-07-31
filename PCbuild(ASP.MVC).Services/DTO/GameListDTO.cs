using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCbuilder_ASP.MVC_.Services.DTO
{
    public class GameListDTO
    {
        public IEnumerable<GameDTO> GameList { get; set; }
        public PagingInfoDTO PagingInfo { get; set; }
    }
}

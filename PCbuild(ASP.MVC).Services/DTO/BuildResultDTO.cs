using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCbuild_ASP.MVC_.Services.DTO
{
    public class BuildResultDTO
    {
        public List<BuildGameDTO> BuildGames { get; set; }
        public BuildEntityDTO Build { get; set; }
    }
}
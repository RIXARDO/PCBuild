
using PCbuilder_ASP.MVC_.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCbuilder_ASP.MVC_.Models
{
    public class BuildResult
    {
        public List<BuildGame> BuildGames { get; set; }
        public BuildEntityViewModel BuildEntity { get; set; }
    }

    public class BuildGame
    {
        public GameViewModel Game { get; set; }
        public int FPS { get; set; }
    }
}
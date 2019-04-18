using PCbuild_ASP.MVC_.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCbuild_ASP.MVC_.Models
{
    public class BuildResult
    {
        public List<BuildGame> BuildGames { get; set; }
        public BuildEntity BuildEntity { get; set; }
    }

    public class BuildGame
    {
        public Game Game { get; set; }
        public int FPS { get; set; }
    }
}
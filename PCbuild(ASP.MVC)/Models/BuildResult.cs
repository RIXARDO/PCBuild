using PCbuild_ASP.MVC_.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCbuild_ASP.MVC_.Models
{
    public class BuildResult
    {
        public Game Game { get; set; }
        public int FPS { get; set; }
    }
}
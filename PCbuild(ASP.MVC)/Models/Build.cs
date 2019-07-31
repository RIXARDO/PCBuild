using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PCbuilder_ASP.MVC_.Domain.Concrete;
using PCbuilder_ASP.MVC_.Domain.Entities;

//Needs destruction
namespace PCbuilder_ASP.MVC_.Models
{
    public class Build
    {
        public enum CPUenum { Intel, AMD }
        public enum GPUenum { Nvidia, Radeon}
        public enum Screen { p1080, p1440, p2560}

        public List<Game> Games { get; set; }
        public List<CPU> CPUs { get; set; }
        public List<GPU> GPUs { get; set; }

        public CPUenum cpuenum { get; set; }
        public GPUenum gpuenum { get; set; }
        public Screen screen { get; set; }

        public Build (List<CPU> cpus, List<GPU> gpus, List<Game> games)
        {
            Games = games;
            CPUs = cpus;
            GPUs = gpus;
        }
    }
}
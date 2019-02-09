using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCbuild_ASP.MVC_.Domain.Entities
{
    public class GPU
    {

        public int GPUID{ get;set;}

        public string Manufacture { get; set; }

        public string Name { get; set; }

        public string Architecture { get; set; }

        public int BoostClock { get; set; }

        public int FrameBuffer { get; set; }

        public int MemorySpeed { get; set; }

        public int AverageBench { get; set; }

        public List<Price> prices { get; set; }
    }
}
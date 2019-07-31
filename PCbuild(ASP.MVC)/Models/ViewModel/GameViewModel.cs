using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCbuilder_ASP.MVC_.Models.ViewModel
{
    public class GameViewModel
    {
        public Guid GameGuid { get; set; }

        public string Name { get; set; }

        public int AverangeRequirements { get; set; }

        public byte[] ImageData32 { get; set; }

        public string ImageMimeType32 { get; set; }

        public byte[] ImageData64 { get; set; }

        public string ImageMimeType64 { get; set; }
    }
}
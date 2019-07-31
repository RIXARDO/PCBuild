using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCbuilder_ASP.MVC_.Services.DTO
{
    public class GameDTO
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

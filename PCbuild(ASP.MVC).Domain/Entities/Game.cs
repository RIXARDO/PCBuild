using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCbuild_ASP.MVC_.Domain.Entities
{
    public class Game
    {
        [HiddenInput(DisplayValue = false)]
        public int GameID { get; set; }

        [Required(ErrorMessage ="Please enter a name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a requirements")]
        public int AverangeRequirements { get; set; }

        public byte[] ImageData32 { get; set; }

        public string ImageMimeType32 { get; set; }

        public byte[] ImageData64 { get; set; }

        public string ImageMimeType64 { get; set; }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
               Game game = (Game)obj;
                return (GameID == game.GameID);
            }
        }

        public override int GetHashCode()
        {
            return GameID<<2;
        }
    }
}
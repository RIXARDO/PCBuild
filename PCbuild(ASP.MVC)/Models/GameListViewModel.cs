using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PCbuild_ASP.MVC_.Domain.Entities;

namespace PCbuild_ASP.MVC_.Models
{
    public class GameListViewModel
    {
        public IEnumerable<Game> Games { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
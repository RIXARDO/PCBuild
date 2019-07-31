using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PCbuilder_ASP.MVC_.Domain.Entities;
using PCbuilder_ASP.MVC_.Models.ViewModel;

namespace PCbuilder_ASP.MVC_.Models
{
    public class GameListViewModel
    {
        public IEnumerable<GameViewModel> Games { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
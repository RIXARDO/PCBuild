using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PCbuild_ASP.MVC_.Domain.Entities;
using PCbuild_ASP.MVC_.Models.ViewModel;

namespace PCbuild_ASP.MVC_.Models
{
    public class GPUListViewModel
    {
        public IEnumerable<GPU> GPUs { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
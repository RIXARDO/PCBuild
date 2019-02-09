using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PCbuild_ASP.MVC_.Domain.Entities;

namespace PCbuild_ASP.MVC_.Models
{
    public class CPUListViewModel
    {
        public IEnumerable<CPU> CPUs;
        public PagingInfo PagingInfo;
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PCbuild_ASP.MVC_.Models.ViewModel;
using PCbuild_ASP.MVC_.Services.Comparison;

namespace PCbuild_ASP.MVC_.Models
{
    public class CompareIndexViewModel<T>
    {
        public Comparison<T> Comparison { get; set; }
        public string returnUrl { get; set; }
        
    }
}
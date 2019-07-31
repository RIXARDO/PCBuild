using System.Collections.Generic;
using System.Linq;
using System.Web;
using PCbuilder_ASP.MVC_.Models.ViewModel;
using PCbuilder_ASP.MVC_.Services.Comparison;

namespace PCbuilder_ASP.MVC_.Models
{
    public class CompareIndexViewModel<T>
    {
        public Comparison<T> Comparison { get; set; }
        public string returnUrl { get; set; }
        
    }
}
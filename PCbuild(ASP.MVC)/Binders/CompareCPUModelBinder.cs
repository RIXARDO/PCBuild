using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PCbuild_ASP.MVC_.Services.Comparison;
using PCbuild_ASP.MVC_.Models.ViewModel;

namespace PCbuild_ASP.MVC_.Binders
{

    public class CompareCPUModelBinder : IModelBinder
    {
        private const string sessionKey = "CPUComparison";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Comparison<CPUViewModel> comparison = 
                (Comparison<CPUViewModel>)controllerContext
                .HttpContext.Session[sessionKey];

            if (comparison == null)
            {
                comparison = new Comparison<CPUViewModel>();
                controllerContext.HttpContext.Session[sessionKey] = comparison;
            }

            return comparison;
        }
    }
}
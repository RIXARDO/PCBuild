using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PCbuild_ASP.MVC_.Domain.Entities;

namespace PCbuild_ASP.MVC_.Binders
{
    public class CompareGPUModelBinder: IModelBinder
    {
        private const string sessionKey = "GPUComparison";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Comparison<GPU> comparison = (Comparison<GPU>)controllerContext.HttpContext.Session[sessionKey];
            if (comparison == null)
            {
                comparison = new Comparison<GPU>();
                controllerContext.HttpContext.Session[sessionKey] = comparison;
            }
            return comparison;
        }
    }
}
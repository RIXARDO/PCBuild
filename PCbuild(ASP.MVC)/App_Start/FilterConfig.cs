using System.Web;
using System.Web.Mvc;

namespace PCbuilder_ASP.MVC_
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

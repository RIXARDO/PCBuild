using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using System.Text;
using PCbuilder_ASP.MVC_.Models;

namespace PCbuilder_ASP.MVC_.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks
        (
            this HtmlHelper html,
            PagingInfo pagingInfo,
            Func<int,string> PageUrl
        )
        {
            StringBuilder result = new StringBuilder();
            for(int i = 1; i<=pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", PageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pagingInfo.CurrentPage)
                    tag.AddCssClass("selectedPage");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}
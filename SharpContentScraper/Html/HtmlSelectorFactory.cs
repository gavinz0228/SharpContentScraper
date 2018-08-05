using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;

using SharpContentScraper;
using SharpContentScraper.Html.HtmlSelectors;
namespace SharpContentScraper.Html
{
    public class HtmlSelectorFactory
    {
        public static Dictionary<string, object> Cache = new Dictionary<string, object>();
        //need to be changed for multi-threading
        public static IHtmlSelector GetDefaultHtmlSelector()
        {
            if (!Cache.ContainsKey("CsQuerySelector"))
                Cache.Add("CsQuerySelector", new CsQuerySelector());
            return (IHtmlSelector)HtmlSelectorFactory.Cache["CsQuerySelector"];
        }
    }
}
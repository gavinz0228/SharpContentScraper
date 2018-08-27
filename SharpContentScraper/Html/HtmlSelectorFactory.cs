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

        public static IHtmlSelector GetDefaultHtmlSelector()
        {
            return (new CsQuerySelector()) as IHtmlSelector;
        }
    }
}
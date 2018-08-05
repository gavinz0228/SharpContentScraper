using CsQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using SharpContentScraper.Html;
namespace SharpContentScraper
{
    public class ScraperPage{
        public string html;
        public ScraperPage(string url, string html)
        {
            Url = new ScraperUrl( url);
            this.html =html;
        }
        public IEnumerable< ScraperElement> GetElements(string query)
        {
            IHtmlSelector htmlSelector = HtmlSelectorFactory.GetDefaultHtmlSelector();
            htmlSelector.LoadHtml(this.html);
            return htmlSelector.SelectElements(query);
        }
        public T MapToObject<T>(Mapper mapper){
            IHtmlSelector htmlSelector = HtmlSelectorFactory.GetDefaultHtmlSelector();
            htmlSelector.LoadHtml(this.html);
            return mapper.MapToObject<T>(mapper, htmlSelector);
        }
        public ScraperUrl Url{get;set;}
        public string Html {get;set;}

        public new string ToString() => this.Html;
    }
}
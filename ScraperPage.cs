using CsQuery;
using System;
using System.Collections.Generic;
using System.Linq;
namespace SharpContentScraper
{
    public class ScraperPage{
        public ScraperPage(string url, string html)
        {
            Url = new ScraperUrl( url);
            Html =html;
        }
        public IEnumerable< ScraperElement> GetElements(string query)
        {
            List<ScraperElement> elements = new List<ScraperElement>();
            CQ dom = this.Html;
            CQ result = dom[query];
            foreach(var e in result.Elements)
                elements.Add(new ScraperElement(e));
            return elements;
        }
        public T MapToObject<T>(Mapper mapper){
            return mapper.MapToObject<T>(mapper, this.Html);
        }
        public ScraperUrl Url{get;set;}
        public string Html {get;set;}

        public new string ToString() => this.Html;
    }
}
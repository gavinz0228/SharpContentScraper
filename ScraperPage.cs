using CsQuery;
using System;
using System.Collections.Generic;
using SharpContentScraper.Utilities;
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
            T obj = (T)Activator.CreateInstance(typeof(T));
            CQ dom = this.Html;
            var mappings = mapper.mappings;
            foreach(var propName in mappings.Keys)
            {
                CQ result = dom[mappings[propName].HtmlSelector];
                //ReflectionUtil.AssignProperty(obj, propName, result.Elements.FirstOrDefault());
            }
        }
        public ScraperUrl Url{get;set;}
        public string Html {get;set;}

        public new string ToString() => this.Html;
    }
}
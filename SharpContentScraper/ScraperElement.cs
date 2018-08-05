using System.Collections.Generic;
using System.Linq;
using SharpContentScraper.Html;
namespace SharpContentScraper
{
    public class ScraperElement
    {    
        public IHtmlSelector htmlSelector;
        internal ScraperElement(IHtmlSelector htmlSelector)
        {
            this.htmlSelector = htmlSelector;
        }
        public string GetAttr(string attrName) => this.htmlSelector.GetAttr(attrName);
        public string GetHtml() => this.htmlSelector.GetHtml();
        public string GetText() => this.htmlSelector.GetText();

        public T MapToObject<T>(Mapper mapper)
        {
            return mapper.MapToObject<T>(mapper, this.htmlSelector);
        }
        public Dictionary<string,string> MapToDictionary(Mapper mapper)
        {
            return mapper.MapToDictionary(mapper, this.htmlSelector);
        }
    }

    public static class ScraperElementExtension
    {
        public static IEnumerable<T> MapToObjects<T>(this IEnumerable<ScraperElement> elements, Mapper mapper)
        {
            return elements.Select(e=> e.MapToObject<T>(mapper));
        }
        public static IEnumerable<Dictionary<string,string>> MapToDictionary(this IEnumerable<ScraperElement> elements, Mapper mapper)
        {
            return elements.Select(e=> e.MapToDictionary(mapper));
        }
    }
    
}
using CsQuery;
using System.Collections.Generic;
using System.Linq;
namespace SharpContentScraper
{
    public class ScraperElement
    {    
        public IDomElement Element;
        private ScraperElement(){}
        internal ScraperElement(IDomElement ele)
        {
            this.Element = ele;
        }
        public string GetAttr(string attrName) => this.Element.GetAttribute(attrName);
        public IEnumerable<string> GetClasses() => this.Element.Classes;

        public string GetHtml() => this.Element.Render();
        public string GetText() => this.Element.InnerText;
        public string GetName() => this.Element.Name;
        public T MapToObject<T>(Mapper mapper){
            //System.Console.WriteLine(GetHtml());
            return mapper.MapToObject<T>(mapper, GetHtml());
        }
        public Dictionary<string,string> MapToDictionary(Mapper mapper)
        {
            return mapper.MapToDictionary(mapper,GetHtml());
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
using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using SharpContentScraper;
using SharpContentScraper.Core;
namespace SharpContentScraper.Html
{
    public interface IHtmlSelector
    {
        void LoadHtml(string html);
        string GetText();
        string GetAttr(string attr);
        string GetHtml();
        IEnumerable<IHtmlSelector> GetChildren(string query);
    }

    public static class IHtmlSelectorExtension
    {
        public static IEnumerable<T> MapToObjects<T>(this IEnumerable<IHtmlSelector> elements, Mapper mapper)
        {
            return elements.Select(e=> Mapper.MapToObject<T>(mapper, e));
        }
        public static IEnumerable<Dictionary<string,string>> MapToDictionary(this IEnumerable<IHtmlSelector> elements, Mapper mapper)
        {
            return elements.Select(e=> Mapper.MapToDictionary(mapper, e));
        }
    }
}
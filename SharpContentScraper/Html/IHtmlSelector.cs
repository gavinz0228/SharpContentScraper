using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;

using SharpContentScraper;
namespace SharpContentScraper.Html
{
    public interface IHtmlSelector
    {
        void LoadHtml(string html);
        IEnumerable<ScraperElement> SelectElements(string query);
        string GetText();
        string GetAttr(string attr);
        string GetHtml();
        IEnumerable<IHtmlSelector> GetChildren(string query);
    }
}
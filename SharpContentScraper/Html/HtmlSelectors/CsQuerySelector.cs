using System;
using System.Collections.Generic;
using SharpContentScraper.Html;

using CsQuery;
namespace SharpContentScraper.Html.HtmlSelectors
{
    public class CsQuerySelector:IHtmlSelector
    {
        private CQ dom;
        private string html;
        public void LoadHtml(string html)
        {
            this.html = html;
            dom = html;
        }
        public IEnumerable<ScraperElement> SelectElements(string query)
        {
            CQ ele = dom[query];
            if(ele == null)
                yield break;
            foreach(var e in ele)
            {
                CsQuerySelector htmlaSelector = new CsQuerySelector();
                htmlaSelector.LoadHtml(e.Render());
                yield return  new ScraperElement(htmlaSelector);
            }
        }
        public IEnumerable<IHtmlSelector> GetChildren(string query)
        {
            CQ ele = dom[query];
            if(ele == null)
                yield break;
            foreach(var e in ele)
            {
                CsQuerySelector htmlaSelector = new CsQuerySelector();
                htmlaSelector.LoadHtml(e.Render());
                yield return  (IHtmlSelector)htmlaSelector;
            }
        }
        public string GetText() => this.dom.Text();
        public string GetHtml() => this.html;
        public string GetAttr(string attr) => this.dom.Attr(attr);

    }
}
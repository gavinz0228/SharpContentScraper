using CsQuery;
using System.Collections.Generic;
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

        public string GetText() => this.Element.InnerText;
        public string GetName() => this.Element.Name;


    }
}
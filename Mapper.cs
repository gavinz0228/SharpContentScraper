using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
namespace SharpContentScraper
{
    public enum ValueType{Text, Attribute};
    public class Mapper{
        public Dictionary<string, ValueMapping> mappings {get;set;}
        public Mapper()
        {
            mappings = new Dictionary<string, ValueMapping>(StringComparer.CurrentCultureIgnoreCase);
        }
        public Mapper MapAttr(string htmlSelector, string attrName,string propertyName)
        {
            if(mappings.ContainsKey(propertyName))
                mappings[propertyName] =  new ValueMapping(){HtmlSelector = htmlSelector, AttributeName = attrName, Type = ValueType.Attribute};
            else
                mappings.Add(propertyName, new ValueMapping(){HtmlSelector = htmlSelector, AttributeName = attrName, Type = ValueType.Attribute});
            return this;
        }
        public Mapper MapText(string htmlSelector,string propertyName)
        {
            if(mappings.ContainsKey(propertyName))
                mappings[propertyName] =  new ValueMapping(){HtmlSelector = htmlSelector, AttributeName = null, Type = ValueType.Text};
            else
                mappings.Add(propertyName, new ValueMapping(){HtmlSelector = htmlSelector, AttributeName = null, Type = ValueType.Text});
            return this;
        }

    }
}
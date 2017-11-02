using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using CsQuery;
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

        public Dictionary<string, string> MapToDictionary(Mapper mapper, string html)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            CQ dom = html;
            var mappings = mapper.mappings;
            foreach(var propName in mappings.Keys)
            {
                var valueInfo = mappings[propName];
                if(string.IsNullOrEmpty(valueInfo.HtmlSelector))
                {
                    if(valueInfo.Type == ValueType.Text)
                        dict.Add( propName, dom.Text() );
                    else 
                        dict.Add(propName, dom.Attr(valueInfo.AttributeName));
                }
                else
                {
                    CQ result = dom[valueInfo.HtmlSelector];
                    Console.Write(result);
                    if(valueInfo.Type == ValueType.Text)
                        dict.Add( propName, result.Text() );
                    else 
                        dict.Add( propName, result.Attr(valueInfo.AttributeName) );
                }
            }
            return dict;
        }
        public T MapToObject<T>(Mapper mapper, string html){
            T obj = (T)Activator.CreateInstance(typeof(T));
            CQ dom = html;
            var mappings = mapper.mappings;
            foreach(var propName in mappings.Keys)
            {
                var valueInfo = mappings[propName];
                var propType = ReflectionUtil.GetPropertyType(typeof(T), propName);
                if(string.IsNullOrEmpty(valueInfo.HtmlSelector))
                {
                    if(valueInfo.Type == ValueType.Text)
                        ReflectionUtil.AssignProperty(obj, propName, ReflectionUtil.ConvertToType(dom.Text(), propType) );
                    else 
                        ReflectionUtil.AssignProperty(obj, propName, ReflectionUtil.ConvertToType( dom.Attr(valueInfo.AttributeName), propType));
                }
                else
                {
                    CQ result = dom[valueInfo.HtmlSelector];
                    Console.Write(result);
                    if(valueInfo.Type == ValueType.Text)
                        ReflectionUtil.AssignProperty(obj, propName, ReflectionUtil.ConvertToType(result.Text(), propType));
                    else 
                        ReflectionUtil.AssignProperty(obj, propName, ReflectionUtil.ConvertToType(result.Attr(valueInfo.AttributeName),propType));
                }
            }
            return obj;
        }

    }
}
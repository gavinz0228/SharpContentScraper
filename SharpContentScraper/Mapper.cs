using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using SharpContentScraper.Html;
using SharpContentScraper.Utilities;
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
            Console.WriteLine( mappings[propertyName].AttributeName);
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

        public Dictionary<string, string> MapToDictionary(Mapper mapper, IHtmlSelector htmlSelector)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var mappings = mapper.mappings;
            foreach(var propName in mappings.Keys)
            {
                var valueInfo = mappings[propName];
                if(string.IsNullOrEmpty(valueInfo.HtmlSelector))
                {
                    if(valueInfo.Type == ValueType.Text)
                        dict.Add( propName, htmlSelector.GetText() );
                    else 
                        dict.Add(propName, htmlSelector.GetAttr(valueInfo.AttributeName));
                }
                else
                {
                    IEnumerator<IHtmlSelector> childSelectors = htmlSelector.GetChildren(valueInfo.HtmlSelector).GetEnumerator();
                    //if there are multiple ones, only use the first one
                    if(childSelectors.MoveNext())
                    {
                        if(valueInfo.Type == ValueType.Text)
                            dict.Add( propName, childSelectors.Current.GetText() );
                        else 
                            dict.Add( propName, childSelectors.Current.GetAttr(valueInfo.AttributeName) );
                    }
                }
            }
            return dict;
        }
        public T MapToObject<T>(Mapper mapper, IHtmlSelector htmlSelector){
            //Console.WriteLine(htmlSelector.GetHtml());
            T obj = (T)Activator.CreateInstance(typeof(T));
            var mappings = mapper.mappings;
            foreach(var propName in mappings.Keys)
            {
                var valueInfo = mappings[propName];
                var propType = ReflectionUtil.GetPropertyType(typeof(T), propName);
                if(string.IsNullOrEmpty(valueInfo.HtmlSelector))
                {
                    
                    if(valueInfo.Type == ValueType.Text)
                        ReflectionUtil.AssignProperty(obj, propName, ReflectionUtil.ConvertToType(htmlSelector.GetText(), propType) );
                    else {
                        Console.WriteLine(valueInfo.AttributeName);
                        Console.WriteLine("_____________------___________"+htmlSelector.GetAttr(valueInfo.AttributeName) + "============");
                        ReflectionUtil.AssignProperty(obj, propName, ReflectionUtil.ConvertToType(htmlSelector.GetAttr(valueInfo.AttributeName), propType));
                    }
                }
                else
                {
                    IEnumerator<IHtmlSelector> childSelectors = htmlSelector.GetChildren(valueInfo.HtmlSelector).GetEnumerator();
                    //if there are multiple ones, only use the first one
                    if(childSelectors.MoveNext())
                    {
                        Console.WriteLine(childSelectors.Current.GetHtml());
                        if(valueInfo.Type == ValueType.Text)
                            ReflectionUtil.AssignProperty(obj, propName, ReflectionUtil.ConvertToType(childSelectors.Current.GetText(), propType));
                        else 
                        {
                            Console.WriteLine(valueInfo.AttributeName); 
                            Console.WriteLine("_____________------___________"+htmlSelector.GetAttr(valueInfo.AttributeName)+ "============");
                            ReflectionUtil.AssignProperty(obj, propName, ReflectionUtil.ConvertToType(childSelectors.Current.GetAttr(valueInfo.AttributeName),propType));
                            
                        }
                    }
                }
            }
            return obj;
        }

    }
}
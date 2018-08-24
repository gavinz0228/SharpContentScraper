using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using SharpContentScraper.Html;
using SharpContentScraper.Utilities;
namespace SharpContentScraper.Core
{
    public enum ValueType{Text, Attribute};
    public class Mapper{
        public Dictionary<string, ValueMapping> Mappings {get;set;}
        public Mapper()
        {
            Mappings = new Dictionary<string, ValueMapping>(StringComparer.CurrentCultureIgnoreCase);
        }
        public Mapper MapAttr(string htmlSelector, string attrName,string propertyName)
        {
            if(Mappings.ContainsKey(propertyName))
                Mappings[propertyName] =  new ValueMapping(){HtmlSelector = htmlSelector, AttributeName = attrName, Type = ValueType.Attribute};
            else
                Mappings.Add(propertyName, new ValueMapping(){HtmlSelector = htmlSelector, AttributeName = attrName, Type = ValueType.Attribute});
            return this;
        }
        public Mapper MapText(string htmlSelector,string propertyName)
        {
            if(Mappings.ContainsKey(propertyName))
                Mappings[propertyName] =  new ValueMapping(){HtmlSelector = htmlSelector, AttributeName = null, Type = ValueType.Text};
            else
                Mappings.Add(propertyName, new ValueMapping(){HtmlSelector = htmlSelector, AttributeName = null, Type = ValueType.Text});
            return this;
        }

        public static Dictionary<string, string> MapToDictionary(Mapper mapper, IHtmlSelector htmlSelector)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var Mappings = mapper.Mappings;
            foreach(var propName in Mappings.Keys)
            {
                var valueInfo = Mappings[propName];
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
        public static T MapToObject<T>(Mapper mapper, IHtmlSelector htmlSelector){
            //Console.WriteLine(htmlSelector.GetHtml());
            T obj = (T)Activator.CreateInstance(typeof(T));
            var mappings = mapper.Mappings;
            foreach(var propName in mappings.Keys)
            {
                var valueInfo = mappings[propName];
                var propType = ReflectionUtil.GetPropertyType(typeof(T), propName);
                if(string.IsNullOrEmpty(valueInfo.HtmlSelector))
                {
                    
                    if(valueInfo.Type == ValueType.Text)
                        ReflectionUtil.AssignProperty(obj, propName, ReflectionUtil.ConvertToType(htmlSelector.GetText(), propType) );
                    else {
                        ReflectionUtil.AssignProperty(obj, propName, ReflectionUtil.ConvertToType(htmlSelector.GetAttr(valueInfo.AttributeName), propType));
                    }
                }
                else
                {
                    IEnumerator<IHtmlSelector> childSelectors = htmlSelector.GetChildren(valueInfo.HtmlSelector).GetEnumerator();
                    //if there are multiple ones, only use the first one
                    if(childSelectors.MoveNext())
                    {
                        if(valueInfo.Type == ValueType.Text)
                            ReflectionUtil.AssignProperty(obj, propName, ReflectionUtil.ConvertToType(childSelectors.Current.GetText(), propType));
                        else 
                        {
                            ReflectionUtil.AssignProperty(obj, propName, ReflectionUtil.ConvertToType(childSelectors.Current.GetAttr(valueInfo.AttributeName),propType));
                        }
                    }
                }
            }
            return obj;
        }

    }
}
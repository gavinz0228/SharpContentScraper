using System;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;
using System.Diagnostics;
namespace SharpContentScraper.Utilities{
    public class ReflectionUtil
    {
        public static Dictionary<Type, Dictionary<string, PropertyInfo>> PropertyCache;
        static ReflectionUtil(){

            PropertyCache = new Dictionary<Type, Dictionary<string, PropertyInfo>> ();
        }
        public static PropertyInfo GetPropInfoFromCache(Type type, string propName )
        {
            if(!PropertyCache.ContainsKey(type)){
                var classProp = new Dictionary<string,PropertyInfo>();
                classProp.Add(propName, GetProp(type, propName));
                PropertyCache.Add(type, classProp );
            }
            else{
                var classProp = PropertyCache[type];
                if(!classProp.ContainsKey(propName)){
                    classProp.Add(propName, GetProp(type, propName));
                }
            }
            return PropertyCache[type][propName];
        }
        public static void AssignProperty(object obj, string propertyName, object value)
        {
            Type type = obj.GetType();
            var propInfo = GetPropInfoFromCache(type, propertyName);
            propInfo.SetValue(obj, value);
        }
        public static PropertyInfo GetProp(Type type, string propName)
        {
            return type.GetProperty(propName);
        }
        public static object ConvertToType(string value, Type targetType)
        {
            return TypeDescriptor.GetConverter(targetType).ConvertFromString(value);
        }
        public static Type GetPropertyType(Type type,string propName)
        {
            var propInfo = type.GetProperty(propName);
            return propInfo.PropertyType;
        }
    }
}
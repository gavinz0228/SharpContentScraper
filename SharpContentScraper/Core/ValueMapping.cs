using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
namespace SharpContentScraper.Core{
    public class ValueMapping
    {
        public string HtmlSelector{get; set;}
        public string AttributeName{get;set;}
        public ValueType Type{get;set;}
    }
}
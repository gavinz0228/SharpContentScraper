using System;
using System.Linq;
namespace SharpContentScraper
{
    class Program
    {
        static void Main(string[] args)
        {
           var result =Scraper.Get("http://higavin.com")
                .GetElements("div a")
                .Select( e => e.GetAttr("href"));
            Console.Write(string.Join("\r\n",result));
        }

    }
}

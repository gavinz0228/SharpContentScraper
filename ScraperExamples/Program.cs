using System;
using System.Linq;
using System.Collections.Generic;
using SharpContentScraper;
using System.Threading.Tasks;
using SharpContentScraper.Html;
using SharpContentScraper.Core;
namespace ScraperExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            ParseHuffingtonPost();
            //ParseHuffingtonPostAsync().Wait();
            //ParseHuffingtonPosToDictionary();
        }
        static void ParseHuffingtonPost()
        {
            var news =  Scraper.Get("https://www.huffingtonpost.com")
               .GetChildren(".card")
                   .MapToObjects<News>(
                        (new Mapper())
                    .MapAttr(".card__link","href","Url")
                    .MapText(".card__link", "Title")
                    ).ToList();

            foreach(var n in news)
            {
                Console.WriteLine(n.Title + " " + n.Url);
                
            }
        }
        static void ParseHuffingtonPosToDictionary()
        {
                var news =  Scraper.Get("https://www.huffingtonpost.com")
               .GetChildren(".card")
               .MapToDictionary(
                   (new Mapper())
                    .MapAttr(".card__link","href","Url")
                    .MapText(".card__link", "Title")
                              );
                foreach(Dictionary<string,string> n in news)
                {
                    foreach(var k in n.Keys)
                        Console.WriteLine(k);
                }
            
        }
        static async Task ParseHuffingtonPostAsync()
        {
            var news =( await Scraper.GetAsync("https://www.huffingtonpost.com"))
                   .GetChildren(".bn-card")
                   .AsParallel()
                   .MapToObjects<News>(
                       (new Mapper())
                        .MapAttr(".bn-card-headline","href","Url")
                        .MapText(".bn-card-headline", "Title")
                                  ).ToList();
            Console.WriteLine("sssss");
            foreach(var n in news)
            {
                Console.WriteLine(n.Title + " " + n.Url);
                
            }
        }

    }
    public class News
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }
}

using System;
using System.Linq;
using SharpContentScraper;
using System.Threading.Tasks;
namespace ScraperExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            ParseHuffingtonPostAsync().Wait();
             

        }
        static void ParseHuffingtonPost()
        {
            var news =  Scraper.Get("https://www.huffingtonpost.com")
                   .GetElements(".bn-card")
                   .MapToObjects<News>(
                       (new Mapper())
                        .MapAttr(".bn-card-headline","href","Url")
                        .MapText(".bn-card-headline", "Title")
                                  ).ToList();

            foreach(var n in news)
            {
                Console.WriteLine(n.Title + " " + n.Url);
                
            }
        }
        static async Task ParseHuffingtonPostAsync()
        {
            var news =( await Scraper.GetAsync("https://www.huffingtonpost.com"))
                   .GetElements(".bn-card")
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

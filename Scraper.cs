using System;
using System.Threading.Tasks;
using System.Net.Http;
namespace SharpContentScraper
{
    public class Scraper{
        public static ScraperPage Get(string url)
        {
            HttpClient client =new HttpClient();
            return Get(url, client);
        }
        public static ScraperPage Get(string url, HttpClient httpClient)
        {
            var httpResponse = httpClient.GetAsync(url);
            var html = httpResponse.Result.Content.ReadAsStringAsync().Result;
            return new ScraperPage(url, html);
        }
    }
}
using System;
using System.Threading.Tasks;
using System.Net.Http;
using SharpContentScraper.Html;
namespace SharpContentScraper.Core
{
    public class Scraper{
        public static IHtmlSelector Get(string url)
        {
            HttpClient client =new HttpClient();
            return Get(url, client);
        }
        public static IHtmlSelector Get(string url, HttpClient httpClient)
        {
            var httpResponse = httpClient.GetAsync(url);
            var html = httpResponse.Result.Content.ReadAsStringAsync().Result;
            IHtmlSelector htmlSelector = HtmlSelectorFactory.GetDefaultHtmlSelector();
            htmlSelector.LoadHtml(html);
            return htmlSelector;
        }
        public async static Task<IHtmlSelector> GetAsync(string url)
        {
            HttpClient httpClient =new HttpClient();
            var httpResponse = httpClient.GetAsync(url);
            var resp = await httpResponse;
            var html = await resp.Content.ReadAsStringAsync();
            IHtmlSelector htmlSelector = HtmlSelectorFactory.GetDefaultHtmlSelector();
            htmlSelector.LoadHtml(html);
            return htmlSelector;
        }
    }
}
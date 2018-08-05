using System.Threading.Tasks;
using SharpContentScraper.Html;
namespace SharpContentScraper.Core
{
    public class ScraperUrl{
        public ScraperUrl ( string url)
        {
            Value = url;
        }
        public string Value{get;set;}
        public  IHtmlSelector  Get(){
            return  Scraper.Get(this.Value);
        }
    }
}
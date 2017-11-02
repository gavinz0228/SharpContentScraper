using System.Threading.Tasks;
namespace SharpContentScraper{
    public class ScraperUrl{
        public ScraperUrl ( string url)
        {
            Value = url;
        }
        public string Value{get;set;}
        public ScraperPage  Get(){
            return  Scraper.Get(this.Value);
        }
    }
}
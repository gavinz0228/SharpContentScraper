using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using LyricsScraping.DAL.DatabaseEntities;
using System.Linq;
using SharpContentScraper.Html;
using SharpContentScraper.Core;
using System.Net;
namespace LyricsScraping.BLL.LyricsScrapers
{
    public class SharpLyricsScraper:ILyricsScraper
    {
        public List<Lyrics> GetAllLyricsBySinger(string singer)
        {
            var links = GetLinks("https://search.azlyrics.com/search.php?q=" + singer + "&w=songs&p=1");
            return links.Select(l => GetLyrics(l)).ToList();
        }
        public List<Lyrics> GetLyricsBySongTitle(string songTitle)
        {
            var links = GetLinks("https://search.azlyrics.com/search.php?q=" + songTitle + "&w=songs&p=1");
            return links.Select(l => GetLyrics(l)).ToList();
        }
        public List<Lyrics> GetLyricsBySingerAndSongTitle(string singer, string songTitle)
        {
            var links = GetLinks("https://search.azlyrics.com/search.php?q=" + singer + " " + songTitle + "&w=songs&p=1");
            return links.Select(l => GetLyrics(l)).ToList();
        }
        private List<string> GetLinks(string url)
        {
            List<string> links = new List<string>();

            var thirdPanel =  Scraper.Get(url).GetChildren(".panel").ToList()[0];
            List<Dictionary<string, string>> mappedDict = thirdPanel.GetChildren(".visitedlyr")
                    .MapToDictionary(
                            (new Mapper())
                        .MapAttr("a","href","url")
                    ).ToList();
            
            foreach(Dictionary<string, string> dict in mappedDict)
                links.Add(dict["url"]);

            return links;
        }
        private Lyrics GetLyrics(string url)
        {
            Lyrics lyrics  = new Lyrics();
            var mainDiv = Scraper.Get(url)
                .GetChildren(".col-xs-12").ToList()[1];
            var lyricsDiv = mainDiv.GetChildren("div").ToList()[7];
            lyrics.Content = WebUtility.UrlDecode(lyricsDiv.GetText());
            lyrics.Title = mainDiv.GetChildren("b").ToList()[1].GetText().Replace("\"", "");
            return lyrics;
        }
    }
}
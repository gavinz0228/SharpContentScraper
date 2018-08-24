using System;
using System.Linq;
using Microsoft;
using LyricsScraping.DAL.DatabaseEntities;
using LyricsScraping.DAL.Repositories;
using LyricsScraping.BLL.LyricsScrapers;
namespace LyricsScraping
{
    class Program
    {
        static void Main(string[] args)
        {
            var lyricsScraper = LyricsScraperFactory.GetLyricsScraperByName("LyricsScraping.BLL.LyricsScrapers.SharpLyricsScraper, LyricsScraping");
            var lyricsList = lyricsScraper.GetAllLyricsBySinger("Ed Sheeran");
            var uniqueLyrics = lyricsList
                .GroupBy(l => l.Title)
                .Select(l => l.First())
                .ToList();
            LyricsRepository lyricsRepository= new LyricsRepository(new LyricsDbContext());
            lyricsRepository.Add(uniqueLyrics);
            Console.WriteLine("Success");

        }
        static void TestLyricsSharperFactory()
        {
            var lyricsScraper = LyricsScraperFactory.GetLyricsScraperByName("LyricsScraping.BLL.LyricsScrapers.SharpLyricsScraper, LyricsScraping");
            Console.WriteLine(lyricsScraper == null);
        }
        static void TestAddEntry(string title)
        {
            Lyrics lyrics = new Lyrics(){ Title = title, Singer = "Hey"};
            LyricsRepository lyricsRepository= new LyricsRepository(new LyricsDbContext());
            lyricsRepository.Add(lyrics);
            Console.WriteLine("Success");
        }
        static void TestGetEntries()
        {
            LyricsRepository lyricsRepository= new LyricsRepository(new LyricsDbContext());
            foreach(Lyrics l in lyricsRepository.GetAll() )
                Console.WriteLine(l.Title);
        }
    }
}

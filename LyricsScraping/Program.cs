using System;
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

            LyricsRepository lyricsRepository= new LyricsRepository(new LyricsDbContext());
            lyricsRepository.Add(lyricsList);
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

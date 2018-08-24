using System;
using Microsoft;
using LyricsScraping.DAL.DatabaseEntities;
using LyricsScraping.DAL.Repositories;

namespace LyricsScraping
{
    class Program
    {
        static void Main(string[] args)
        {
            
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

using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using LyricsScraping.DAL.DatabaseEntities;
using SharpContentScraper;
namespace LyricsScraping.BLL.LyricsScrapers
{
    public class SharpLyricsScraper:ILyricsScraper
    {
        public List<Lyrics> GetAllLyricsBySinger(string singer)
        {
            return new List<Lyrics>();
        }
        public List<Lyrics> GetLyricsBySongTitle(string songTitle)
        {
            return new List<Lyrics>();
        }
        public List<Lyrics> GetLyricsBySingerAndSongTitle(string singer, string songTitle)
        {
            return new List<Lyrics>();
        }
    }
}
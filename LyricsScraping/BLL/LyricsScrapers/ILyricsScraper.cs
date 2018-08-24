using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LyricsScraping.DAL.DatabaseEntities;
namespace LyricsScraping.BLL.LyricsScrapers
{
    public interface ILyricsScraper 
    {
        List<Lyrics> GetAllLyricsBySinger(string singer);
        List<Lyrics> GetLyricsBySongTitle(string songTitle);
        List<Lyrics> GetLyricsBySingerAndSongTitle(string singer, string songTitle);
    }
}

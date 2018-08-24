using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using LyricsScraping.DAL.DatabaseEntities;
namespace LyricsScraping.BLL.LyricsScrapers
{
    public class LyricsScraperFactory
    {
        public static ILyricsScraper GetLyricsScraperByName(string className)
        {
            return new SharpLyricsScraper();
        }
    }
}
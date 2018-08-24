using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using LyricsScraping.DAL.DatabaseEntities;

namespace LyricsScraping.DAL.Repositories
{
    public class LyricsRepository : ILyricsRepository
    {
        private readonly LyricsDbContext _dbContext;
        public LyricsRepository(LyricsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Lyrics lyrics)
        {
            _dbContext.Lyrics.Add(lyrics);
            _dbContext.SaveChanges();
        }
        public void Add(IEnumerable<Lyrics> lyrics)
        {
            _dbContext.Lyrics.AddRange(lyrics);
            _dbContext.SaveChanges();
        }
        public IEnumerable<Lyrics> GetAll()
        {
            return _dbContext.Lyrics;
        }
    }
}

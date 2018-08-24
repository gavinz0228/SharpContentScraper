using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
namespace LyricsScraping.DAL.DatabaseEntities
{
    public class LyricsDbContext : DbContext
    {
        public DbSet<Lyrics> Lyrics { get; set; }

        public DbSet<Singer> Singer {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=SongLyrics.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Singer>()
                .HasKey(s => s.Name)
                .HasName("PrimaryKey_Singer_Name");

            modelBuilder.Entity<Lyrics>()
                .HasKey(l => l.Title)
                .HasName("PrimaryKey_Lyrics_Title");
            
        }
    }
}

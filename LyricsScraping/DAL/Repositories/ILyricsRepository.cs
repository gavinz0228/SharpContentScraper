using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using LyricsScraping.DAL.DatabaseEntities;

namespace LyricsScraping.DAL.Repositories
{
    interface ILyricsRepository 
    {
        void Add(Lyrics lyrics);  
        void Add(IEnumerable<Lyrics> lyrics);  
        IEnumerable<Lyrics> GetAll();
    }

}

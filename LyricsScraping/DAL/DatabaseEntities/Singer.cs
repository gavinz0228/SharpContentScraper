using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace LyricsScraping.DAL.DatabaseEntities
{
    public class Singer 
    {
        public string Name {get; set;}
        public List<Lyrics> Lyrics {get; set;}
    }
}

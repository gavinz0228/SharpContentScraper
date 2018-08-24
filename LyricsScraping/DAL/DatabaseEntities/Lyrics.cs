using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace LyricsScraping.DAL.DatabaseEntities
{
    public class Lyrics 
    {
        public string Title {get; set;}
        public string Singer {get; set;}
        public string Author {get; set;}
        public string Content {get; set;}
        public DateTime CreationTime {get; set;}
        public DateTime DownloadTime {get; set;}
    }
}

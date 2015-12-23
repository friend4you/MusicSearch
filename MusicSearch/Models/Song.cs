using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicSearch.Models
{
    public class Song
    {
        public int SongId { set; get; }
        public double Lat { set; get; }
        public double Lon { set; get; }
        public int VkSongId { set; get; }
        public int Rating { set; get; }
        public double Radius { set; get; }
    }
}
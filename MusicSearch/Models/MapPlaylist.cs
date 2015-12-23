using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicSearch.Models
{
    public class MapPlaylist
    {
        public virtual User User { set; get; }
        public virtual List<Song> Songs { set; get; }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace MusicSearch.Models
{
    public class MusicSearchContext: DbContext
    {
        public DbSet<User> Users { set; get; }
        public DbSet<Song> Songs { set; get; }
        public DbSet<MapPlaylist> MapPlaylists { set; get; }
        public DbSet<Rating> Ratings { set; get; }
    }
}
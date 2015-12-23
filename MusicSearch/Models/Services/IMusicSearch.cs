using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSearch.Models.Services
{
    interface IMusicSearch
    {
        List<Song> FindNearBySongs(double lat, double lon, double radius);
        void RateSong(int songId, int score, int userId);
        void CreateSong(Song song);
    }
}

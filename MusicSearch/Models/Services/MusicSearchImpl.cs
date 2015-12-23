using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicSearch.Models.Services
{
    public class MusicSearchImpl : IMusicSearch
    {
        public void CreateSong(Song song)
        {
            using (var db = new MusicSearchContext())
            {
                db.Songs.Add(song);
                db.SaveChanges();
            }
        }

        public List<Song> FindNearBySongs(double lat, double lon, double radius)
        {
            throw new NotImplementedException();
        }


        public void RateSong(int songId, int score, int userId)
        {
            using (var db = new MusicSearchContext())
            {
                var rating = from ratings in db.Ratings
                             where ratings.Song.SongId == songId && ratings.User.UserId == userId
                             select ratings;

                if (rating.Count() != 0)
                {
                    rating.First().Score = score;
                    db.SaveChanges();
                }
                else
                {
                    Rating rate = new Rating();
                    rate.User = (User)(from users in db.Users
                                      where users.UserId == userId
                                      select users).First();
                    rate.Song = (Song)(from songs in db.Songs
                                      where songs.SongId == songId
                                      select songs).First();
                    rate.Score = score;
                    db.Ratings.Add(rate);
                    db.SaveChanges();
                }                
            }
        }
    }
}
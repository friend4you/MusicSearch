using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicSearch.Models
{
    public class Rating
    {
        public int RatingId { set; get; }
        public virtual User User { set; get; }
        public virtual Song Song { set; get; }
        public int Score { set; get; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MusicSearch.Models
{
    public class User
    {
        public int UserId { set; get; }
        public string VkId { set; get; }
    }
}
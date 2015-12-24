using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicSearch.Models.Services
{
    public class UserImpl : IUser
    {
        public void AddUser(User user)
        {
            using(var db = new MusicSearchContext())
            {
                if (db.Users.Contains<User>(user))
                    return;
                else
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }                
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dot_net_mid_project.Models;

namespace dot_net_mid_project.Repository
{
    enum USER : int
    {
        Admin = 1,
        Customer = 2,
        Employee = 3,
        Manager = 4
    }
    public class UserRepository
    {
        static Entities Entities;
        static UserRepository()
        {
            Entities = new Entities();

        }
        public static User GetUser(string _id)
        {
            int ? id = null;
            if (Int32.TryParse(_id, out int res))
            {
                 id = res;
            }
            var rs = (from user in Entities.Users
                     where user.id == id
                     select user).FirstOrDefault();
            return rs;
        }

        public static User Authenticate(string email, string password)
        {
            var user = (from users in Entities.Users
                     where users.email.Trim() == email.Trim() &&
                     users.password.Trim() == password.Trim()
                     select users).FirstOrDefault();
            return user;
        }

        public static bool CreateUser(User collection)
        {
            User user = new User();
         
            user.phone = collection.phone;
            user.password = collection.password;
            user.email = collection.email;
            Entities.Users.Add(user);
            Entities.SaveChanges();
            return true;
        }

    }
}
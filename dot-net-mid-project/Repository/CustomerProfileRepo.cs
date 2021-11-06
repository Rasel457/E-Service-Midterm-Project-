using dot_net_mid_project.Models;
using dot_net_mid_project.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dot_net_mid_project.Repository
{
    
    public class CustomerProfileRepo
    {
        static Entities db;
        static CustomerProfileRepo()
        {
            db = new Entities();
        }

        public static CuserModel GetProfileInfo(int id)
        {
            var user = (from u in db.Users
                        where u.id == id
                        select u).FirstOrDefault();
            CuserModel ur = new CuserModel()
            {
                id = user.id,
                name = user.name,
                phone = user.phone,
                email = user.email,
                password = user.password,
                house = user.house,
                road = user.road

            };
            return ur;

        }

        public static CuserModel GetEditInfo(int id)
        {
            var user = (from u in db.Users
                        where u.id == id
                        select u).FirstOrDefault();
            CuserModel usr = new CuserModel()
            {
                id = user.id,
                name = user.name,
                phone = user.phone,
                email = user.email,
                password = user.password,
                house = user.house,
                road = user.road

            };
            return usr;

        }
    }
}
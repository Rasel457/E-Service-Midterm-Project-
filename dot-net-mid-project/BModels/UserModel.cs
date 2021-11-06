using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dot_net_mid_project.BModels
{
    public class UserModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public Nullable<int> address_id { get; set; }
        public int usertype { get; set; }
        public string password { get; set; }
        public string RePassword { get; set; }

    }
}
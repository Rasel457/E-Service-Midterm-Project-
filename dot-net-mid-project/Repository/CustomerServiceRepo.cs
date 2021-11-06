using dot_net_mid_project.Models;
using dot_net_mid_project.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dot_net_mid_project.Repository
{
    public class CustomerServiceRepo
    {
        static Entities db;
        static CustomerServiceRepo()
        {
            db = new Entities();
        }

        public static ServiceModel Get(int id)
        {
            var ser = (from s in db.Services
                       where s.id == id
                       select s).FirstOrDefault();

            ServiceModel sr = new ServiceModel()
            {
                id = ser.id,
                name = ser.name,
                details = ser.details,
                price = ser.price,
                status = ser.status,
                type = ser.type

            };
            return sr;
        }
        public static List<ServiceModel> GetAll()
        {
            var services = new List<ServiceModel>();
            foreach (var ser in db.Services)
            {
                ServiceModel sr = new ServiceModel()
                {

                    id = ser.id,
                    name = ser.name,
                    details = ser.details,
                    price = ser.price,
                    status = ser.status,
                    type = ser.type

                };
                services.Add(sr);

            }
            return services;

        }
    }
}
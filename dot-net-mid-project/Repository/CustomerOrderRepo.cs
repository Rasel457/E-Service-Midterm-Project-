using dot_net_mid_project.Models;
using dot_net_mid_project.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dot_net_mid_project.Repository
{
    public class CustomerOrderRepo
    {
        static Entities db;
        static CustomerOrderRepo()
        {
            db = new Entities();
        }

        public static void PlaceOrder(List<ServiceModel> services,object u_id,string add,int emp_id)
        {
            Order o = new Order();
            o.customer_id =(int)u_id;
            o.order_place_date = DateTime.Now;
            o.status ="Ordered";
            o.delevery_address=add;
            db.Orders.Add(o);
            db.SaveChanges();
            foreach (var s in services)
            {
                var od = new Order_Details()
                {
                    service_id = s.id,
                    employee_id =emp_id,
                    unit_price = s.price,
                    quantity = 1,
                    order_id = o.id
                };
                db.Order_Details.Add(od);
                db.SaveChanges();
            }
        }

        public static List<Order> Myorders(object c_id)
        {
            var customerId = (int)c_id;
            var orders = from o in db.Orders
                         where o.customer_id == customerId
                         select o;
            return orders.ToList(); ;
        }
    }
}
using dot_net_mid_project.Auth;
using dot_net_mid_project.Models;
using dot_net_mid_project.Models.VM;
using dot_net_mid_project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace dot_net_mid_project.Controllers
{
    [CustomerAccess]
    public class CustomerServiceController : Controller
    {
        // GET: CustomerService
        public ActionResult Index()
        {
            var s = CustomerServiceRepo.GetAll();
            return View(s);
        }

        public ActionResult AddtoCart(int id)
        {
            var s = CustomerServiceRepo.Get(id);
            List<ServiceModel> services;
            if (Session["cart"] == null)
            {
                services = new List<ServiceModel>();
                
            }
            else
            {
                var j_string = Session["cart"].ToString();
                services = new JavaScriptSerializer().Deserialize<List<ServiceModel>>(j_string);

            }
            services.Add(s);
            var json2 = new JavaScriptSerializer().Serialize(services);
            Session["cart"] = json2;
            return RedirectToAction("Cart");


        }
        public ActionResult Cart()
        {
            if (Session["cart"] != null)
            {
                var json = Session["cart"].ToString();
                var services = new JavaScriptSerializer().Deserialize<List<ServiceModel>>(json);
                return View(services);
            }
            return RedirectToAction("Index");


        }
        [HttpPost]
        public ActionResult Checkout(Order or)
        {
            var add = or.delevery_address;
            var json = Session["cart"].ToString();
            Entities db = new Entities();
            var emp = (from e in db.Employees
                       where e.work_status == "free"
                       && e.work_area == add
                       select e).First();
            var emp_id = emp.id;
            var services = new JavaScriptSerializer().Deserialize<List<ServiceModel>>(json);
            var c_id = Session["userid"];
            CustomerOrderRepo.PlaceOrder(services, c_id, add,emp_id);
            Session.Remove("cart");
            return RedirectToAction("MyOrder");


        }

        public ActionResult DeleteToCart(int id)
        {
            var jsonString = Session["cart"].ToString();
            var data = new JavaScriptSerializer().Deserialize<List<ServiceModel>>(jsonString);
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i] != null)
                    if (data[i].id == id && data[i] != null)
                    {
                        data[i] = null;
                    }
            }
            Session["cart"] = new JavaScriptSerializer().Serialize(data);
            return RedirectToAction("Cart");

        }

        public ActionResult MyOrder()
        {
            var c_id = Session["userid"];
            var orders = CustomerOrderRepo.Myorders(c_id);
            return View(orders);

        }

        public ActionResult Details(int id)
        {
            Entities db = new Entities();
            
            var entity = (from o in db.Orders
                          where o.id == id 
                          select o).First();
          
            double x = 0;
            double y = 0;
            int i = 0;
            foreach (var item in entity.Order_Details)
            {
                var price = item.unit_price;
                var qty = item.quantity;
                x = x+(price * qty);
                i++;
            }
            double[] arr = new double[i];
            ViewBag.totalPrice = x;
            i = 0;
            foreach (var item in entity.Order_Details)
            {
                var price = item.unit_price;
                var qty = item.quantity;
                y =  (price * qty);
                arr[i] = y;
                i++;
                y = 0;
            }
            ViewBag.value = -1;
            ViewBag.Array = arr;

        return View(entity);
            
        }

        [HttpPost]
        public ActionResult Search()
        {

            var catagory = Request["type"];
            using (Entities db = new Entities())
            {
                var entity = (from s in db.Services
                              where s.type == catagory && s.status=="open"
                              select s);
                return View(entity.ToList());
            }


        }
    }
} 
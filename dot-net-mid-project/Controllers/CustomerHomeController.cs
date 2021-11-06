using dot_net_mid_project.Auth;
using dot_net_mid_project.Models;
using dot_net_mid_project.Models.VM;
using dot_net_mid_project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace dot_net_mid_project.Controllers
{
    [CustomerAccess]
    public class CustomerHomeController : Controller
    {
        // GET: CustomerHome
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AllServices()
        {
            Entities db = new Entities();
            var data = db.Services.ToList();
            return View(data);
            
        }

        public ActionResult MyProfile()
        {
            object u_id = Session["userid"];
            var id = (int)u_id;
            var user = CustomerProfileRepo.GetProfileInfo(id);
            return View(user);

        }
        [HttpGet]
        public ActionResult EditProfile()
        {
            object u_id = Session["userid"];
            var id = (int)u_id;
            var user = CustomerProfileRepo.GetEditInfo(id);
            return View(user);

        }
        [HttpPost]
        public ActionResult Update(CuserModel s)
        {
            using (Entities db = new Entities())
            {
                var entity = (from u in db.Users
                              where u.id == s.id
                              select u).FirstOrDefault();
                entity.id = s.id;
                entity.name = s.name.Trim();
                entity.phone = s.phone.Trim();
                entity.email = s.email.Trim();
                entity.password = s.password.Trim();
                entity.house = s.house.Trim();
                entity.road = s.road.Trim();
                db.SaveChanges();
                return RedirectToAction("MyProfile");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
         
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using dot_net_mid_project.Models;
using dot_net_mid_project.Repository;
namespace dot_net_mid_project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User input_user)
        {
            var  user = UserRepository.Authenticate(input_user.email, input_user.password);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.id.ToString(), true);
                Session["userid"] = user.id;
                if(user.usertype == (int)USER.Admin) 
                   return RedirectToAction("Index", "DashBoard");
                else if (user.usertype == (int)USER.Customer)
                    return RedirectToAction("Index", "CustomerHome");
                else if (user.usertype == (int)USER.Employee)
                    return RedirectToAction("Index", "EmployeeHome");
                else if (user.usertype == (int)USER.Manager)
                    return RedirectToAction("Index", "ManagerHome");

                else return RedirectToAction("Index");

            }
            ViewData["messeage"] = "Wrong Credentials";
            return View("Index");
        }

    

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
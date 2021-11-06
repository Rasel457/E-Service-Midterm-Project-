using dot_net_mid_project.Auth;
using dot_net_mid_project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dot_net_mid_project.Controllers
{
    [AdminAccess]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            UserRepository.GetUser(Session["userid"].ToString());

            return View();
        }
    }
}
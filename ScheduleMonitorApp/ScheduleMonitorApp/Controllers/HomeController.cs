using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScheduleMonitorApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "This is a Network based Scheduler and Monitoring System";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact!!";

            return View();
        }
    }
}
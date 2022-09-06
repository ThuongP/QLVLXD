using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VLXD.Models;
using System.Data.Entity;
namespace VLXD.Controllers
{
    public class HomeController : Controller
    {
        QLVLXDEntities db = new QLVLXDEntities();
        public ActionResult Index()
        {
            var vatlieu = db.VATLIEUx.ToList();
            return View(vatlieu);
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
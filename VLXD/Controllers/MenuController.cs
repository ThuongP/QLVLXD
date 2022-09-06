using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VLXD.Models;
using System.Collections;

namespace VLXD.Controllers
{
    public class MenuController : Controller
    {
        QLVLXDEntities db = new QLVLXDEntities();
        // GET: Menu
        public ActionResult Index()
        {
            var loaisp = db.LOAIVATLIEUx.ToList();
            Hashtable arrLoaiSP = new Hashtable();
            foreach (var item in loaisp)
            {
                arrLoaiSP.Add(item.MaLoaiVL, item.TenLoaiVL);
            }
            ViewBag.LoaiSP = arrLoaiSP;
            return PartialView("Index");

        }
    }
}
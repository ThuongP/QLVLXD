using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VLXD.Models;

namespace VLXD.Controllers
{
    public class CHITIETHOADONController : Controller
    {
        private QLVLXDEntities db = new QLVLXDEntities();

        // GET: CHITIETHOADON
        public ActionResult Index(string mahd)
        {
            List<KHACHHANG> khachhang = db.KHACHHANGs.ToList();
            List<HOADON> hoadon = db.HOADONs.ToList();
            List<VATLIEU> vatlieu = db.VATLIEUx.ToList();
            List<CHITIETHOADON> chitiethoadon = db.CHITIETHOADONs.ToList();
            var main = from h in hoadon
                       join k in khachhang on h.MaKH equals k.MaKH
                       where h.MaHD == mahd
                       select new ViewModel
                       {
                           hoadon = h,
                           khachhang = k
                       };
            var sub = from c in chitiethoadon
                      join v in vatlieu on c.MaVL equals v.MaVL
                      where c.MaHD == mahd
                      select new ViewModel
                      {
                          chitiethoadon = c,
                          vatlieu = v,
                          Thanhtien = Convert.ToDouble(c.DonGia * c.SoLuong)
                      };
            ViewBag.Main = main;
            ViewBag.Sub = sub;

            //var cHITIETHOADONs = db.CHITIETHOADONs.Include(c => c.HOADON).Include(c => c.VATLIEU);
            //return View(cHITIETHOADONs.ToList());
            return View();
        }

        // GET: CHITIETHOADON/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETHOADON cHITIETHOADON = db.CHITIETHOADONs.Find(id);
            if (cHITIETHOADON == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETHOADON);
        }

        // GET: CHITIETHOADON/Create
        public ActionResult Create()
        {
            ViewBag.MaHD = new SelectList(db.HOADONs, "MaHD", "MaKH");
            ViewBag.MaVL = new SelectList(db.VATLIEUx, "MaVL", "TenVL");
            return View();
        }

        // POST: CHITIETHOADON/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHD,MaVL,SoLuong,DonGia,GiamGia")] CHITIETHOADON cHITIETHOADON)
        {
            if (ModelState.IsValid)
            {
                db.CHITIETHOADONs.Add(cHITIETHOADON);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHD = new SelectList(db.HOADONs, "MaHD", "MaKH", cHITIETHOADON.MaHD);
            ViewBag.MaVL = new SelectList(db.VATLIEUx, "MaVL", "TenVL", cHITIETHOADON.MaVL);
            return View(cHITIETHOADON);
        }

        // GET: CHITIETHOADON/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETHOADON cHITIETHOADON = db.CHITIETHOADONs.Find(id);
            if (cHITIETHOADON == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHD = new SelectList(db.HOADONs, "MaHD", "MaKH", cHITIETHOADON.MaHD);
            ViewBag.MaVL = new SelectList(db.VATLIEUx, "MaVL", "TenVL", cHITIETHOADON.MaVL);
            return View(cHITIETHOADON);
        }

        // POST: CHITIETHOADON/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHD,MaVL,SoLuong,DonGia,GiamGia")] CHITIETHOADON cHITIETHOADON)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cHITIETHOADON).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHD = new SelectList(db.HOADONs, "MaHD", "MaKH", cHITIETHOADON.MaHD);
            ViewBag.MaVL = new SelectList(db.VATLIEUx, "MaVL", "TenVL", cHITIETHOADON.MaVL);
            return View(cHITIETHOADON);
        }

        // GET: CHITIETHOADON/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETHOADON cHITIETHOADON = db.CHITIETHOADONs.Find(id);
            if (cHITIETHOADON == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETHOADON);
        }

        // POST: CHITIETHOADON/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CHITIETHOADON cHITIETHOADON = db.CHITIETHOADONs.Find(id);
            db.CHITIETHOADONs.Remove(cHITIETHOADON);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

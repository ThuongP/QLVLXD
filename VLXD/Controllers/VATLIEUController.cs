using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VLXD.Models;
using System.IO;
using PagedList;

namespace VLXD.Controllers
{
    public class VATLIEUController : Controller
    {
        private QLVLXDEntities db = new QLVLXDEntities();

        // GET: VATLIEU
        public ActionResult Index(string sortOrder, int ?page)
        {
            ViewBag.SortByName = String.IsNullOrEmpty(sortOrder) ? "ten_desc" : "";
            ViewBag.SortByPrice = (sortOrder == "dongia" ? "dongia_desc" : "dongia");
            ViewBag.SortOrder = sortOrder;

            var vATLIEUx = db.VATLIEUx.Include(v => v.LOAIVATLIEU);
            switch(sortOrder)
            {
                case "ten_desc":
                    vATLIEUx = vATLIEUx.OrderByDescending(s => s.TenVL);
                    break;
                case "dongia":
                    vATLIEUx = vATLIEUx.OrderBy(s => s.GiaVL);
                    break;
                case "dongia_desc":
                    vATLIEUx = vATLIEUx.OrderByDescending(s => s.GiaVL);
                    break;
                default:
                    vATLIEUx = vATLIEUx.OrderBy(s => s.TenVL);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(vATLIEUx.ToPagedList(pageNumber, pageSize));
        }

        // GET: VATLIEU/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VATLIEU vATLIEU = db.VATLIEUx.Find(id);
            if (vATLIEU == null)
            {
                return HttpNotFound();
            }
            return View(vATLIEU);
        }

        // GET: VATLIEU/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiVL = new SelectList(db.LOAIVATLIEUx, "MaLoaiVL", "TenLoaiVL");
            return View();
        }

        // POST: VATLIEU/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaVL,TenVL,DonVi,GiaVL,MaLoaiVL,HinhVL")] VATLIEU vATLIEU,
            HttpPostedFileBase HinhVL)
        {
            if (ModelState.IsValid)
            {
                if(HinhVL !=null && HinhVL.ContentLength >0)
                {
                    string filename = Path.GetFileName(HinhVL.FileName);
                    string path = Server.MapPath("~/Images/" + filename);
                    vATLIEU.HinhVL = "Images/" + filename;
                    HinhVL.SaveAs(path); 
                }
                db.VATLIEUx.Add(vATLIEU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoaiVL = new SelectList(db.LOAIVATLIEUx, "MaLoaiVL", "TenLoaiVL", vATLIEU.MaLoaiVL);
            return View(vATLIEU);
        }

        // GET: VATLIEU/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VATLIEU vATLIEU = db.VATLIEUx.Find(id);
            if (vATLIEU == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiVL = new SelectList(db.LOAIVATLIEUx, "MaLoaiVL", "TenLoaiVL", vATLIEU.MaLoaiVL);
            return View(vATLIEU);
        }

        // POST: VATLIEU/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaVL,TenVL,DonVi,GiaVL,MaLoaiVL,HinhVL")] VATLIEU vATLIEU,
            HttpPostedFileBase HinhVL)
        {
            if (ModelState.IsValid)
            {
                if (HinhVL != null && HinhVL.ContentLength > 0)
                {
                    System.IO.File.Delete(Server.MapPath("~/" + vATLIEU.HinhVL));
                    string filename = Path.GetFileName(HinhVL.FileName);
                    string path = Server.MapPath("~/Images/" + filename);
                    vATLIEU.HinhVL = "Images/" + filename;
                    HinhVL.SaveAs(path);
                    
                }
                db.Entry(vATLIEU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoaiVL = new SelectList(db.LOAIVATLIEUx, "MaLoaiVL", "TenLoaiVL", vATLIEU.MaLoaiVL);
            return View(vATLIEU);
        }

        // GET: VATLIEU/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VATLIEU vATLIEU = db.VATLIEUx.Find(id);
            if (vATLIEU == null)
            {
                return HttpNotFound();
            }
            return View(vATLIEU);
        }

        // POST: VATLIEU/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VATLIEU vATLIEU = db.VATLIEUx.Find(id);
            db.VATLIEUx.Remove(vATLIEU);
            db.SaveChanges();
            System.IO.File.Delete(Server.MapPath("~/" + vATLIEU.HinhVL)); 
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

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
    public class LOAIVATLIEUController : Controller
    {
        private QLVLXDEntities db = new QLVLXDEntities();

        // GET: LOAIVATLIEU
        public ActionResult Index()
        {
            return View(db.LOAIVATLIEUx.ToList());
        }

        // GET: LOAIVATLIEU/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAIVATLIEU lOAIVATLIEU = db.LOAIVATLIEUx.Find(id);
            if (lOAIVATLIEU == null)
            {
                return HttpNotFound();
            }
            return View(lOAIVATLIEU);
        }

        // GET: LOAIVATLIEU/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LOAIVATLIEU/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLoaiVL,TenLoaiVL")] LOAIVATLIEU lOAIVATLIEU)
        {
            if (ModelState.IsValid)
            {
                db.LOAIVATLIEUx.Add(lOAIVATLIEU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lOAIVATLIEU);
        }

        // GET: LOAIVATLIEU/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAIVATLIEU lOAIVATLIEU = db.LOAIVATLIEUx.Find(id);
            if (lOAIVATLIEU == null)
            {
                return HttpNotFound();
            }
            return View(lOAIVATLIEU);
        }

        // POST: LOAIVATLIEU/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLoaiVL,TenLoaiVL")] LOAIVATLIEU lOAIVATLIEU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lOAIVATLIEU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lOAIVATLIEU);
        }

        // GET: LOAIVATLIEU/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAIVATLIEU lOAIVATLIEU = db.LOAIVATLIEUx.Find(id);
            if (lOAIVATLIEU == null)
            {
                return HttpNotFound();
            }
            return View(lOAIVATLIEU);
        }

        // POST: LOAIVATLIEU/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LOAIVATLIEU lOAIVATLIEU = db.LOAIVATLIEUx.Find(id);
            db.LOAIVATLIEUx.Remove(lOAIVATLIEU);
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

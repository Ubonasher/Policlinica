using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Policlinica.Models;

namespace Policlinica.Controllers
{
    public class DiagnozController : Controller
    {
        private BdEntities db = new BdEntities();

        //
        // GET: /Diagnoz/

        public ActionResult Index(int VremId = 0)
        {
            ViewBag.VremId = VremId;
            return View(db.Diagnoz.ToList());
        }

        //
        // GET: /Diagnoz/Details/5

        public ActionResult Details(int id = 0, int VremId = 0)
        {
            Diagnoz diagnoz = db.Diagnoz.Find(id);
            if (diagnoz == null)
            {
                return HttpNotFound();
            }
            ViewBag.VremId = VremId;
            return View(diagnoz);
        }

        //
        // GET: /Diagnoz/Create

        public ActionResult Create()
        {
            ViewBag.Vr = db.UserProfile.ToList();
            return View();
        }

        //
        // POST: /Diagnoz/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Diagnoz diagnoz)
        {
            if (ModelState.IsValid)
            {
                db.Diagnoz.Add(diagnoz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(diagnoz);
        }

        //
        // GET: /Diagnoz/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Diagnoz diagnoz = db.Diagnoz.Find(id);
            if (diagnoz == null)
            {
                return HttpNotFound();
            }
            return View(diagnoz);
        }

        //
        // POST: /Diagnoz/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Diagnoz diagnoz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diagnoz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(diagnoz);
        }

        //
        // GET: /Diagnoz/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Diagnoz diagnoz = db.Diagnoz.Find(id);
            if (diagnoz == null)
            {
                return HttpNotFound();
            }
            return View(diagnoz);
        }

        //
        // POST: /Diagnoz/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Diagnoz diagnoz = db.Diagnoz.Find(id);
            db.Diagnoz.Remove(diagnoz);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
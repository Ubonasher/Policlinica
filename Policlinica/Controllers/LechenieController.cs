using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Policlinica.Models;
using System.Collections;
using System.Globalization;
using WebMatrix.WebData;
using WebMatrix.Data;

namespace Policlinica.Controllers
{
    public class LechenieController : Controller
    {
        private BdEntities db = new BdEntities();

        //
        // GET: /Lechenie/

        public ActionResult Index(String Pac, int VremId )
        {
            var lechenie = (from lech in db.Lechenie orderby lech.Vrem.Дата ascending where lech.UserProfile.UserName == Pac select lech).ToList();
            ViewBag.Pac = Pac;
            ViewBag.VremId = VremId;
            return View(lechenie.ToList());
        }

        //
        // GET: /Lechenie/
        [Authorize(Roles = "p")]
        public ActionResult IndexPac()
        {
            var lechenie = (from lech in db.Lechenie orderby lech.Vrem.Дата ascending where lech.UserProfile.UserName == User.Identity.Name select lech).ToList();
            return View(lechenie.ToList());
        }

        //
        // GET: /Lechenie/Details/5

        public ActionResult Details(int id = 0, String Pac = "", int VremId = 0)
        {
            Lechenie lechenie = db.Lechenie.Find(id);
            if (lechenie == null)
            {
                return HttpNotFound();
            }
            ViewBag.Pac = Pac;
            ViewBag.VremId = VremId;
            return View(lechenie);
        }

        //
        // GET: /Lechenie/Create

        public ActionResult Create(int id)
        {
            ViewBag.Диагноз = new SelectList(db.Diagnoz, "Id", "Диагноз");
            ViewBag.fmt = (new CultureInfo("hr-HR")).DateTimeFormat;
            ViewBag.Время = db.Vrem.Find(id);
            ViewBag.VremId = id;
            return View();
        }

        //
        // POST: /Lechenie/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(String vremID, Lechenie lechenie)
        {
            int IntvremID = Convert.ToInt32(vremID);
            if (ModelState.IsValid )
            {
                Vrem tempVrem = db.Vrem.Find(IntvremID);
                
                lechenie.Время = tempVrem.Id;
                lechenie.Врач = tempVrem.Врач;
                lechenie.Пациент = tempVrem.Пациент;
                db.Lechenie.Add(lechenie);
                db.SaveChanges();
                var updateCommand = @"UPDATE [Vrem] SET [Vrem].[Статус] = {0}  WHERE Id = {1}";
                db.Database.ExecuteSqlCommand(updateCommand, "c", Convert.ToString(tempVrem.Id));
                db.SaveChanges();
                return RedirectToAction("Index","Zaj",null);
            }

            ViewBag.Диагноз = new SelectList(db.Diagnoz, "Id", "Диагноз");
            ViewBag.fmt = (new CultureInfo("hr-HR")).DateTimeFormat;
            ViewBag.Время = db.Vrem.Find(IntvremID);
            return View(lechenie);
        }

        //
        // GET: /Lechenie/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Lechenie lechenie = db.Lechenie.Find(id);
            if (lechenie == null)
            {
                return HttpNotFound();
            }
            ViewBag.Диагноз = new SelectList(db.Diagnoz, "Id", "Диагноз", lechenie.Диагноз);
            ViewBag.Пациент = new SelectList(db.UserProfile, "UserId", "UserName", lechenie.Пациент);
            ViewBag.Время = new SelectList(db.Vrem, "Id", "Статус", lechenie.Время);
            ViewBag.Врач = new SelectList(db.UserProfile, "UserId", "UserName", lechenie.Врач);
            return View(lechenie);
        }

        //
        // POST: /Lechenie/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Lechenie lechenie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lechenie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Диагноз = new SelectList(db.Diagnoz, "Id", "Диагноз", lechenie.Диагноз);
            ViewBag.Пациент = new SelectList(db.UserProfile, "UserId", "UserName", lechenie.Пациент);
            ViewBag.Время = new SelectList(db.Vrem, "Id", "Статус", lechenie.Время);
            ViewBag.Врач = new SelectList(db.UserProfile, "UserId", "UserName", lechenie.Врач);
            return View(lechenie);
        }

        //
        // GET: /Lechenie/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Lechenie lechenie = db.Lechenie.Find(id);
            if (lechenie == null)
            {
                return HttpNotFound();
            }
            return View(lechenie);
        }

        //
        // POST: /Lechenie/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lechenie lechenie = db.Lechenie.Find(id);
            db.Lechenie.Remove(lechenie);
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
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Policlinica.Models;
using WebMatrix.WebData;
using System.Web.Security;

namespace Policlinica.Controllers
{
    public class ZajController : Controller
    {
        private BdEntities db = new BdEntities();

        //
        // GET: /Zaj/

        public ActionResult Index()
        {
            var vremm = (from vrem in db.Vrem orderby vrem.Дата , vrem.Время ascending  where vrem.UserProfile1.UserName == WebSecurity.CurrentUserName && vrem.Статус == "z" && vrem.Дата > DateTime.Today   select vrem ).ToList();
            return View(vremm);
        }

        //
        // GET: /Zaj/

        public ActionResult IndexPac()
        {
            var vremm = (from vrem in db.Vrem orderby vrem.Дата , vrem.Время ascending  where vrem.UserProfile.UserName == WebSecurity.CurrentUserName select vrem ).ToList();
            return View(vremm);
        }
        
        //
        //
        public ActionResult SearchRes(string searchString)
        {
            var vremm = (from vrem in db.Vrem orderby vrem.Дата, vrem.Время ascending where vrem.UserProfile1.UserName == WebSecurity.CurrentUserName && vrem.UserProfile.ФИО == searchString && vrem.Статус == "z" && vrem.Дата > DateTime.Today select vrem).ToList();

            return View(vremm);
        }

        //
        // GET: /Zaj/Details/5

        public ActionResult Details(int id = 0)
        {
            Vrem vrem = db.Vrem.Find(id);
            if (vrem == null)
            {
                return HttpNotFound();
            }
            return View(vrem);
        }

        //
        // GET: /Default1/Create

        public ActionResult Create()
        {
            var Vr = db.Database.SqlQuery<String>("SELECT [UserProfile].[UserName] FROM [UserProfile] WHERE EXISTS (SELECT [UserId] , [RoleId] FROM [webpages_UsersInRoles] WHERE [UserProfile].[UserId] = [webpages_UsersInRoles].[UserId] AND [webpages_UsersInRoles].[RoleId] = 2)").ToList();
            ViewBag.Врач = Vr;
            return View();
        }

        //
        // POST: /Default1/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vrem vrem, String UN="")
        {
            //int i = WebSecurity.GetUserId(UN);
            vrem.Врач = WebSecurity.GetUserId(UN);
            vrem.Статус = "s";
            if (ModelState.IsValid)
            {
                db.Vrem.Add(vrem);
                db.SaveChanges();
                return RedirectToAction( "IndexAdm" , "Vrem" , null );
            }

            ViewBag.Пациент = new SelectList(db.UserProfile, "UserId", "UserName", vrem.Пациент);
            ViewBag.Врач = new SelectList(db.UserProfile, "UserId", "UserName", vrem.Врач);
            return View(vrem);
        }

        //
        // GET: /Zaj/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Vrem vrem = db.Vrem.Find(id);
            if (vrem == null)
            {
                return HttpNotFound();
            }
            ViewBag.Пациент = new SelectList(db.UserProfile, "UserId", "UserName", vrem.Пациент);
            ViewBag.Врач = new SelectList(db.UserProfile, "UserId", "UserName", vrem.Врач);
            return View(vrem);
        }

        //
        // POST: /Zaj/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Vrem vrem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vrem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexAdm","Vrem");
            }
            ViewBag.Пациент = new SelectList(db.UserProfile, "UserId", "UserName", vrem.Пациент);
            ViewBag.Врач = new SelectList(db.UserProfile, "UserId", "UserName", vrem.Врач);
            return View(vrem);
        }

        //
        // GET: /Zaj/Otm/5


        public ActionResult Otm(int id = 0)
        {
            Vrem vrem = db.Vrem.Find(id);
            if (vrem == null)
            {
                return HttpNotFound();
            }
            vrem.Пациент = null;
            db.Entry(vrem).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("IndexPac");
        }

        //
        // GET: /Zaj/Delete/5


        public ActionResult Delete(int id = 0)
        {
            Vrem vrem = db.Vrem.Find(id);
            if (vrem == null)
            {
                return HttpNotFound();
            }
            return View(vrem);
        }

        //
        // POST: /Zaj/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vrem vrem = db.Vrem.Find(id);
            db.Vrem.Remove(vrem);
            db.SaveChanges();
            return RedirectToAction("IndexAdm", "Vrem");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
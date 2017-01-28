using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Policlinica.Models;
using WebMatrix.WebData;

namespace Policlinica.Controllers
{
    [Authorize(Roles = "adm")]
    public class VrachiController : Controller
    {
        private BdEntities db = new BdEntities();

        //
        // GET: /Vrachi/

        public ActionResult Index()
        {
            var userprofile = db.UserProfile.Include(u => u.Otdelenie);
            return View(userprofile.ToList());
        }

        //
        // GET: /Vrachi/Details/5

        public ActionResult Details(int id = 0)
        {
            UserProfile userprofile = db.UserProfile.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }



        //
        // GET: /Vrachi/Edit/5

        public ActionResult Edit(int id = 0)
        {
            UserProfile userprofile = db.UserProfile.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            ViewBag.Отделение = new SelectList(db.Otdelenie, "Id", "Название_отделени", userprofile.Отделение);
            return View(userprofile);
        }

        //
        // POST: /Vrachi/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserProfile userprofile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userprofile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Отделение = new SelectList(db.Otdelenie, "Id", "Название_отделени", userprofile.Отделение);
            return View(userprofile);
        }

        //
        // GET: /Vrachi/Delete/5

        public ActionResult Delete(int id = 0)
        {
            UserProfile userprofile = db.UserProfile.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // POST: /Vrachi/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserProfile userprofile = db.UserProfile.Find(id);
            SimpleMembershipProvider provider = (SimpleMembershipProvider)System.Web.Security.Membership.Provider;

            System.Web.Security.Roles.RemoveUserFromRole(provider.GetUserNameFromId(id), "v");

            System.Web.Security.Membership.DeleteUser(provider.GetUserNameFromId(id));
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
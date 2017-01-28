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

namespace Policlinica.Controllers
{
    public class VremController : Controller
    {
        private BdEntities db = new BdEntities();

        //
        // GET: /Vrem/

        public ActionResult IndexAdm()
        {
            var vremm = (from vrem in db.Vrem orderby vrem.Дата, vrem.Время ascending where vrem.Дата > DateTime.Today select vrem).ToList();
            return View(vremm);
        }


        //
        // GET: /Vrem/

        public ActionResult Index(int idO)
        {
            var vremm = (from vrem in db.Vrem where vrem.UserProfile1.Отделение == idO && vrem.Статус == "s" select vrem).ToList();
            List<Nullable<System.DateTime>> temp = new List<Nullable<System.DateTime>>();
            foreach (var item in vremm)
            {
                bool a = false;
                if (temp.Count == 0)
                {
                    temp.Add(item.Дата);
                }
                else
                {
                    foreach (var item1 in temp)
                    {
                        if (item1 == item.Дата)
                        {
                            a = true;
                            break;
                        }
                    }
                    if (a == false)
                    {
                        temp.Add(item.Дата);
                    }
                }
            }
            temp.Sort();
            ViewBag.listD = temp;
            ViewBag.fmt = (new CultureInfo("hr-HR")).DateTimeFormat;
            ViewBag.idO = idO;
            return View();
        }



        //
        // GET: /Vrem/Vremja/5

        public ActionResult Vremja( Nullable<System.DateTime> d, int idO)
        {
            var vremm = (from vrem in db.Vrem where vrem.UserProfile1.Отделение == idO && vrem.Статус == "s" && vrem.Дата == d select vrem).ToList();
            List<Nullable<System.TimeSpan>> temp = new List<Nullable<System.TimeSpan>>();
            foreach (var item in vremm)
            {
                bool a = false;
                if (temp.Count == 0)
                {
                    temp.Add(item.Время);
                }
                else
                {
                    foreach (var item1 in temp)
                    {
                        if (item1 == item.Время)
                        {
                            a = true;
                            break;
                        }
                    }
                    if (a == false)
                    {
                        temp.Add(item.Время);
                    }
                }
            }
            temp.Sort();
            ViewBag.d = d;
            ViewBag.listV = temp;
            ViewBag.idO = idO;
            return View();

        }

        //
        // GET: /Vrem/FinalBron/5

        public ActionResult FinalBron(Nullable<System.TimeSpan> v, Nullable<System.DateTime> d, int idO)
        {
            var vremm = (from vrem in db.Vrem where vrem.UserProfile1.Отделение == idO && vrem.Статус == "s" && vrem.Дата == d  && vrem.Время == v select vrem).ToList();
            Vrem vrem1=new Vrem();
            if (vremm.Count != 0)
            {
                vrem1 = vremm[0];
                var zantemp = (from t in db.Vrem where t.Врач == vrem1.Врач && t.Статус == "z" && t.Дата > DateTime.Today select t).ToList();
                int zan = zantemp.Count;

                foreach (var item in vremm)
                {
                    var temp = (from t in db.Vrem where t.Врач == item.Врач && t.Статус == "z" && t.Дата > DateTime.Today select t).ToList();
                    if (zan > (int)temp.Count)
                    {
                        vrem1 = item;
                    }
                }
                var tempP = (from us in db.UserProfile where us.UserName == WebSecurity.CurrentUserName select us).First();
                vrem1.Пациент = tempP.UserId;
                db.Entry(vrem1).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.fmt = (new CultureInfo("hr-HR")).DateTimeFormat;
            }
            return View(vrem1);
        }

    }
}
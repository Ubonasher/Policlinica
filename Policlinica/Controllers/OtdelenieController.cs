using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Policlinica.Models;

namespace KP_policlinica.Controllers
{
    public class OtdelenieController : Controller
    {
        private BdEntities db = new BdEntities();

        //
        // GET: /Otdelenie/

        public ActionResult Index()
        {
            return View(db.Otdelenie.ToList());
        }

    }
}
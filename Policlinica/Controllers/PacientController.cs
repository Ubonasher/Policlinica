using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Policlinica.Controllers
{
    [Authorize(Roles = "p")]
    public class PacientController : Controller
    {
        //
        // GET: /Pacient/

        public ActionResult Index()
        {
            return View();
        }

    }
}

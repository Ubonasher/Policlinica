using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Policlinica.Controllers
{
    [Authorize(Roles = "v")]
    public class VrachController : Controller
    {
        //
        // GET: /Vrach/

        public ActionResult Index()
        {
            return View();
        }

    }
}

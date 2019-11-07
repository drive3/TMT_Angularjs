using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.Controllers
{
    public class CatelogyController : Controller
    {
        // GET: Catelogy
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListCatelogy()
        {
            return View();
        }
    }
}
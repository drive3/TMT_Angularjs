using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.Controllers
{
    public class ProductController : Controller
    {
        // GET: Porduct
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListProduct()
        {
            return View();
        }
        public ActionResult CreateProduct()
        {
            return View();
        }
        public ActionResult EditProduct()
        {
            return View();
        }

        public ActionResult List() {
            return View();

        }
        public ActionResult Form() {
            return View();
        }
    }
}
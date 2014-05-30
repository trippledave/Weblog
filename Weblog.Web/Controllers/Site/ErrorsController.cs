using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Weblog.Web.Controllers.Site
{
    public class ErrorsController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Index()
        {
            return RedirectToAction("Error");

        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult DisplayError(Exception e)
        {
            ViewBag.Error= e.Message;
            return RedirectToAction("Error");
        }

        public ActionResult Error404()
        {
            return View();
        }

    }
}

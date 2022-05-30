using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace ira.Controllers
{
    [Authorize]
    public class ErrorPageController : Controller
    {
        public ActionResult Index(int id) 
        {

            return View();
        }

        public ActionResult Oops(int id)
        {

            Response.StatusCode = id;

            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult NotAuthorized()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab12.Areas.MyNewArea.Controllers
{
    public class HomeController : Controller
    {
        // GET: MyNewArea/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}
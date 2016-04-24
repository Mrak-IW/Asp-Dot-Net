using Lab12.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab12.Areas.MyNewArea.Controllers
{
    public class Lab12Controller : Controller
    {
        // GET: MyNewArea/Lab12
        public ActionResult Index(string routeName, string parameter)
        {
			ViewInfo output = new ViewInfo();
			output.routeName = routeName;
			output.someText = "Контроллер из дополнительной области";
			return View("/Views/Lab12/Index.cshtml", output);
        }
    }
}
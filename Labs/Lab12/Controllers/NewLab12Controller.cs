using Lab12.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab12.Controllers
{
    public class NewLab12Controller : Controller
    {
        // GET: PrefixLab12
        public ActionResult SuperAction(string routeName, string parameter)
        {
			ViewInfo output = new ViewInfo();
			output.routeName = routeName;
			output.someText = parameter;

			List<string> result = new List<string>();

			foreach (var p in Request.QueryString)
			{
				result.Add(p + " = " + Request.QueryString[p.ToString()]);
			}

			ViewBag.ParamList = result;

			return View("/Views/Lab12/Index.cshtml", output);
        }
    }
}
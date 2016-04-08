using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Lab9.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Task1(int? x)
		{
			if (x != null)
			{
				if (x <= 0)
				{
					return Redirect("/home/Task1_1?x=" + x);
				}
				else
				{
					return Redirect("/home/Task1_2?x=" + x);
				}
			}
			return View();
		}

		public ActionResult Task1_1(int x)
		{
			ViewBag.Result = new HtmlString("А чё так мало?<br />X * 2 = " + x * 2);
			return View();
		}

		public ActionResult Task1_2(int x)
		{
			ViewBag.Result = new HtmlString("Вполне достаточно<br />X * X = " + x * x);
			return View();
		}

		public ActionResult Task2_GetFile(string filename)
		{
			if (filename == "" || filename == null)
			{
				return Redirect("/home/Task2_GetFile?filename=filename.none");
			}

			string path = Server.MapPath("~/Content/" + filename);
			if (System.IO.File.Exists(path))
			{
				string type = MimeMapping.GetMimeMapping(filename);

				return File(path, type);
			}

			string message = string.Format("File \"{0}\" is not found on this server.", filename);
			//Encoding encSrc = Encoding.Default;
			//Encoding encDst = Encoding.UTF8;
			//byte[] bytes = encSrc.GetBytes(message);
			//bytes = Encoding.Convert(encSrc, encDst, bytes);
			//message = encDst.GetString(bytes);

			return HttpNotFound(message);
		}

		public string Task3(string filename)
		{
			string result = "";
			
			result += "Метод: " + HttpContext.Request.HttpMethod + "<br />";
			result += "Заголовки: <br />";
			foreach (string h in HttpContext.Request.Headers)
			{
				result += string.Format("{0}: {1}<br />", h, HttpContext.Request.Headers[h]);
			}
			result += string.Format("Барузер: {0}<br />", HttpContext.Request.Browser.Browser);

			return result;
		}

		public ActionResult Task4(string method)
		{
			int c = 0, s = 0;
			switch (method)
			{
				case "cookie":
					c = int.Parse(Request.Cookies["count"].Value);
					s = (int)Session["count"];
					c++;
					ViewBag.c = c;
					ViewBag.s = s;
					Response.Cookies["count"].Value = c.ToString();
					break;
				case "session":
					c = int.Parse(Request.Cookies["count"].Value);
					s = (int)Session["count"];
					s++;
					ViewBag.c = c;
					ViewBag.s = s;
					Session["count"] = s;
					break;
				default:
					ViewBag.c = c;
					ViewBag.s = s;
					Response.Cookies["count"].Value = c.ToString();
					Session["count"] = s;
					break;
			}
			return View();
		}
	}
}
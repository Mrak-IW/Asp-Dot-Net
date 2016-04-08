using Lab8.Models.MathSolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab8.Controllers
{
	public class HomeController : Controller
	{
		[HttpGet]
		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Index(string username = null)
		{
			if (username == "")
			{
				ViewBag.HelloText = "С анонимами не здороваюсь!";
			}
			else
			{
				ViewBag.HelloText = string.Format("Привет, {0}", username);
			}
			return View();
		}

		/// <summary>
		/// Задание 1. Решает квадратное уравнение с заданными параметрами
		/// </summary>
		/// <param name="a">Коэфициент при x^2</param>
		/// <param name="b">Коэфициент при x^1</param>
		/// <param name="c">Коэфициент при x^0</param>
		/// <returns>Массив корней уравнения</returns>
		public string Solve(double a, double b, double c)
		{
			//http://localhost:49963/home/solve?a=1&b=0&c=1
			string output = "";
			ComplexNumber[] result = SquareSolver.Solve(a, b, c);

			for (int i = 0; i < result.Length; i++)
			{
				output = string.Format("{2}x[{0}] = {1}<br />", i, result[i], output);
			}

			return output;
		}

		/// <summary>
		/// Задание 2. Выводит на экран переданный через GET массив.
		/// </summary>
		/// <param name="arr">Массив для вывода.</param>
		/// <returns>HTML-представление массива</returns>
		public string PrintArray(string[] arr)
		{
			//http://localhost:49963/home/printarray?arr[0]=кот&arr[1]=пёс&arr[2]=скунс
			string result = "<ol>";

			if (arr != null)
			{
				foreach (string s in arr)
				{
					result = string.Concat(result, "<li>", s, "</li>");
				}

				result += "</ol>";
			}
			else
			{
				result = string.Format("Передайте в контроллер массив в <a href=\"/home/printarray?arr[0]=кот&arr[1]=пёс&arr[2]=скунс\">формате</a>");
			}

			return result;
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Lab13.Controllers
{
	public class ValuesController : ApiController
	{
		// GET api/values

		public dynamic GetTask1_1_Task4()
		{
			//return string.Format("{0}:{1}:{2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
			return new { hour = DateTime.Now.Hour, minute = DateTime.Now.Minute, second = DateTime.Now.Second };
		}

		public string[] GetTask1_2(string param1)
		{
			string[] result = new string[1];
			result[0] = param1.Length > 0 ? param1 : "1";
			return result;
		}

		[HttpGet]
		public string[] Task1_3(string param1, string param2)
		{
			string[] result = new string[2];
			result[0] = param1.Length > 0 ? param1 : "2";
			result[1] = param2.Length > 0 ? param2 : "2";
			return result;
		}

		[Route("api/values/GetOne")]
		public string GetOne()
		{
			return "Task6 GetOne";
		}

		[Route("api/values/GetTwo")]
		public string GetTwo()
		{
			return "Task6 GetTwo";
		}

		[HttpGet]
		[Route("api/values/Mul/{a}/{b}")]
		public double Mul(double a, double b)
		{
			return a * b;
		}

		[HttpGet]
		[Route("api/values/Div/{a}/{b}")]
		public double Div(double a, double b)
		{
			return a / b;
		}

		// POST api/values
		public void Post([FromBody]string value)
		{
		}

		// PUT api/values/5
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/values/5
		public void Delete(int id)
		{
		}
	}
}

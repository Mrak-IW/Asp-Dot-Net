using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Lab13.Models
{
	public class Task8ActionResult : IHttpActionResult
	{
		private string[] array;

		public Task8ActionResult(string[] array)
		{
			this.array = array;
		}

		public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			string head = "<html><head><meta charset=utf-8/></head><body><ol>";
			string rear = "</ol></body></html>";
			string res = "";
			foreach (string str in array)
			{
				res += string.Format("<li>{0}</li>", str);
			}
			//Создаём объект ответа
			HttpResponseMessage response = new HttpResponseMessage();
			//Инициализируем тело ответа
			response.Content = new StringContent(string.Concat(head, res, rear));
			response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
			return Task.FromResult(response);
		}
	}
}
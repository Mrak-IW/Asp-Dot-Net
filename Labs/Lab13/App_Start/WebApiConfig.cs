using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Lab13
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Конфигурация и службы веб-API

			// Маршруты веб-API
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "Task1",
				routeTemplate: "webapi/{controller}"
			);

			config.Routes.MapHttpRoute(
				name: "Task2",
				routeTemplate: "api/math/{controller}/{param1}/{param2}",
				defaults: new { param2 = RouteParameter.Optional }
			);

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			// Отключаем возможность вывода данных в формате XML
			config.Formatters.Remove(config.Formatters.XmlFormatter);
		}
	}
}

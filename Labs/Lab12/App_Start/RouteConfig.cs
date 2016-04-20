using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Lab12
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.IgnoreRoute("Lab12-Index/forbidden");

			routes.MapRoute(
				name: "Task10",
				url: "{controller}/",
				defaults: new { action = "SuperAction", routeName = "Маршрут Task10" },
				constraints : new { controller = "^New.*" }
			);

			routes.MapRoute(
				name: "Task5",
				url: "MySite/{controller}~{action}/",
				defaults: new { routeName = "Маршрут Task5" }
			);

			routes.MapRoute(
				name: "Task2",
				url: "{controller}-{action}/{parameter}",
				defaults: new { routeName = "Маршрут Task2" }
			);

			routes.MapRoute(
				name: "Task8",
				url: "{controller}/{action}/{param1}/{param2}",
				defaults: new { routeName = "Маршрут Task8" }
			);

			routes.MapRoute(
				name: "Task7",
				url: "Task7",
				defaults: new { controller = "Lab12", action = "Task7", routeName = "Маршрут Task7" }
			);

			routes.MapRoute(
				name: "Task6",
				url: "Main/Run",
				defaults: new { controller = "Lab12", action = "Index", routeName = "Маршрут Task6" }
			);

			routes.MapRoute(
				name: "Task3",
				url: "{controller}/",
				defaults: new { action = "Task3", routeName = "Маршрут Task3" }
			);

			routes.MapRoute(
				name: "Task2Alt",
				url: "{controller}/{action}/{parameter}",
				defaults: new { routeName = "Маршрут Task2Alt" }
			);

			routes.MapRoute(
				name: "Task1",
				url: "{controller}/{action}",
				defaults: new { controller = "Lab12", action = "Index", routeName = "Маршрут Task1" }
			);

			routes.MapRoute(
				name: "Task9",
				url: "{controller}/{action}/{*catchall}",
				defaults: new { routeName = "Маршрут Task9" }
			);
		}
	}
}

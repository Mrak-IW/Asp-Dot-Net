﻿using System.Web;
using System.Web.Optimization;

namespace Homework2
{
	public class BundleConfig
	{
		// Дополнительные сведения об объединении см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js"));

			// Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
			// используйте средство построения на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js",
					  "~/Scripts/respond.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/devicons.css",
					  "~/Content/style.css"));

			bundles.Add(new ScriptBundle("~/bundles/btnHandlers").Include(
								  "~/Scripts/AjaxScripts/deviceBtnHandlers.js",
								  "~/Scripts/AjaxScripts/addDeviceBtnHandlers.js"));
		}
	}
}

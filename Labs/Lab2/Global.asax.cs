﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Lab2
{
	public class Global : System.Web.HttpApplication
	{

		protected void Application_Start(object sender, EventArgs e)
		{

		}

		protected void Session_Start(object sender, EventArgs e)
		{
			FileStream fs;
			string path = Request.PhysicalApplicationPath + "Output\\Sessions.log";

			if (File.Exists(path))
			{
				fs = File.Open(path, FileMode.Append);
			}
			else
			{
				fs = File.Open(path, FileMode.Create);
			}

			using (StreamWriter sw = new StreamWriter(fs))
			{
				sw.WriteLine("Сессия стартовала: {0}", System.DateTime.Now);
			}
		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{

		}

		protected void Application_Error(object sender, EventArgs e)
		{

		}

		protected void Session_End(object sender, EventArgs e)
		{
			FileStream fs;
			string path = Request.PhysicalApplicationPath + "Output\\Sessions.log";

			if (File.Exists(path))
			{
				fs = File.Open(path, FileMode.Append);
			}
			else
			{
				fs = File.Open(path, FileMode.Create);
			}

			using (StreamWriter sw = new StreamWriter(fs))
			{
				sw.WriteLine("Сессия завершилась: {0}", System.DateTime.Now);
			}
		}

		protected void Application_End(object sender, EventArgs e)
		{

		}
	}
}
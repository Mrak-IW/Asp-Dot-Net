using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Configuration;

namespace Lab4
{
	public partial class Default : System.Web.UI.Page
	{
		public MyConfig myConfigSection;

		protected void Page_Load(object sender, EventArgs e)
		{
			Configuration config = WebConfigurationManager.OpenWebConfiguration(Request.ApplicationPath);
			myConfigSection = config.GetSection("MyConfig") as MyConfig;

			if (IsPostBack)
			{
				btnRedirect.Click += btnRedirect_OnClick;
				btnTransfer.Click += btnTransfer_OnClick;
			}
		}

		protected void btnRedirect_OnClick(object sender, EventArgs e)
		{
			Response.Redirect("~/Response.aspx");
		}
		protected void btnTransfer_OnClick(object sender, EventArgs e)
		{
			Server.Transfer("~/Server.aspx");
		}
	}
}
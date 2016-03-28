using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab3
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//if (!IsPostBack)
			{
				PhTask4.Controls.Add(new CustomList("Яблоко", "Груша", "Лампочка"));
				Control sc = new SexChanger();
				sc.ID = "Task4";
				PhTask4.Controls.AddAt(0, sc);
			}
		}

		protected string GetHtmlList(params string[] args)
		{
			StringBuilder htmlResult = new StringBuilder();
			htmlResult.Append("<ol>");
			foreach (string s in args)
			{
				htmlResult.Append(string.Format("<li>{0}</li>", s));
			}
			htmlResult.Append("</ol>");

			return htmlResult.ToString();
		}
	}
}
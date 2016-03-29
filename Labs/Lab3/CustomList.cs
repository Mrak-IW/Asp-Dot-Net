using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab3
{
	public class CustomList : Control
	{
		string[] fields = new string[3];

		public CustomList()
		{
			fields[0] = "строка 0";
			fields[1] = "строка 1";
			fields[2] = "строка 2";
		}

		public CustomList(params string[] args)
		{
			if (args != null)
			{
				for (int i = 0; i < args.Length && i < fields.Length; i++)
				{
					fields[i] = args[i];
				}
			}
		}

		protected override void Render(HtmlTextWriter writer)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Ol);
			for (int i = 0; i < fields.Length; i++)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Li);
				writer.Write(fields[i]);
				writer.RenderEndTag();
			}
			writer.RenderEndTag();
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab11.Helpers
{
	public static class ListHelper
	{
		public static MvcHtmlString CreateList(this HtmlHelper html, int[] items)
		{
			string result = "";
			if (items != null)
			{
				TagBuilder ul = new TagBuilder("ul");
				foreach (int i in items)
				{
					TagBuilder li = new TagBuilder("li");
					li.SetInnerText(i.ToString());
					ul.InnerHtml += li;
				}

				result = ul.ToString();
			}
			return new MvcHtmlString(result);
		}
	}
}
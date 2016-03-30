using HomeWork2.SmartHouseDir.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Homework1
{
	public partial class WfDevice : System.Web.UI.UserControl
	{
		public ISmartDevice Device { get; set; }

		public WfDevice()
		{
		}

		public WfDevice(ISmartDevice device)
		{
			Device = device;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Device != null)
			{
			}
			else
			{
				Label err = new Label();
				err.Text = "Для данного элемента управления не задано конкретное устройство";
				Controls.Add(err);
			}
		}
	}
}
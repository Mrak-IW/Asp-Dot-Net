using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HomeWork2.SmartHouseDir.Classes;
using HomeWork2.SmartHouseDir.Enums;
using HomeWork2.SmartHouseDir.Interfaces;
using HomeWork2.SmartHouseDir.Classes.InternalParts;

namespace Homework1
{
	public partial class Default : System.Web.UI.Page
	{
		SmartHouse sh;
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Session["SmartHouse"] == null)
			{
				sh = new SmartHouse();
				sh.AddDevice(new SmartLamp("l1", new Dimmer(100, 10, 10)));
				sh.AddDevice(new SmartLamp("l2", new Dimmer(100, 10, 15)));
				sh.AddDevice(new SmartLamp("l3", new Dimmer(100, 10, 10)));
				sh.AddDevice(new SmartLamp("l4", new Dimmer(100, 10, 10)));
				sh.AddDevice(new SmartLamp("l5", new Dimmer(100, 10, 15)));
				sh.AddDevice(new SmartLamp("l6", new Dimmer(100, 10, 15)));
				sh.AddDevice(new Fridge("fr", new Dimmer(0, -5, 1)));
				sh.AddDevice(new Clock("clk"));
				sh["fr"].On();
				sh["clk"].On();
				(sh["fr"] as IHaveThermostat).Temperature= (sh["fr"] as IHaveThermostat).TempMin;

				Session.Add("SmartHouse", sh);
			}
			else
			{
				sh = Session["SmartHouse"] as SmartHouse;
			}

			foreach (ISmartDevice dev in sh)
			{
				WfDevice c = Page.LoadControl("~/WfDevice.ascx") as WfDevice;
				c.Device = dev;
				PhDevices.Controls.Add(c);
			}

			lblDeviceCount.Text = string.Format("Устройств в системе: {0}", sh.Count);
			btnAddDevice.Click += btnAddDevice_OnClick;
		}

		protected void btnAddDevice_OnClick(object sender, EventArgs e)
		{
			PhDevices.Controls.Add(new WfAddDevice().LoadControl("~/WfAddDevice.ascx"));
		}
	}
}
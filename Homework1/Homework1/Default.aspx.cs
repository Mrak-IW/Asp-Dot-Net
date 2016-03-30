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
		protected void Page_Load(object sender, EventArgs e)
		{
			SmartHouse sh = new SmartHouse();
			sh.AddDevice(new SmartLamp("l1", new Dimmer(100, 10, 10)));
			sh.AddDevice(new SmartLamp("l2", new Dimmer(100, 10, 15)));
			sh.AddDevice(new Fridge("fr", new Dimmer(0, -5, 1)));
			sh.AddDevice(new Clock("clk"));
			sh["fr"].On();
			sh["clk"].On();
			(sh["fr"] as IHaveThermostat).DecreaseTemperature();

			PhDevices.Controls.Add(new WfDevice());
		}
	}
}
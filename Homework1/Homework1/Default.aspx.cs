using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HomeWorkSmartHouse.SmartHouseDir.Classes;
using HomeWorkSmartHouse.SmartHouseDir.Enums;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using HomeWorkSmartHouse.SmartHouseDir.Classes.InternalParts;

namespace Homework1
{
	public partial class Default : System.Web.UI.Page
	{
		SmartHouse sh;
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				Session["showAddDevice"] = null;
			}

			if (Session["SmartHouse"] == null)
			{
				sh = new SmartHouse();
				sh.AddDevice(new SmartLamp("l1", new Dimmer(100, 10, 10)));
				sh.AddDevice(new SmartLamp("l2", new Dimmer(100, 10, 15)));
				sh.AddDevice(new Fridge("fr1", new Dimmer(0, -5, 1)));
				sh.AddDevice(new Clock("clk1"));
				sh["fr1"].On();
				sh["clk1"].On();
				(sh["fr1"] as IHaveThermostat).Temperature = (sh["fr1"] as IHaveThermostat).TempMin;

				Session.Add("SmartHouse", sh);
			}
			else
			{
				sh = Session["SmartHouse"] as SmartHouse;
			}

			RefreshControls();
		}

		public void RefreshControls()
		{
			PhDevices.Controls.Clear();
			foreach (ISmartDevice dev in sh)
			{
				WfDevice c = Page.LoadControl("~/Controls/WfDevice.ascx") as WfDevice;
				c.Device = dev;
				PhDevices.Controls.Add(c);
			}

			lblDeviceCount.Text = string.Format("Устройств в системе: {0}", sh.Count);
			btnAddDevice.Click += btnAddDevice_OnClick;

			bool? show = Session["showAddDevice"] as bool?;
			if (show != null && show == true)
			{
				ShowAddDevice();
			}
		}

		protected void btnAddDevice_OnClick(object sender, EventArgs e)
		{
			ShowAddDevice();
			Session["showAddDevice"] = true;
		}

		protected void ShowAddDevice()
		{
			if (FindControl("frmCreateDevice") == null)
			{
				WfAddDevice form = Page.LoadControl("~/Controls/WfAddDevice.ascx") as WfAddDevice;
				form.DevType = ddlDeviceType.SelectedValue;
				form.SmartHouse = sh;
				form.ParentForm = this;
				form.ID = "frmCreateDevice";
				PhDevices.Controls.Add(form);
			}
		}
	}
}
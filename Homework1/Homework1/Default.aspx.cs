using System;
using System.Web.UI;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using Homework1.Classes;
using System.Reflection;

namespace Homework1
{
	public partial class Default : System.Web.UI.Page
	{
		private ISmartHouse sh;

		protected void Page_Load(object sender, EventArgs e)
		{
			ISmartHouseCreator shc = Manufacture.GetManufacture(Assembly.Load("SmartHouse"));

			if (!IsPostBack)
			{
				Session["showAddDevice"] = null;
			}

			if (Session["SmartHouse"] == null)
			{
				ISmartDevice dev;
				IBrightable ibri;
				IHaveThermostat iterm;

				sh = shc.CreateSmartHouse();

				dev = shc.CreateDevice("SmartLamp", "l1");

				ibri = dev as IBrightable;
				ibri.BrightnessMax = 100;
				ibri.BrightnessMin = 10;
				ibri.BrightnessStep = 10;
				ibri.Brightness = ibri.BrightnessMax;
				sh.AddDevice(dev);

				dev = shc.CreateDevice("SmartLamp", "l2");

				ibri = dev as IBrightable;
				ibri.BrightnessMax = 100;
				ibri.BrightnessMin = 10;
				ibri.BrightnessStep = 15;
				ibri.Brightness = ibri.BrightnessMax;
				sh.AddDevice(dev);

				dev = shc.CreateDevice("Fridge", "fr1");

				iterm = dev as IHaveThermostat;
				iterm.TempMax = 0;
				iterm.TempMin = -5;
				iterm.TempStep = 1;
				dev.On();
				iterm.DecreaseTemperature();
				sh.AddDevice(dev);

				dev = shc.CreateDevice("Clock", "clk1");

				dev.On();
				sh.AddDevice(dev);

				Session.Add("SmartHouse", sh);
			}
			else
			{
				sh = Session["SmartHouse"] as ISmartHouse;
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

			lblDeviceCount.Text = string.Format("Устройств в системе: {0}<br />Добавить ещё одно:", sh.Count);
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
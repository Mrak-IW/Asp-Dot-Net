using HomeWorkSmartHouse.SmartHouseDir.Enums;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Homework1
{
	public partial class WfDevice : System.Web.UI.UserControl
	{
		public ISmartDevice Device { get; set; }
		public string templatePath = "~/Controls/WfDevice.ascx";

		protected void Page_Load(object sender, EventArgs e)
		{
			this.ID = "devCard" + Device.Name;
			BuildControlMarkup();
		}

		protected void ResetSubControls(string formPath)
		{
			Controls.Clear();
			Control tmp = LoadControl(formPath);
			while (tmp.Controls.Count != 0)
			{
				Controls.Add(tmp.Controls[0]);
			}
			GetControlLinks(Controls);
			//Это было заменено на универсальный алгоритм с рефлексией
			//Теперь можно сюда не лазать, если поменяется шаблон контрола
			//LblId = FindControl("LblId") as Label;
			//btnPowerState = FindControl("btnPowerState") as Button;
			//imgDevIcon = FindControl("imgDevIcon") as Image;
			//pnlProperties = FindControl("pnlProperties") as Table;
		}

		protected void GetControlLinks(ControlCollection controls)
		{
			Type thisType = this.GetType();

			foreach (Control ctrl in controls)
			{
				if (ctrl.ID != null)
				{
					FieldInfo fi = thisType.GetField(ctrl.ID, BindingFlags.Instance | BindingFlags.NonPublic);
					if (fi != null)
					{
						fi.SetValue(this, ctrl);
					}
				}

				if (ctrl.Controls != null && ctrl.Controls.Count > 0)
				{
					GetControlLinks(ctrl.Controls);
				}
			}
		}


		protected void BuildControlMarkup()
		{
			if (Device != null)
			{
				DisplayISmartDevice();
				DisplayIcon();
				DisplayIHaveThermostat(pnlProperties);
				DisplayIBrightable(pnlProperties);
				DisplayIOpenCloseable(pnlProperties);
				DisplayRemoveButton(pnlProperties);
			}
			else
			{
				LblId.Text = "Для данного элемента управления не задано конкретное устройство";
			}
		}

		protected void DisplayISmartDevice()
		{
			Button b;

			LblId.Text = Device.DeviceType + "<br /> \"" + Device.Name + "\"";

			b = btnPowerState;
			b.ID = "btnPower" + Device.Name;
			b.CssClass = "btnPower";
			switch (Device.State)
			{
				case EPowerState.On:
					b.ToolTip = "Выключить";
					b.Attributes.Add("state", "on");

					b.Click += (senderCtrl, eargs) =>
					{
						Device.Off();
						ResetSubControls(templatePath);
						BuildControlMarkup();
					};

					break;
				case EPowerState.Off:
					b.ToolTip = "Включить";
					b.Attributes.Add("state", "off");

					b.Click += (senderCtrl, eargs) =>
					{
						Device.On();
						ResetSubControls(templatePath);
						BuildControlMarkup();
					};
					break;
			}
		}


		protected void DisplayIcon()
		{
			Panel icon = new Panel();
			icon.ID = "devIcon" + Device.Name;
			icon.CssClass = "devIcon";
			icon.Attributes["devtype"] = Device.GetType().Name;

			if (Device.State == EPowerState.On)
			{
				icon.Attributes["on"] = "on";
			}

			if (Device is IOpenCloseable)
			{
				IOpenCloseable ioc = Device as IOpenCloseable;
				if (ioc.IsOpened)
				{
					icon.Attributes["open"] = "open";
				}
			}

			phIcon.Controls.Add(icon);

			DisplayIHaveClock(icon);
		}

		protected void DisplayRemoveButton(Control destination)
		{
			Button b;
			Panel tr = new Panel();

			b = new Button();
			b.Text = "Удалить";
			b.ToolTip = "Выкинуть в окно";
			b.ID = "btnRemove" + Device.Name;
			b.CssClass = "btnSwitch";

			b.Click += (senderCtrl, eargs) =>
			{
				Device.Parent.RemoveDevice(Device.Name);
				Parent.Controls.Remove(this);
			};

			tr.Controls.Add(b);

			destination.Controls.Add(tr);
		}

		protected void DisplayIHaveClock(Control destination)
		{
			if (Device.State == EPowerState.On && Device is IHaveClock)
			{
				IHaveClock iclk = Device as IHaveClock;
				Label time = new Label();
				time.Text = string.Format("{0:D2} {1:D2}", iclk.Time.Hour, iclk.Time.Minute);
				destination.Controls.Add(time);
			}
		}

		protected void DisplayIBrightable(Control destination)
		{
			if (Device is IBrightable)
			{
				IBrightable dev = Device as IBrightable;
				Label td;
				Button b;
				Panel tr = new Panel();

				b = new Button();
				b.ID = "btnBrightnessDec" + Device.Name;
				b.ToolTip = "Min = " + dev.BrightnessMin;
				b.CssClass = "btnArrow btnArrowLeft";

				b.Click += (senderCtrl, eargs) =>
				{
					dev.DecreaseBrightness();
					ResetSubControls(templatePath);
					BuildControlMarkup();
				};
				tr.Controls.Add(b);

				//================

				td = new Label();
				td.CssClass = "value";
				td.Text = dev.Brightness.ToString();
				tr.Controls.Add(td);

				//================

				b = new Button();
				b.ID = "btnBrightnessInc" + Device.Name;
				b.ToolTip = "Max = " + dev.BrightnessMax;
				b.CssClass = "btnArrow btnArrowRight";

				b.Click += (senderCtrl, eargs) =>
				{
					dev.IncreaseBrightness();
					ResetSubControls(templatePath);
					BuildControlMarkup();
				};
				tr.Controls.Add(b);

				td = new Label();
				td.Text = "Яркость";
				tr.Controls.Add(td);

				destination.Controls.Add(tr);
			}
			else
			{ }
		}

		protected void DisplayIHaveThermostat(Control destination)
		{
			if (Device is IHaveThermostat)
			{
				IHaveThermostat dev = Device as IHaveThermostat;
				Label td;
				Button b;
				Panel tr = new Panel();

				b = new Button();
				b.ID = "btnTemperatureDec" + Device.Name;
				b.Attributes["title"] = "Min = " + dev.TempMin;
				b.CssClass = "btnArrow btnArrowLeft";

				b.Click += (senderCtrl, eargs) =>
				{
					dev.DecreaseTemperature();
					ResetSubControls(templatePath);
					BuildControlMarkup();
				};
				tr.Controls.Add(b);

				td = new Label();
				td.CssClass = "value";
				td.Text = dev.Temperature.ToString();
				tr.Controls.Add(td);

				b = new Button();
				b.ID = "btnTemperatureInc" + Device.Name;
				b.Attributes["title"] = "Max = " + dev.TempMax;
				b.CssClass = "btnArrow btnArrowRight";

				b.Click += (senderCtrl, eargs) =>
				{
					dev.IncreaseTemperature();
					ResetSubControls(templatePath);
					BuildControlMarkup();
				};
				tr.Controls.Add(b);

				td = new Label();
				td.Text = "Температура";
				tr.Controls.Add(td);

				destination.Controls.Add(tr);
			}
			else
			{ }
		}

		protected void DisplayIOpenCloseable(Control destination)
		{
			if (Device is IOpenCloseable)
			{
				IOpenCloseable dev = Device as IOpenCloseable;
				Button b;
				Panel tr = new Panel();

				b = new Button();
				b.ToolTip = dev.IsOpened ? "Закрыть" : "Открыть";
				b.Text = dev.IsOpened ? "Открыто" : "Закрыто";
				b.ID = "btnOpenClose" + Device.Name;
				b.CssClass = "btnSwitch";

				b.Click += (senderCtrl, eargs) =>
				{
					dev.IsOpened = !dev.IsOpened;
					ResetSubControls(templatePath);
					BuildControlMarkup();
				};
				tr.Controls.Add(b);

				destination.Controls.Add(tr);
			}
			else
			{ }
		}
	}
}
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
		public string templatePath = "~/WfDevice.ascx";

		protected void Page_Load(object sender, EventArgs e)
		{
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
			ImageButton b;

			LblId.Text = Device.DeviceType + "<br /> \"" + Device.Name + "\"";

			b = btnPowerState;
			b.ID = "btnPower" + Device.Name;
			switch (Device.State)
			{
				case EPowerState.On:
					b.ToolTip = "Выключить";
					b.ImageUrl = "~/Images/btnRoundLight.png";
					b.Attributes.Add("onmouseover", "this.src='Images/btnRoundLightPush.png';");
					b.Attributes.Add("onmouseout", "this.src='Images/btnRoundLight.png';");

					b.Click += (senderCtrl, eargs) =>
					{
						Device.Off();
						ResetSubControls(templatePath);
						BuildControlMarkup();
					};
					
					break;
				case EPowerState.Off:
					b.ToolTip = "Включить";
					b.ImageUrl = "~/Images/btnRoundDark.png";
					b.Attributes.Add("onmouseover", "this.src='Images/btnRoundDarkPush.png';");
					b.Attributes.Add("onmouseout", "this.src='Images/btnRoundDark.png';");

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
			Image icon = new Image();
			icon.ID = "imgDevIcon" + Device.Name;
			icon.ImageUrl = "Images/lampIcon.png";
			PhIcon.Controls.Add(icon);
		}

		protected void DisplayRemoveButton(Control destination)
		{
			Button b;
			Panel tr = new Panel();

			b = new Button();
			b.Text = "Удалить";
			b.ID = "btnRemove" + Device.Name;
			b.Click += (senderCtrl, eargs) =>
			{
				Device.Parent.RemoveDevice(Device.Name);
				Parent.Controls.Remove(this);
			};

			tr.Controls.Add(b);

			destination.Controls.Add(tr);
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
				b.Text = "-";
				b.ID = "btnBrightnessDec" + Device.Name;
				b.Attributes["title"] = "Min = " + dev.BrightnessMin;
				b.Click += (senderCtrl, eargs) =>
				{
					dev.DecreaseBrightness();
					ResetSubControls(templatePath);
					BuildControlMarkup();
				};
				tr.Controls.Add(b);

				td = new Label();
				td.CssClass = "value";
				td.Text = dev.Brightness.ToString();
				tr.Controls.Add(td);

				b = new Button();
				b.Text = "+";
				b.ID = "btnBrightnessInc" + Device.Name;
				b.Attributes["title"] = "Max = " + dev.BrightnessMax;
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
				b.Text = "-";
				b.ID = "btnTemperatureDec" + Device.Name;
				b.Attributes["title"] = "Min = " + dev.TempMin;
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
				b.Text = "+";
				b.ID = "btnTemperatureInc" + Device.Name;
				b.Attributes["title"] = "Max = " + dev.TempMax;
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
				Label td;
				Button b;
				Panel tr = new Panel();

				td = new Label();
				{
					b = new Button();
					b.Text = dev.IsOpened ? "Закрыть" : "Открыть";
					b.ID = "btnOpenClose" + Device.Name;
					b.Click += (senderCtrl, eargs) =>
					{
						dev.IsOpened = !dev.IsOpened;
						ResetSubControls(templatePath);
						BuildControlMarkup();
					};
					td.Controls.Add(b);
				}
				tr.Controls.Add(td);

				td = new Label();
				td.Text = dev.IsOpened ? "Открыто" : "Закрыто";
				td.Attributes["colspan"] = "2";
				tr.Controls.Add(td);

				destination.Controls.Add(tr);
			}
		}
	}
}
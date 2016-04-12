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
			Panel icon = new Panel();
			icon.ID = "devIcon" + Device.Name;
			icon.CssClass = "devIcon";
			phIcon.Controls.Add(icon);
		}

		protected void DisplayRemoveButton(Control destination)
		{
			ImageButton b;
			Panel tr = new Panel();

			b = new ImageButton();
			b.ToolTip = "Выкинуть в окно";
			b.ID = "btnRemove" + Device.Name;
			b.CssClass = "btnSwitch";

			b.ImageUrl = "~/Images/btnRoundDark.png";
			b.Attributes.Add("onmouseover", "this.src='Images/btnRoundLight.png';");
			b.Attributes.Add("onmouseout", "this.src='Images/btnRoundDark.png';");
			b.Attributes.Add("onmousedown", "this.src='Images/btnRoundLightPush.png';");

			b.Click += (senderCtrl, eargs) =>
			{
				Device.Parent.RemoveDevice(Device.Name);
				Parent.Controls.Remove(this);
			};

			tr.Controls.Add(b);

			Label l = new Label();
			l.Text = "Удалить";
			l.Attributes["colspan"] = "2";
			tr.Controls.Add(l);

			destination.Controls.Add(tr);
		}

		protected void DisplayIBrightable(Control destination)
		{
			if (Device is IBrightable)
			{
				IBrightable dev = Device as IBrightable;
				Label td;
				ImageButton b;
				Panel tr = new Panel();

				b = new ImageButton();
				b.ID = "btnBrightnessDec" + Device.Name;
				b.ToolTip = "Min = " + dev.BrightnessMin;
				b.CssClass = "btnArrow";

				b.ImageUrl = "~/Images/btnArrowLeftDark.png";
				b.Attributes.Add("onmouseover", "this.src='Images/btnArrowLeftLight.png';");
				b.Attributes.Add("onmouseout", "this.src='Images/btnArrowLeftDark.png';");

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

				b = new ImageButton();
				b.ID = "btnBrightnessInc" + Device.Name;
				b.ToolTip = "Max = " + dev.BrightnessMax;
				b.CssClass = "btnArrow";

				b.ImageUrl = "~/Images/btnArrowRightDark.png";
				b.Attributes.Add("onmouseover", "this.src='Images/btnArrowRightLight.png';");
				b.Attributes.Add("onmouseout", "this.src='Images/btnArrowRightDark.png';");

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
				ImageButton b;
				Panel tr = new Panel();

				b = new ImageButton();
				b.CssClass = "btnArrow";
				b.ID = "btnTemperatureDec" + Device.Name;
				b.Attributes["title"] = "Min = " + dev.TempMin;

				b.ImageUrl = "~/Images/btnArrowLeftDark.png";
				b.Attributes.Add("onmouseover", "this.src='Images/btnArrowLeftLight.png';");
				b.Attributes.Add("onmouseout", "this.src='Images/btnArrowLeftDark.png';");

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

				b = new ImageButton();
				b.ID = "btnTemperatureInc" + Device.Name;
				b.Attributes["title"] = "Max = " + dev.TempMax;
				b.CssClass = "btnArrow";

				b.ImageUrl = "~/Images/btnArrowRightDark.png";
				b.Attributes.Add("onmouseover", "this.src='Images/btnArrowRightLight.png';");
				b.Attributes.Add("onmouseout", "this.src='Images/btnArrowRightDark.png';");

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
				ImageButton b;
				Panel tr = new Panel();

				b = new ImageButton();
				b.ToolTip = dev.IsOpened ? "Закрыть" : "Открыть";
				b.ID = "btnOpenClose" + Device.Name;
				b.CssClass = "btnSwitch";

				b.ImageUrl = "~/Images/btnRoundDark.png";
				b.Attributes.Add("onmouseover", "this.src='Images/btnRoundLight.png';");
				b.Attributes.Add("onmouseout", "this.src='Images/btnRoundDark.png';");
				b.Attributes.Add("onmousedown", "this.src='Images/btnRoundLightPush.png';");

				b.Click += (senderCtrl, eargs) =>
				{
					dev.IsOpened = !dev.IsOpened;
					ResetSubControls(templatePath);
					BuildControlMarkup();
				};
				tr.Controls.Add(b);


				td = new Label();
				td.Text = dev.IsOpened ? "Открыто" : "Закрыто";
				td.Attributes["colspan"] = "2";
				tr.Controls.Add(td);

				destination.Controls.Add(tr);
			}
			else
			{ }
		}
	}
}
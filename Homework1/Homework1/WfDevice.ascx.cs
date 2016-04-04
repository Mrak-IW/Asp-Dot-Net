using HomeWork2.SmartHouseDir.Enums;
using HomeWork2.SmartHouseDir.Interfaces;
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
			foreach (Control c in tmp.Controls)
			{
				Controls.Add(c);
			}
			GetControlLinks(Controls);
			//Это было заменено на универсальный алгоритм с рефлексией
			//Теперь можно сюда не лазать, если поменяется шаблон контрола
			//LblId = FindControl("LblId") as Label;
			//btnPowerState = FindControl("btnPowerState") as Button;
			//imgDevIcon = FindControl("imgDevIcon") as Image;
			//tblPropertiesTable = FindControl("tblPropertiesTable") as Table;
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
				DisplayIHaveThermostat(tblPropertiesTable);
				DisplayIBrightable(tblPropertiesTable);
				DisplayIOpenCloseable(tblPropertiesTable);
			}
			else
			{
				LblId.Text = "Для данного элемента управления не задано конкретное устройство";
			}
		}

		protected void DisplayISmartDevice()
		{
			Button b;

			LblId.Text = Device.DeviceType + " \"" + Device.Name + "\"";

			b = btnPowerState;
			switch (Device.State)
			{
				case EPowerState.On:
					b.Text = "On";
					b.Attributes.Add("title", "выключить");
					b.Click += (senderCtrl, eargs) =>
					{
						Device.Off();
						ResetSubControls(templatePath);
						BuildControlMarkup();
					};
					break;
				case EPowerState.Off:
					b.Text = "Off";
					b.Attributes.Add("title", "включить");
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
			icon.ID = "imgDevIcon";
			icon.ImageUrl = "Images/lampIcon.png";
			PhIcon.Controls.Add(icon);
		}

		protected void DisplayIBrightable(Table destination)
		{
			if (Device is IBrightable)
			{
				IBrightable dev = Device as IBrightable;
				TableCell td;
				Button b;
				TableRow tr = new TableRow();

				td = new TableCell();
				td.Text = "Яркость";
				tr.Cells.Add(td);

				td = new TableCell();
				td.Text = dev.Brightness.ToString();
				tr.Cells.Add(td);

				td = new TableCell();
				{
					b = new Button();
					b.Text = "+";
					b.ID = "btnBrightnessInc";
					b.Attributes["title"] = "Max = " + dev.BrightnessMax;
					b.Click += (senderCtrl, eargs) =>
					{
						dev.IncreaseBrightness();
						ResetSubControls(templatePath);
						BuildControlMarkup();
					};
					td.Controls.Add(b);

					b = new Button();
					b.Text = "-";
					b.ID = "btnBrightnessDec";
					b.Attributes["title"] = "Min = " + dev.BrightnessMin;
					b.Click += (senderCtrl, eargs) =>
					{
						dev.DecreaseBrightness();
						ResetSubControls(templatePath);
						BuildControlMarkup();
					};
					td.Controls.Add(b);
				}
				tr.Cells.Add(td);

				destination.Rows.Add(tr);
			}
			else
			{ }
		}

		protected void DisplayIHaveThermostat(Table destination)
		{
			if (Device is IHaveThermostat)
			{
				IHaveThermostat dev = Device as IHaveThermostat;
				TableCell td;
				Button b;
				TableRow tr = new TableRow();

				td = new TableCell();
				td.Text = "Температура";
				tr.Cells.Add(td);

				td = new TableCell();
				td.Text = dev.Temperature.ToString();
				tr.Cells.Add(td);

				td = new TableCell();
				{
					b = new Button();
					b.Text = "+";
					b.ID = "btnTemperatureInc";
					b.Attributes["title"] = "Max = " + dev.TempMax;
					b.Click += (senderCtrl, eargs) =>
					{
						dev.IncreaseTemperature();
						ResetSubControls(templatePath);
						BuildControlMarkup();
					};
					td.Controls.Add(b);

					b = new Button();
					b.Text = "-";
					b.ID = "btnTemperatureDec";
					b.Attributes["title"] = "Min = " + dev.TempMin;
					b.Click += (senderCtrl, eargs) =>
					{
						dev.DecreaseTemperature();
						ResetSubControls(templatePath);
						BuildControlMarkup();
					};
					td.Controls.Add(b);
				}
				tr.Cells.Add(td);

				destination.Rows.Add(tr);
			}
			else
			{ }
		}

		protected void DisplayIOpenCloseable(Table destination)
		{
			if (Device is IOpenCloseable)
			{
				IOpenCloseable dev = Device as IOpenCloseable;
				TableCell td;
				Button b;
				TableRow tr = new TableRow();

				td = new TableCell();
				td.Text = dev.IsOpened ? "Открыто" : "Закрыто";
				td.Attributes["colspan"] = "2";
				tr.Cells.Add(td);

				td = new TableCell();
				{
					b = new Button();
					b.Text = dev.IsOpened ? "Закрыть" : "Открыть";
					b.ID = "btnOpenClose";
					b.Click += (senderCtrl, eargs) =>
					{
						dev.IsOpened = !dev.IsOpened;
						ResetSubControls(templatePath);
						BuildControlMarkup();
					};
					td.Controls.Add(b);
				}
				tr.Cells.Add(td);

				destination.Rows.Add(tr);
			}
		}
	}
}
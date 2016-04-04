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
	public partial class WfAddDevice : System.Web.UI.UserControl
	{
		public ISmartDevice Device { get; set; }
		public string templatePath = "~/WfAddDevice.ascx";

		protected void Page_Load(object sender, EventArgs e)
		{
			ddlDeviceType.SelectedIndexChanged += (senderCtrl, eventArgs) =>
			{
				ResetSubControls(templatePath);
				BuildControlMarkup();
			};

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
			//Assembly ass = Assembly.Load("HomeWork2");   //Да, я в курсе, как это звучит :-)
			//Type[] types = ass.GetTypes();
			//Type type = types.Where(item => item.Name == ddlDeviceType.SelectedValue).FirstOrDefault() as Type;

			DisplayISmartDevice(tblPropertiesTable);
			DisplayIcon();
			//DisplayIHaveThermostat(tblPropertiesTable);
			//DisplayIBrightable(tblPropertiesTable);
			//DisplayIOpenCloseable(tblPropertiesTable);
			switch (ddlDeviceType.SelectedValue)
			{
				case "Fridge":
					break;
				case "SmartLamp":
					DisplayIBrightable(tblPropertiesTable);
					break;
				case "Clock":
					break;
				default:
					break;
			}
		}

		protected void DisplayISmartDevice(Table destination)
		{
			TableCell td;
			TableRow tr = new TableRow();
			td = new TableCell();
			td.Text = "Имя устройства";
			tr.Controls.Add(td);

			td = new TableCell();
			TextBox tb = new TextBox();
			tb.ID = "tbName";
			tb.Attributes["placeholder"] = "Ввести имя";
			td.Controls.Add(tb);
			tr.Controls.Add(td);

			destination.Controls.Add(tr);
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
				TableRow tr = new TableRow();
				TextBox tb;

				td = new TableCell();
				td.Text = "Яркость min";
				tr.Cells.Add(td);

				td = new TableCell();
				tb = new TextBox();
				tb.ID = "tbBrightnessMin";
				tb.Attributes["placeholder"] = "0";
				td.Controls.Add(tb);

				tr.Cells.Add(td);

				destination.Rows.Add(tr);

				td = new TableCell();
				td.Text = "Яркость max";
				tr.Cells.Add(td);

				td = new TableCell();
				tb = new TextBox();
				tb.ID = "tbBrightnessMax";
				tb.Attributes["placeholder"] = "100";
				td.Controls.Add(tb);

				tr.Cells.Add(td);

				destination.Rows.Add(tr);
			}
			else
			{ }
		}

		//protected void DisplayIHaveThermostat(Table destination)
		//{
		//	if (Device is IHaveThermostat)
		//	{
		//		IHaveThermostat dev = Device as IHaveThermostat;
		//		TableCell td;
		//		Button b;
		//		TableRow tr = new TableRow();

		//		td = new TableCell();
		//		td.Text = "Температура";
		//		tr.Cells.Add(td);

		//		td = new TableCell();
		//		td.Text = dev.Temperature.ToString();
		//		tr.Cells.Add(td);

		//		td = new TableCell();
		//		{
		//			b = new Button();
		//			b.Text = "+";
		//			b.ID = "btnTemperatureInc";
		//			b.Attributes["title"] = "Max = " + dev.TempMax;
		//			b.Click += (senderCtrl, eargs) =>
		//			{
		//				dev.IncreaseTemperature();
		//				ResetSubControls(templatePath);
		//				BuildControlMarkup();
		//			};
		//			td.Controls.Add(b);

		//			b = new Button();
		//			b.Text = "-";
		//			b.ID = "btnTemperatureDec";
		//			b.Attributes["title"] = "Min = " + dev.TempMin;
		//			b.Click += (senderCtrl, eargs) =>
		//			{
		//				dev.DecreaseTemperature();
		//				ResetSubControls(templatePath);
		//				BuildControlMarkup();
		//			};
		//			td.Controls.Add(b);
		//		}
		//		tr.Cells.Add(td);

		//		destination.Rows.Add(tr);
		//	}
		//	else
		//	{ }
		//}

		//protected void DisplayIOpenCloseable(Table destination)
		//{
		//	if (Device is IOpenCloseable)
		//	{
		//		IOpenCloseable dev = Device as IOpenCloseable;
		//		TableCell td;
		//		Button b;
		//		TableRow tr = new TableRow();

		//		td = new TableCell();
		//		td.Text = dev.IsOpened ? "Открыто" : "Закрыто";
		//		td.Attributes["colspan"] = "2";
		//		tr.Cells.Add(td);

		//		td = new TableCell();
		//		{
		//			b = new Button();
		//			b.Text = dev.IsOpened ? "Закрыть" : "Открыть";
		//			b.ID = "btnOpenClose";
		//			b.Click += (senderCtrl, eargs) =>
		//			{
		//				dev.IsOpened = !dev.IsOpened;
		//				ResetSubControls(templatePath);
		//				BuildControlMarkup();
		//			};
		//			td.Controls.Add(b);
		//		}
		//		tr.Cells.Add(td);

		//		destination.Rows.Add(tr);
		//	}
		//}
	}
}
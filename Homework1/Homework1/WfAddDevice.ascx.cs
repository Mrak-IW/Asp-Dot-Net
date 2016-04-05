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
	public partial class WfAddDevice : System.Web.UI.UserControl
	{
		public ISmartHouse SmartHouse { get; set; }
		public string templatePath = "~/WfAddDevice.ascx";
		public string DevType { get; set; }

		public WfAddDevice()
		{ }

		public WfAddDevice(string devType)
		{
			DevType = devType;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (IsPostBack)
			{
				btnAddDevice.Click += btnAddDevice_OnClick;
			}

			BuildControlMarkup();
		}

		protected void btnAddDevice_OnClick(object sender, EventArgs e)
		{

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
			Assembly ass = Assembly.Load("HomeWork2");   //Да, я в курсе, как это звучит :-)
			Type[] types = ass.GetTypes();
			Type type = types.Where(item => item.Name == DevType).FirstOrDefault() as Type;

			DisplayISmartDevice(tblPropertiesTable);
			DisplayIcon();
			
			if (type.GetInterface("IBrightable") != null)
			{
				DisplayIBrightable(tblPropertiesTable);
			}
			if (type.GetInterface("IHaveThermostat") != null)
			{
				DisplayIHaveThermostat(tblPropertiesTable);
			}
			if (type.GetInterface("IHaveClock") != null)
			{
			}
			if (type.GetInterface("IOpenCloseable") != null)
			{
			}
			if (type.GetInterface("IRepearable") != null)
			{
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
			TableCell td;
			TextBox tb;
			TableRow tr;

			tr = new TableRow();

			td = new TableCell();
			td.Text = "Яркость min";
			tr.Cells.Add(td);

			td = new TableCell();
			tb = new TextBox();
			tb.ID = "tbBrightnessMin";
			tb.TextMode = TextBoxMode.Number;
			tb.Attributes["placeholder"] = "0";
			td.Controls.Add(tb);

			tr.Cells.Add(td);

			destination.Rows.Add(tr);

			tr = new TableRow();

			td = new TableCell();
			td.Text = "Яркость max";
			tr.Cells.Add(td);

			td = new TableCell();
			tb = new TextBox();
			tb.ID = "tbBrightnessMax";
			tb.TextMode = TextBoxMode.Number;
			tb.Attributes["placeholder"] = "100";
			td.Controls.Add(tb);

			tr.Cells.Add(td);

			destination.Rows.Add(tr);
		}

		protected void DisplayIHaveThermostat(Table destination)
		{
			TableCell td;
			TextBox tb;
			TableRow tr;

			tr = new TableRow();

			td = new TableCell();
			td.Text = "Температура min";
			tr.Cells.Add(td);

			td = new TableCell();
			tb = new TextBox();
			tb.ID = "tbTemperatureMin";
			tb.TextMode = TextBoxMode.Number;
			tb.Attributes["placeholder"] = "-273";
			td.Controls.Add(tb);

			tr.Cells.Add(td);

			destination.Rows.Add(tr);

			tr = new TableRow();

			td = new TableCell();
			td.Text = "Температура max";
			tr.Cells.Add(td);

			td = new TableCell();
			tb = new TextBox();
			tb.ID = "tbTemperatureMax";
			tb.TextMode = TextBoxMode.Number;
			tb.Attributes["placeholder"] = "10";
			td.Controls.Add(tb);

			tr.Cells.Add(td);

			destination.Rows.Add(tr);
		}
	}
}
using HomeWorkSmartHouse.SmartHouseDir.Classes;
using HomeWorkSmartHouse.SmartHouseDir.Classes.InternalParts;
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
		private const string idTempMax = "tbTemperatureMax";
		private const string idTempMin = "tbTemperatureMin";
		private const string idBrightMax = "tbBrightnessMax";
		private const string idBrightMin = "tbBrightnessMin";
		private const string idName = "tbName";

		public ISmartHouse SmartHouse { get; set; }
		public Default ParentForm { get; set; }
		public string templatePath = "~/Controls/WfAddDevice.ascx";
		public string DevType { get; set; }

		public WfAddDevice()
		{ }

		public WfAddDevice(string devType)
		{
			DevType = devType;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			BuildControlMarkup();

			btnAddDevice.Click += btnAddDevice_OnClick;
			btnClose.Click += btnClose_OnClick;
		}

		protected void btnClose_OnClick(object sender, EventArgs e)
		{
			Session["showAddDevice"] = null;
			ParentForm.RefreshControls();
		}

		protected void btnAddDevice_OnClick(object sender, EventArgs e)
		{
			string name;

			name = (FindControl(idName) as TextBox).Text;

			//TODO: Вот где-то здесь какая-то верификация должна быть. Наверное.
			switch (DevType)
			{
				case "Fridge":
					{
						int min, max, step;
						TextBox tbMin = FindControl(idTempMin) as TextBox;
						TextBox tbMax = FindControl(idTempMax) as TextBox;

						if (!int.TryParse(tbMax.Text, out max))
						{
							int.TryParse(tbMax.Attributes["placeholder"], out max);
						}
						if (!int.TryParse(tbMin.Text, out min))
						{
							int.TryParse(tbMin.Attributes["placeholder"], out min);
						}
						step = (max - min) / 5;
						if (step == 0)
						{
							step = 1;
						}
						if (step < 0)
						{
							step = -step;
						}
						Dimmer d = new Dimmer(max, min, step);

						ISmartDevice dev = new Fridge(name, d);

						SmartHouse.AddDevice(dev);
					}
					break;
				case "SmartLamp":
					{
						int min, max, step;

						TextBox tbMin = FindControl(idBrightMin) as TextBox;
						TextBox tbMax = FindControl(idBrightMax) as TextBox;

						if (!int.TryParse(tbMax.Text, out max))
						{
							int.TryParse(tbMax.Attributes["placeholder"], out max);
						}
						if (!int.TryParse(tbMin.Text, out min))
						{
							int.TryParse(tbMin.Attributes["placeholder"], out min);
						}
						step = (max - min) / 10;
						if (step == 0)
						{
							step = 1;
						}
						if (step < 0)
						{
							step = -step;
						}
						Dimmer d = new Dimmer(max, min, step);

						ISmartDevice dev = new SmartLamp(name, d);

						SmartHouse.AddDevice(dev);
					}
					break;
				case "Clock":
					SmartHouse.AddDevice(new Clock(name));
					break;
			}
			//Session["SmartHouse"] = SmartHouse;
			Session["showAddDevice"] = null;
			ParentForm.RefreshControls();
		}

		protected void BuildControlMarkup()
		{
			Assembly ass = Assembly.Load("SmartHouse");   //Да, я в курсе, как это звучит :-)
			Type[] types = ass.GetTypes();
			Type type = types.Where(item => item.Name == DevType).FirstOrDefault() as Type;
			FieldInfo fDevType = type.GetField("devType", BindingFlags.NonPublic | BindingFlags.Static);
			string devTypeName = fDevType.GetValue(null) as string;

			lblDevType.Text = devTypeName;
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
			tb.ID = idName;
			tb.Attributes["placeholder"] = "Ввести имя";
			tb.EnableViewState = true;
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
			tb.ID = idBrightMin;
			tb.TextMode = TextBoxMode.Number;
			tb.Attributes["placeholder"] = "0";
			tb.Attributes["min"] = "0";
			tb.Attributes["max"] = "200";
			tb.EnableViewState = true;
			td.Controls.Add(tb);

			tr.Cells.Add(td);

			destination.Rows.Add(tr);

			tr = new TableRow();

			td = new TableCell();
			td.Text = "Яркость max";
			tr.Cells.Add(td);

			td = new TableCell();
			tb = new TextBox();
			tb.ID = idBrightMax;
			tb.TextMode = TextBoxMode.Number;
			tb.Attributes["placeholder"] = "100";
			tb.Attributes["min"] = "0";
			tb.Attributes["max"] = "200";
			tb.EnableViewState = true;
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
			tb.ID = idTempMin;
			tb.TextMode = TextBoxMode.Number;
			tb.Attributes["placeholder"] = "-273";
			tb.Attributes["min"] = "-273";
			tb.EnableViewState = true;
			td.Controls.Add(tb);

			tr.Cells.Add(td);

			destination.Rows.Add(tr);

			tr = new TableRow();

			td = new TableCell();
			td.Text = "Температура max";
			tr.Cells.Add(td);

			td = new TableCell();
			tb = new TextBox();
			tb.ID = idTempMax;
			tb.TextMode = TextBoxMode.Number;
			tb.Attributes["placeholder"] = "10";
			tb.Attributes["min"] = "-273";
			tb.EnableViewState = true;
			td.Controls.Add(tb);

			tr.Cells.Add(td);

			destination.Rows.Add(tr);
		}
	}
}
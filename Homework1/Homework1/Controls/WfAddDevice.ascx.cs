//using HomeWorkSmartHouse.SmartHouseDir.Classes;
//using HomeWorkSmartHouse.SmartHouseDir.Classes.InternalParts;
//using HomeWorkSmartHouse.SmartHouseDir.Enums;
using Homework1.Classes;
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
		private const string idTempStep = "tbTemperatureStep";
		private const string idBrightMax = "tbBrightnessMax";
		private const string idBrightMin = "tbBrightnessMin";
		private const string idBrightStep = "tbBrightnessStep";
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
			ISmartHouseCreator shc = Manufacture.GetManufacture(Assembly.Load("SmartHouse"));

			string name;

			name = (FindControl(idName) as TextBox).Text.ToLower();

			ISmartDevice dev = shc.CreateDevice(DevType, name);

			//TODO: Вот где-то здесь какая-то верификация должна быть. Наверное.

			if (dev is IHaveThermostat)
			{
				int min, max, step;
				IHaveThermostat iterm = dev as IHaveThermostat;

				TextBox tbMin = FindControl(idTempMin) as TextBox;
				TextBox tbMax = FindControl(idTempMax) as TextBox;
				TextBox tbStep = FindControl(idTempStep) as TextBox;

				if (!int.TryParse(tbMax.Text, out max))
				{
					int.TryParse(tbMax.Attributes["placeholder"], out max);
				}
				if (!int.TryParse(tbMin.Text, out min))
				{
					int.TryParse(tbMin.Attributes["placeholder"], out min);
				}
				if (!int.TryParse(tbStep.Text, out step))
				{
					int.TryParse(tbStep.Attributes["placeholder"], out step);
				}

				iterm.TempMax = max;
				iterm.TempMin = min;
				iterm.TempStep = step;
				iterm.Temperature = max;
			}

			if (dev is IBrightable)
			{
				int min, max, step;
				IBrightable ibri = dev as IBrightable;

				TextBox tbMin = FindControl(idBrightMin) as TextBox;
				TextBox tbMax = FindControl(idBrightMax) as TextBox;
				TextBox tbStep = FindControl(idBrightStep) as TextBox;

				if (!int.TryParse(tbMax.Text, out max))
				{
					int.TryParse(tbMax.Attributes["placeholder"], out max);
				}
				if (!int.TryParse(tbMin.Text, out min))
				{
					int.TryParse(tbMin.Attributes["placeholder"], out min);
				}
				if (!int.TryParse(tbStep.Text, out step))
				{
					int.TryParse(tbStep.Attributes["placeholder"], out step);
				}

				ibri.BrightnessMax = max;
				ibri.BrightnessMin = min;
				ibri.BrightnessStep = step;
				ibri.Brightness = max;
			}

			SmartHouse.AddDevice(dev);

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

			frmAddDevice.Attributes["devtype"] = DevType;

			btnAddDevice.Text = "Добавить " + devTypeName;
			DisplayISmartDevice(pnlPropertiesTable);
			DisplayIcon();

			if (type.GetInterface("IBrightable") != null)
			{
				DisplayIBrightable(pnlPropertiesTable);
			}
			if (type.GetInterface("IHaveThermostat") != null)
			{
				DisplayIHaveThermostat(pnlPropertiesTable);
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

		protected void DisplayISmartDevice(Panel destination)
		{
			Label span;
			Panel div = new Panel();
			span = new Label();
			span.Text = "Имя устройства";
			div.Controls.Add(span);

			TextBox tb = new TextBox();
			tb.ID = idName;
			tb.Attributes["placeholder"] = "Ввести имя";
			div.Controls.Add(tb);

			destination.Controls.Add(div);
		}

		protected void DisplayIcon()
		{
			Panel icon = new Panel();
			icon.ID = "devIcon";
			icon.CssClass = "devIcon";
			icon.Attributes["devtype"] = DevType;
			phIcon.Controls.Add(icon);
		}

		protected void DisplayIBrightable(Panel destination)
		{
			Label span;
			TextBox tb;
			Panel div;

			//min яркости
			div = new Panel();

			span = new Label();
			span.Text = "Яркость min";
			div.Controls.Add(span);

			tb = new TextBox();
			tb.ID = idBrightMin;
			tb.TextMode = TextBoxMode.Number;
			tb.Attributes["placeholder"] = "0";
			tb.Attributes["min"] = "0";
			tb.Attributes["max"] = "200";
			div.Controls.Add(tb);

			destination.Controls.Add(div);

			//max яркости
			div = new Panel();

			span = new Label();
			span.Text = "Яркость max";
			div.Controls.Add(span);

			tb = new TextBox();
			tb.ID = idBrightMax;
			tb.TextMode = TextBoxMode.Number;
			tb.Attributes["placeholder"] = "100";
			tb.Attributes["min"] = "0";
			tb.Attributes["max"] = "200";
			div.Controls.Add(tb);

			destination.Controls.Add(div);

			//Шаг яркости
			div = new Panel();

			span = new Label();
			span.Text = "Шаг яркости";
			div.Controls.Add(span);

			tb = new TextBox();
			tb.ID = idBrightStep;
			tb.TextMode = TextBoxMode.Number;
			tb.Attributes["placeholder"] = "10";
			tb.Attributes["min"] = "0";
			tb.Attributes["max"] = "200";
			div.Controls.Add(tb);

			destination.Controls.Add(div);
		}

		protected void DisplayIHaveThermostat(Panel destination)
		{
			Label span;
			TextBox tb;
			Panel div;

			//min температуры
			div = new Panel();

			span = new Label();
			span.Text = "Температура min";
			div.Controls.Add(span);

			tb = new TextBox();
			tb.ID = idTempMin;
			tb.TextMode = TextBoxMode.Number;
			tb.Attributes["placeholder"] = "-273";
			tb.Attributes["min"] = "-273";
			div.Controls.Add(tb);

			destination.Controls.Add(div);

			//max температуры
			div = new Panel();

			span = new Label();
			span.Text = "Температура max";
			div.Controls.Add(span);

			tb = new TextBox();
			tb.ID = idTempMax;
			tb.TextMode = TextBoxMode.Number;
			tb.Attributes["placeholder"] = "10";
			tb.Attributes["min"] = "-273";
			div.Controls.Add(tb);

			destination.Controls.Add(div);

			//Шаг температуры
			div = new Panel();

			span = new Label();
			span.Text = "Шаг температуры";
			div.Controls.Add(span);

			tb = new TextBox();
			tb.ID = idTempStep;
			tb.TextMode = TextBoxMode.Number;
			tb.Attributes["placeholder"] = "10";
			tb.Attributes["min"] = "0";
			div.Controls.Add(tb);

			destination.Controls.Add(div);
		}
	}
}
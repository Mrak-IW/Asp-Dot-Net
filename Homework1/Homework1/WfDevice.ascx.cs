using HomeWork2.SmartHouseDir.Enums;
using HomeWork2.SmartHouseDir.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Homework1
{
	public partial class WfDevice : System.Web.UI.UserControl
	{
		public ISmartDevice Device { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{

			if (Device != null)
			{
				Button b;

				LblId.Text = Device.DeviceType + " \"" + Device.Name + "\"";

				b = btnChangeState;
				switch (Device.State)
				{
					case EPowerState.On:
						b.Text = "On";
						b.Attributes.Add("title", "выключить");
						b.Click += (senderCtrl, eargs) =>
						{
							Device.Off();
							//state.Text = Device.ToString().Replace(" ", "&nbsp;");
							Server.Transfer(Request.Path);
						};
						break;
					case EPowerState.Off:
						b.Text = "Off";
						b.Attributes.Add("title", "включить");
						b.Click += (senderCtrl, eargs) =>
						{
							Device.On();
							//state.Text = Device.ToString().Replace(" ", "&nbsp;");
							Server.Transfer(Request.Path);
						};
						break;
				}

				imgDevIcon.ImageUrl = "Images/lampIcon.png";

				DisplayIHaveThermostat(tblPropertiesTable);
				DisplayIBrightable(tblPropertiesTable);
			}
			else
			{
				Label err = new Label();
				err.Text = "Для данного элемента управления не задано конкретное устройство";
				PhControls.Controls.Add(err);
			}
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
					b.Click += (senderCtrl, eargs) =>
					{
						dev.IncreaseBrightness();
						Server.Transfer(Request.Path);
					};
					td.Controls.Add(b);

					b = new Button();
					b.Text = "-";
					b.ID = "btnBrightnessDec";
					b.Click += (senderCtrl, eargs) =>
					{
						dev.DecreaseBrightness();
						Server.Transfer(Request.Path);
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
					b.Click += (senderCtrl, eargs) =>
					{
						dev.IncreaseTemperature();
						Server.Transfer(Request.Path);
					};
					td.Controls.Add(b);

					b = new Button();
					b.Text = "-";
					b.ID = "btnTemperatureDec";
					b.Click += (senderCtrl, eargs) =>
					{
						dev.DecreaseTemperature();
						Server.Transfer(Request.Path);
					};
					td.Controls.Add(b);
				}
				tr.Cells.Add(td);

				destination.Rows.Add(tr);
			}
			else
			{ }
		}
	}
}
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
			Button b;
			if (Device != null)
			{
				LblId.Text = Device.Name;
				Label state = new Label();
				state.Text = Device.ToString().Replace(" ", "&nbsp;");
				PhControls.Controls.Add(state);

				b = null;
				switch (Device.State)
				{
					case EPowerState.On:
						b = new Button();
						b.ID = "btnOff";
						b.Text = "Выключить";
						b.Click += (senderCtrl, eargs) =>
						{
							Device.Off();
							state.Text = Device.ToString().Replace(" ", "&nbsp;");
							Server.Transfer(Request.Path);
						};
						break;
					case EPowerState.Off:
						b = new Button();
						b.ID = "btnOn";
						b.Text = "Включить";
						b.Click += (senderCtrl, eargs) =>
						{
							Device.On();
							state.Text = Device.ToString().Replace(" ", "&nbsp;");
							Server.Transfer(Request.Path);
						};
						break;
					case EPowerState.Broken:
						if (Device is IRepareable)
						{
							b = new Button();
							b.ID = "btnRepare";
							b.Text = "Починить";
							b.Click += (senderCtrl, eargs) =>
							{
								(Device as IRepareable).Repare();
								state.Text = Device.ToString().Replace(" ", "&nbsp;");
								Server.Transfer(Request.Path);
							};
						}
						break;
				}
				if (b != null)
				{
					PhControls.Controls.Add(b);
				}

				if (Device is IHaveThermostat)
				{
					b = new Button();
					b.ID = "btnIncresaeTemp";
					b.Text = "Теплее";
					b.Click += (senderCtrl, eargs) =>
					{
						(Device as IHaveThermostat).IncreaseTemperature();
						state.Text = Device.ToString().Replace(" ", "&nbsp;");
					};
					PhControls.Controls.Add(b);

					b = new Button();
					b.ID = "btnDecresaeTemp";
					b.Text = "Холоднее";
					b.Click += (senderCtrl, eargs) =>
					{
						(Device as IHaveThermostat).DecreaseTemperature();
						state.Text = Device.ToString().Replace(" ", "&nbsp;");
					};
					PhControls.Controls.Add(b);
				}

				if (Device is IBrightable)
				{
					b = new Button();
					b.ID = "btnIncresaeLight";
					b.Text = "Светлее";
					b.Click += (senderCtrl, eargs) =>
					{
						(Device as IBrightable).IncreaseBrightness();
						state.Text = Device.ToString().Replace(" ", "&nbsp;");
					};
					PhControls.Controls.Add(b);

					b = new Button();
					b.ID = "btnDecresaeLight";
					b.Text = "Темнее";
					b.Click += (senderCtrl, eargs) =>
					{
						(Device as IBrightable).DecreaseBrightness();
						state.Text = Device.ToString().Replace(" ", "&nbsp;");
					};
					PhControls.Controls.Add(b);
				}
			}
			else
			{
				Label err = new Label();
				err.Text = "Для данного элемента управления не задано конкретное устройство";
				PhControls.Controls.Add(err);
			}
		}
	}
}
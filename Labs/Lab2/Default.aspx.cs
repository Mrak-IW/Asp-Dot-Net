using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab2
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (IsPostBack)
			{
				BtnViewState.Click += BtnViewState_OnClick;
				BtnSession.Click += BtnSession_OnClick;
				BtnSolve.Click += BtnSolve_OnClick;
			}
			else
			{
				Session["number"] = int.Parse(BtnSession.Text);
			}
		}

		protected void BtnViewState_OnClick(object sender, EventArgs e)
		{
			int value = int.Parse((sender as Button).Text);
			(sender as Button).Text = (value + 1).ToString();
		}

		protected void BtnSession_OnClick(object sender, EventArgs e)
		{
			int value = (int)Session["number"];
			(sender as Button).Text = (++value).ToString();
			Session["number"] = value;
		}

		protected void BtnSolve_OnClick(object sender, EventArgs e)
		{
			double a, b, c, d;
			a = double.Parse(TbA.Text);
			b = double.Parse(TbB.Text);
			c = double.Parse(TbC.Text);
			ComplexNumber[] x = new ComplexNumber[2];
			d = b * b - 4 * a * c;

			LblAnswer.Text = string.Format("D = {0}<br />", d);
			if (d < 0)
			{
				x[0] = (new ComplexNumber(0, 1) * Math.Sqrt(-d) - b) / (2 * a);
				x[1] = (new ComplexNumber(0, 1) * -Math.Sqrt(-d) - b) / (2 * a);
			}
			else if (d == 0)
			{
				x[0] = new ComplexNumber(-b / (2 * a), 0);
			}
			else
			{
				x[0] = new ComplexNumber((Math.Sqrt(d) - b) / (2 * a), 0);
				x[1] = new ComplexNumber(-(Math.Sqrt(d) - b) / (2 * a), 0);
			}

			for (int i = 0; i < x.Length; i++)
			{
				if (x[i] != null)
				{
					LblAnswer.Text += string.Format("x[{0}] = {1}<br />", i, x[i]);
				}
			}
		}

		protected void Button1_Click(object sender, EventArgs e)
		{
			Session.Abandon();
		}
	}
}
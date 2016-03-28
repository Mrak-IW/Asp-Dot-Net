using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab3
{
	public partial class SexChanger : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (this.Controls.Count == 0)
			{
				rb_male = new RadioButton();
				rb_female = new RadioButton();
				BtnConfirm = new Button();
				LblResult = new Label();

				rb_female.Text = "Ж";
				rb_male.Text = "М";
				rb_female.GroupName = "rb_sex";
				rb_male.GroupName = "rb_sex";
				BtnConfirm.Text = "Сменить пол";

				Controls.Add(rb_male);
				//Controls.Add(new )
				Controls.Add(rb_female);
				Controls.Add(BtnConfirm);
				Controls.Add(LblResult);
			}

			if (IsPostBack)
			{
				BtnConfirm.Click += BtnConfirm_OnClick;
			}
		}

		protected void BtnConfirm_OnClick(object sender, EventArgs e)
		{
			if (rb_female.Checked)
			{
				LblResult.Text = "Вы выбрали женский пол. Мы понимаем и уважаем ваш выбор.";
			}
			else if (rb_male.Checked)
			{
				LblResult.Text = "Вы выбрали мужской пол. Мы понимаем и уважаем ваш выбор.";
			}
			else
			{
				LblResult.Text = "Вы не выбрали пол. Определяйтесь быстрее, а то так и будете без пола ходить.";
			}
		}

		//protected override void Render(HtmlTextWriter writer)
		//{
		//	foreach (Control c in this.Controls)
		//	{
		//		c.RenderControl(writer);
		//	}
		//}
	}
}
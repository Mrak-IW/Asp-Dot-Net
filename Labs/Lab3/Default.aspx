<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Lab3.Default" %>

<%@ Register Src="~/SexChanger.ascx" TagName="sc" TagPrefix="my" %>

<%@ Register Assembly="Lab3" Namespace="Lab3" TagPrefix="mysc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<title></title>
</head>
<body>
	<form id="form1" runat="server">
		<div>
			Task1<br />
			<my:sc runat="server" />
			<hr />

			Task2
			<br />
			<mysc:CustomList runat="server" />
			<s>А вот хрен там! Не регистрируется контрол без aspx-разметки.</s> Кстати, контрол с aspx-разметкой ("пользовательский" элемент управления)
			принципиально ничем не отличается от "специального серверного", кроме отсутствия этой самой разметки, и прекрасно регистрируется
			обоими приведёнными способами.
			<br />
			<s>UPD: А ещё, похоже, у "пользовательских" контролов, отсутствует реализация метода Render.</s>
			<br />
			UPD2: При создании "пользовательского" элемента методом добавления динамических контролов, у него не инициализируется внутренняя структура.
			Приходится инициализировать её вручную, к примеру в методе Page_Load.
			<br />
			UPD3: Есть нихреновая разница, добавлять-ли пользовательский элемент, зарегистрированный с атрибутом Src="*.ascx" или без него.
			<br />
			UPD4: Регистрируется. Просто через то место, из которого обычно произрастают руки разработчиков. Необходимо, не обращая внимание на ругань
			VisualStudio, вписать новый тег в разметку и запустить сборку и выполнение. Если всё правильно написано, результат будет в браузере.
			Ругань студии-же может и не прекратиться.
			<hr />

			Task3<br />
			<my:sc runat="server" ID="task3" />
			<%
				Label lblres = task3.FindControl("LblResult") as Label;
				if (lblres.Text != "")
				{
					Response.Write("<br/ >Выведено динамически:&nbsp;" + lblres.Text);
				}
			%>
			<%= GetHtmlList("это", "список", "любой", "длины", "которая", "будет", "нужна") %>
			<hr />

			Task4<br />
			<asp:PlaceHolder ID="PhTask4" runat="server"></asp:PlaceHolder>
			<hr />
		</div>
	</form>
</body>
</html>

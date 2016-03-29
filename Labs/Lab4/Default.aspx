<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Lab4.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<title></title>
</head>
<body>
	<form id="form1" runat="server">
		<div>
			Task1. Служебные объекты.
		<br />
			<%
				StringBuilder html = new StringBuilder();
				html.AppendFormat("IP:&nbsp;\"{0}\"<br />", Request.UserHostAddress);
				html.AppendFormat("Метод HTTP:&nbsp;\"{0}\"<br />", Request.HttpMethod);

				html.AppendFormat("Заголовки HTTP:<br /><ul>");
				foreach (string k in Request.Headers.AllKeys)
				{
					html.AppendFormat("<li>{0}:&nbsp;\"{1}\"</li>", k, Request.Headers[k]);
				}
				html.AppendFormat("</ul>");

				html.AppendFormat("Имя сервера:&nbsp;\"{0}\"<br />", Server.MachineName);
				html.AppendFormat("Путь к странице:&nbsp;\"{0}\"<br />", Request.PhysicalPath);

				Response.Write(html);
			%>
			<hr />

			Task2. Конфигурационный файл.
		<br />
			<%
				html = new StringBuilder();
				html.AppendFormat("stringParam:&nbsp;\"{0}\"<br />", myConfigSection.stringParam);
				html.AppendFormat("intParam:&nbsp;\"{0}\"<br />", myConfigSection.intParam);
				html.AppendFormat("doubleParam:&nbsp;\"{0}\"<br />", myConfigSection.doubleParam);

				Response.Write(html);
			%>
			<hr />

			Task3. Служебные объекты - перенаправления.
		<br />
			<asp:Button ID="btnRedirect" runat="server" Text="Response.Redirect" />
			<br />
			<asp:Button ID="btnTransfer" runat="server" Text="ServerTransfer" />
			<hr />
		</div>
	</form>
</body>
</html>

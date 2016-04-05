<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Homework1.Default" %>

<%@ Register Src="~/WfDevice.ascx" TagName="dev" TagPrefix="sh" %>
<%@ Register Src="~/WfAddDevice.ascx" TagName="devAdd" TagPrefix="sh" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<title></title>
	<link rel="stylesheet" href="Styles/style.css" />
</head>
<body>
	<form id="form1" runat="server">
		<div>
			<div class="header">
				<asp:Label ID="lblDeviceCount" runat="server" Text="lblDeviceCount"></asp:Label>
				<asp:DropDownList ID="ddlDeviceType" runat="server">
					<asp:ListItem Value="Fridge">Холодильник</asp:ListItem>
					<asp:ListItem Value="SmartLamp">Лампа</asp:ListItem>
					<asp:ListItem Value="Clock">Часы</asp:ListItem>
				</asp:DropDownList>
				<asp:Button ID="btnAddDevice" runat="server" Text="Добавить устройство" />
			</div>
			<asp:PlaceHolder ID="PhDevices" runat="server"></asp:PlaceHolder>
		</div>
	</form>
</body>
</html>

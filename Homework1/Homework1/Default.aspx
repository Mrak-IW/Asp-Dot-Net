<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Homework1.Default" %>

<%@ Register Src="~/Controls/WfDevice.ascx" TagName="dev" TagPrefix="sh" %>
<%@ Register Src="~/Controls/WfAddDevice.ascx" TagName="devAdd" TagPrefix="sh" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<title></title>
	<link rel="stylesheet" href="Styles/style.css" />
</head>
<body>
	<form id="form1" runat="server">
		<div class="header">
			<img id="logo" src="./Images/Logo.png" />
			<div id="headerControls">
				<asp:Label ID="lblDeviceCount" runat="server" Text="lblDeviceCount"></asp:Label><br />
				<asp:DropDownList ID="ddlDeviceType" runat="server">
					<asp:ListItem Value="Fridge">Холодильник</asp:ListItem>
					<asp:ListItem Value="SmartLamp">Лампа</asp:ListItem>
					<asp:ListItem Value="Clock">Часы</asp:ListItem>
				</asp:DropDownList><br />
				<div id="addDevice">
					<asp:ImageButton ID="btnAddDevice" runat="server" ImageUrl="Images/btnRoundDark.png" ToolTip="Добавить устройство" />
					Добавить устройство
				</div>
			</div>
		</div>
		<div class="devicePanel">
			<asp:PlaceHolder ID="PhDevices" runat="server"></asp:PlaceHolder>
		</div>
	</form>
</body>
</html>

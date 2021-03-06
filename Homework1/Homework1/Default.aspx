﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Homework1.Default" %>

<%@ Register Src="~/Controls/WfDevice.ascx" TagName="dev" TagPrefix="sh" %>
<%@ Register Src="~/Controls/WfAddDevice.ascx" TagName="devAdd" TagPrefix="sh" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<title></title>
	<link rel="stylesheet" href="Styles/style.css" />
	<link rel="stylesheet" href="Styles/devicons.css" />
</head>
<body>
	<form id="form1" runat="server">
		<div class="header">
			<img id="logo" src="./Images/Logo.png" />
			<div id="headerControls">
				<asp:Label ID="lblDeviceCount" runat="server" Text="lblDeviceCount"></asp:Label><br />
				<asp:DropDownList ID="ddlDeviceType" runat="server" EnableViewState="true">

				</asp:DropDownList><br />
				<asp:Button ID="btnAddDevice" CssClass="btnSwitch" runat="server" Text="Добавить устройство" ToolTip="Добавить устройство" />
			</div>
			<div class="headerBorder"></div>
		</div>
		<div class="devicePanel">
			<asp:PlaceHolder ID="PhDevices" runat="server"></asp:PlaceHolder>
		</div>
	</form>
</body>
</html>

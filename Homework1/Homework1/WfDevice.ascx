﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WfDevice.ascx.cs" Inherits="Homework1.WfDevice" %>
<asp:Panel ID="PnlMain" runat="server">
	<asp:CheckBox ID="ChbSelect" runat="server" />
	<asp:Label ID="LblId" runat="server" Text="ID"></asp:Label>
	<asp:PlaceHolder ID="PhControls" runat="server"></asp:PlaceHolder>
</asp:Panel>
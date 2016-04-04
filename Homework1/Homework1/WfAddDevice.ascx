<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WfAddDevice.ascx.cs" Inherits="Homework1.WfAddDevice" %>
<div class="frmAddDevice">

	<div class="topLine">
		<asp:DropDownList ID="ddlDeviceType" runat="server" AutoPostBack="true">
			<asp:ListItem Value="Fridge">Холодильник</asp:ListItem>
			<asp:ListItem Value="SmartLamp">Лампа</asp:ListItem>
			<asp:ListItem Value="Clock">Часы</asp:ListItem>
		</asp:DropDownList>
		<asp:Button ID="btnAddDevice" Text="Добавить" runat="server" />
	</div>
	<div class="devIcon">
		<asp:PlaceHolder ID="PhIcon" runat="server"></asp:PlaceHolder>
	</div>
	<asp:Table ID="tblPropertiesTable" class="propertiesTable" runat="server"></asp:Table>
</div>

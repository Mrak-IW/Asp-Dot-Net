<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WfDevice.ascx.cs" Inherits="Homework1.WfDevice" %>
<asp:Panel ID="PnlMain" CssClass="deviceBody" runat="server">
	<div class="topLine">
		<div class="lblID">
			<asp:Label ID="LblId" runat="server" Text="ID"></asp:Label>
		</div>
		<asp:Button ID="btnPowerState" CssClass="btnPowerState" runat="server" Text="ON/OFF" />
	</div>
	<div class="devIcon">
		<asp:PlaceHolder ID="PhIcon" runat="server"></asp:PlaceHolder>
	</div>
	<asp:Table ID="tblPropertiesTable" class="propertiesTable" runat="server"></asp:Table>
</asp:Panel>

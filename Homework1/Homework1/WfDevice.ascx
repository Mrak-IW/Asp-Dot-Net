<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WfDevice.ascx.cs" Inherits="Homework1.WfDevice" %>
<asp:Panel ID="PnlMain" CssClass="deviceBody" runat="server">
	<div class="btnRepare">
		REPARE
	</div>
	<div class="lblName">
		<asp:Label ID="LblId" runat="server" Text="ID"></asp:Label>
	</div>
	<div class="btnPowerState">
		<asp:Button ID="btnChangeState" runat="server" Text="ON/OFF" />
	</div>
	<div class="devIcon">
		<asp:Image ID="imgDevIcon" runat="server" />
	</div>
	<asp:Table ID="tblPropertiesTable" class="propertiesTable" runat="server"></asp:Table>
	
	<asp:PlaceHolder ID="PhControls" runat="server"></asp:PlaceHolder>

</asp:Panel>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WfDevice.ascx.cs" Inherits="Homework1.WfDevice" %>
<asp:Panel ID="PnlMain" CssClass="deviceBody" runat="server">
	<div class="topLine">
		<div class="lblID">
			<asp:Label ID="LblId" runat="server" Text="ID"></asp:Label>
		</div>
		<asp:ImageButton ID="btnPowerState" ImageUrl="~/Images/btnRoundDark.png" CssClass="btnPowerState" AlternateText="ON/OFF"  runat="server" />
	</div>
	<div class="devIconContainer">
		<asp:PlaceHolder ID="PhIcon" runat="server"></asp:PlaceHolder>
	</div>
	<asp:Panel ID="pnlProperties" CssClass="propertiesPanel" runat="server"></asp:Panel>
</asp:Panel>

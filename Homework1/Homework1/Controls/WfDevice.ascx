<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WfDevice.ascx.cs" Inherits="Homework1.WfDevice" %>
<div class="deviceContainer">
	<asp:Panel ID="PnlMain" CssClass="deviceBody" runat="server">
		<div class="topLine">
			<div class="lblID">
				<asp:Label ID="LblId" runat="server" Text="ID"></asp:Label>
			</div>
			<asp:Button ID="btnPowerState" CssClass="btnPowerState" ToolTip="ON/OFF" runat="server" />
		</div>
		<div class="devIconContainer">
			<asp:PlaceHolder ID="phIcon" runat="server"></asp:PlaceHolder>
		</div>
		<asp:Panel ID="pnlProperties" CssClass="propertiesPanel" runat="server"></asp:Panel>
	</asp:Panel>
	<asp:PlaceHolder ID="phSpacers" runat="server"></asp:PlaceHolder>
</div>

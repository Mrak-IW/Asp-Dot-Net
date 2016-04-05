<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WfAddDevice.ascx.cs" Inherits="Homework1.WfAddDevice" %>
<div class="frmAddDevice">

	<div class="topLine">
		<asp:Button ID="btnAddDevice" Text="Добавить" runat="server" />
	</div>
	<div class="devIcon">
		<asp:PlaceHolder ID="PhIcon" runat="server"></asp:PlaceHolder>
	</div>
	<asp:Table ID="tblPropertiesTable" CssClass="propertiesTable" runat="server"></asp:Table>
</div>

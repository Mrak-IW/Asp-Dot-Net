<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WfAddDevice.ascx.cs" Inherits="Homework1.WfAddDevice" %>
<div class="overlay">
	<table>
		<tr>
			<td>
				<div class="frmAddDevice" id="frmAddDevice" runat="server">
					<div class="topLine">
						<asp:Button ID="btnAddDevice" CssClass="btnSwitch" Text="Добавить устройство" runat="server" />
						<asp:Button ID="btnClose" CssClass="btnSwitch btnClose" Text="Закрыть" runat="server" />
					</div>
					<div class="devIconContainer">
						<asp:PlaceHolder ID="phIcon" runat="server"></asp:PlaceHolder>
					</div>
					<asp:Panel ID="pnlPropertiesTable" CssClass="propertiesPanel" runat="server"></asp:Panel>
				</div>
			</td>
		</tr>
	</table>
</div>

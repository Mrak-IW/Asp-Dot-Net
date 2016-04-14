<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WfAddDevice.ascx.cs" Inherits="Homework1.WfAddDevice" %>
<div class="overlay">
	<table>
		<tr>
			<td>
				<div class="frmAddDevice">
					<div class="topLine">
						<asp:Button ID="btnAddDevice" CssClass="btnSwitch" Text="" runat="server" />
						<label for="<%= btnAddDevice.UniqueID %>""><span>Добавить</span></label>
						<asp:Label ID="lblDevType" Text="&lt;тип&gt;" runat="server"></asp:Label>
						<asp:Button ID="btnClose" CssClass="btnSwitch btnClose" Text="" runat="server" />
						<label for="<%= btnAddDevice.UniqueID %>""><span class="lblClose">Закрыть</span></label>
					</div>
					<div class="devIcon">
						<asp:PlaceHolder ID="PhIcon" runat="server"></asp:PlaceHolder>
					</div>
					<asp:Table ID="tblPropertiesTable" CssClass="propertiesTable" runat="server"></asp:Table>
				</div>
			</td>
		</tr>
	</table>
</div>

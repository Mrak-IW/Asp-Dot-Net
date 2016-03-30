<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Homework1.Default" %>
<%@ Register Src="~/WfDevice.ascx" TagName="dev" TagPrefix="sh" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<asp:PlaceHolder ID="PhDevices" runat="server"></asp:PlaceHolder>
    </div>
    </form>
</body>
</html>

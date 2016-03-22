<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Lab2.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Task2
            <br />

            <asp:Button ID="BtnViewState" runat="server" ViewStateMode="Enabled" Text="0" />
            <asp:Button ID="BtnSession" runat="server" Text="0" />

            <hr />

            Task3
            <br />

            <asp:TextBox ID="TbA" runat="server">1</asp:TextBox>x^2 + 
            <asp:TextBox ID="TbB" runat="server">2</asp:TextBox>x + 
            <asp:TextBox ID="TbC" runat="server">1</asp:TextBox>
            = 0
            <br />
            <asp:Button ID="BtnSolve" runat="server" Text="Порешить" />
            <br />
            <asp:Label ID="LblAnswer" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Lab3.Default" %>

<%@ Register Src="~/SexChanger.ascx" TagName="sc" TagPrefix="my" %>

<%@ Register Assembly="Lab3" Namespace="Lab3" TagPrefix="mysc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Task1<br />
            <my:sc runat="server" />
            <hr />

            Task2<br />
            <hr />
        </div>
    </form>
</body>
</html>

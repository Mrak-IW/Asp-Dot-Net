<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SexChanger.ascx.cs" Inherits="Lab3.SexChanger" %>
<!--Sex/gender chooser actually, but, let it be 8-P -->
Пол: 
<asp:RadioButton ID="rb_male" GroupName="rb_sex" runat="server" Text="Ме"/>
<asp:RadioButton ID="rb_female" GroupName="rb_sex" runat="server" Text="Жо" />
<br />
<asp:Button ID="BtnConfirm" runat="server" Text="Выбрать" />
<br />
<asp:Label ID="LblResult" runat="server" Text=""></asp:Label>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AktienSimulator.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <p>
            Registrieren:</p>
        <p>
            Nickname:&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="textNickname" runat="server"></asp:TextBox>
        </p>
        <p>
            Passwort:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="textPassword" runat="server"></asp:TextBox>
        </p>
        <asp:Button ID="btnRegistrieren" runat="server" OnClick="btnRegistrieren_Click" Text="Registrieren" />
        <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" />
        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Speichern" />
    </form>
</body>
</html>

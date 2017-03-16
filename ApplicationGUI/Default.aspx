<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AktienSimulator.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td>Nickname:
                </td>
                <td>
                    <asp:TextBox ID="textNickname" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Passwort:
                </td>
                <td>
                    <asp:TextBox ID="textPassword" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnRegistrieren" runat="server" OnClick="btnRegistrieren_Click" Text="Registrieren" />
                </td>
                <td>
                    <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
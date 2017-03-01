<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AktienSimulator.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="Panel1" runat="server" Direction="RightToLeft" Height="56px" style="margin-top: 0px">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Speichern" />
            <br />
            <asp:Button ID="btnTest" runat="server" OnClick="btnTest_Click" Text="Test" Width="87px" />
        </asp:Panel>
        <p>
            Account:
            <asp:Label ID="lblAccount" runat="server" OnDataBinding="lblAccount_DataBinding"></asp:Label>
        </p>
        <p>
            Nickname:&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="textNickname" runat="server"></asp:TextBox>
        </p>
        <p aria-atomic="False">
            Passwort:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="textPassword" runat="server"></asp:TextBox>
        </p>
        <asp:Button ID="btnRegistrieren" runat="server" OnClick="btnRegistrieren_Click" Text="Registrieren" />
        <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" />
        <br />
        <br />
        Kredit aufnehmen:<br />
        <asp:TextBox ID="textKreditHöhe" runat="server"></asp:TextBox>
        <asp:Button ID="btnKreditAufnehmen" runat="server" OnClick="btnKreditAufnehmen_Click" Text="Kredit aufnehmen" />
        <br />
        <br />
        Ihr Depot:<br />
        <br />
        Anzahl:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="textAnzahl" runat="server">1</asp:TextBox>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" OnDataBinding="GridView1_DataBinding" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="Bezeichnung" HeaderText="Aktie" ReadOnly="True" SortExpression="Aktie" />
                        <asp:BoundField DataField="Kurs" DataFormatString="{0:C2}" HeaderText="Kurs" ReadOnly="True" SortExpression="Kurs" />
                        <asp:TemplateField HeaderText="Event">
                            <ItemTemplate>
                                <asp:Literal ID="litEvent" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Anzahl">
                            <ItemTemplate>
                                <asp:Literal ID="litAnzahl" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:ButtonField ButtonType="Button" CommandName="Kaufen" Text="Kaufen" />
                        <asp:ButtonField ButtonType="Button" CommandName="Verkaufen" Text="Verkaufen" />
                    </Columns>
                </asp:GridView>
                <br />
                Ihre Bilanz:
                <asp:Label ID="lblBilanz" runat="server" OnDataBinding="lblBilanz_DataBinding"></asp:Label>
                <asp:Timer ID="Timer1" runat="server" Interval="1000" OnTick="UpdateEvents">
                </asp:Timer>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
    </form>
</body>
</html>

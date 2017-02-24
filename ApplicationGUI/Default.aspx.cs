using ApplicationLogic;
using Contracts;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AktienSimulator
{
    public partial class Default : GenericPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrieren_Click(object sender, EventArgs e)
        {
            var errorcode = LogicAccount.RegisterAccount(textNickname.Text, textPassword.Text);
            switch (errorcode)
            {
                case ErrorCodes.Register.NoError:
                    Response.Write("<script>alert('Erfolgreich registriert.');</script>");
                    break;
                case ErrorCodes.Register.NameAlreadyTaken:
                    Response.Write("<script>alert('Benutzername ist schon vergeben.');</script>");
                    break;
                default:
                    break;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Database.SaveDatabase();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            ErrorCodes.Login errorcode = ErrorCodes.Login.NoError;
            Account = LogicAccount.LogIn(textNickname.Text, textPassword.Text, ref errorcode);
            switch (errorcode)
            {
                case ErrorCodes.Login.NoError:
                    Response.Write("<script>alert('Erfolgreich angemeldet.');</script>");
                    break;
                case ErrorCodes.Login.NicknameNotFound:
                    Response.Write("<script>alert('Es wurde kein Account unter dem angegeben Nicknamen gefunden.');</script>");
                    break;
                case ErrorCodes.Login.WrongPassword:
                    Response.Write("<script>alert('Falsches Passwort.');</script>");
                    break;
                default:
                    break;
            }

            lblAccount.DataBind();
        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
            Response.Write("<script>alert('" + Account + "');</script>");
        }

        protected void lblAccount_DataBinding(object sender, EventArgs e)
        {
            lblAccount.Text = Account.Nickname;        
        }

        protected void GridView1_DataBinding(object sender, EventArgs e)
        {
            GridView1.DataSource = Depots;
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int anzahl = Convert.ToInt32(textAnzahl.Text);
            var depot = Depots.ElementAt(index);

            if (e.CommandName == "Kaufen")
            {
                var errorcode = LogicAktie.BuyAktie(Account, Depots, depot.AktieRow, anzahl);
                switch (errorcode)
                {
                    case ErrorCodes.BuyAktie.NotEnoughMoney:
                        Response.Write("<script>alert('Sie haben nicht genügend Geld zur Verfügung!');</script>");
                        break;
                    default:
                        break;
                }
            }
            else if (e.CommandName == "Verkaufen")
            {
                var errorcode = LogicAktie.SellAktie(Account, depot, anzahl);
                switch (errorcode)
                {
                    case ErrorCodes.SellAktie.NotEnoughAmount:
                        Response.Write("<script>alert('Sie besitzen nicht die gewünschte Menge zum Verkaufen!');</script>");
                        break;
                    default:
                        break;
                }
            }

            GridView1.DataBind();
            lblBilanz.DataBind();
        }

        protected void lblBilanz_DataBinding(object sender, EventArgs e)
        {
            lblBilanz.Text = Account.Bilanz.ToString("0,0.00");
        }
    }
}
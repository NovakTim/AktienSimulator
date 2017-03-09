using ApplicationLogic;
using Contracts;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
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
            lblBilanz.DataBind();
        }

        protected void UpdateEvents(object sender, EventArgs e)
        {
            var aktien = Database.DataSet.Aktie.ToList();
            LogicEvent.UpdateChangeEvent(aktien);
            LogicEvent.UpdateKurswert(aktien);

            if(Account != null)
            {
                var depots = LogicDepot.GetDepots(Account.Nickname);
                LogicDividende.UpdateDividende(Account, depots);
            }

            GridView1.DataBind();
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
            var account = LogicAccount.LogIn(textNickname.Text, textPassword.Text, ref errorcode);
            switch (errorcode)
            {
                case ErrorCodes.Login.NoError:
                    Response.Write("<script>alert('Erfolgreich angemeldet.');</script>");
                    Account = account;
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
            GridView1.DataBind();
        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
            Response.Write("<script>alert('" + Account + "');</script>");
        }

        protected void lblAccount_DataBinding(object sender, EventArgs e)
        {
            lblAccount.Text = Account?.Nickname;        
        }

        protected void GridView1_DataBinding(object sender, EventArgs e)
        {
            GridView1.DataSource = Database.DataSet.Aktie.ToList();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int anzahl = Convert.ToInt32(textAnzahl.Text);
            var aktie = Database.DataSet.Aktie.ElementAt(index);

            if (e.CommandName == "Kaufen")
            {
                bool newDepotCreated = false;
                var errorcode = LogicAktie.BuyAktie(Account, Depots, aktie.ID, anzahl, ref newDepotCreated);
                switch (errorcode)
                {
                    case ErrorCodes.BuyAktie.NotEnoughMoney:
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Sie haben nicht genügend Geld zur Verfügung!');", true);
                        break;
                    default:
                        if (newDepotCreated)
                            UpdateDepots();
                        break;
                }
            }
            else if (e.CommandName == "Verkaufen")
            {
                bool newDepotCreated = false;
                var errorcode = LogicAktie.SellAktie(Account, Depots, aktie.ID, anzahl, ref newDepotCreated);
                switch (errorcode)
                {
                    case ErrorCodes.SellAktie.NotEnoughAmount:
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Sie besitzen nicht die gewünschte Menge zum Verkaufen!');", true);
                        break;
                    default:
                        if (newDepotCreated)
                            UpdateDepots();
                        break;
                }
            }

            GridView1.DataBind();
            lblBilanz.DataBind();
        }

        protected void lblBilanz_DataBinding(object sender, EventArgs e)
        {
            lblBilanz.Text = Account?.Bilanz.ToString("0,0.00");
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal litEvent = e.Row.FindControl("litEvent") as Literal;
                Literal litAnzahl = e.Row.FindControl("litAnzahl") as Literal;
                AktienSimulatorDataSet.AktieRow aktie = e.Row.DataItem as AktienSimulatorDataSet.AktieRow;
                litEvent.Text = aktie.EventRow.Bezeichnung;
                var depot = Depots?.FirstOrDefault(x => x.Aktie == aktie.ID);
                if (depot != null)
                    litAnzahl.Text = depot.Anzahl.ToString();
                else
                    litAnzahl.Text = "0";
            }
        }

        protected void btnKreditAufnehmen_Click(object sender, EventArgs e)
        {
            LogicKredit.KreditAufnehmen(Account, Convert.ToDecimal(textKreditHöhe.Text));
        }
    }
}
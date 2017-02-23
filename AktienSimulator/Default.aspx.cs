using ApplicationLogic;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AktienSimulator
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrieren_Click(object sender, EventArgs e)
        {
            var errorcode = LogicAccount.RegisterAccount(textNickname.Text, textPassword.Text);
            switch (errorcode)
            {
                case Contracts.ErrorCodes.Register.NoError:
                    Response.Write("<script>alert('Erfolgreich registriert.');</script>");
                    break;
                case Contracts.ErrorCodes.Register.NameAlreadyTaken:
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
            var errorcode = LogicAccount.Login(textNickname.Text, textPassword.Text);
            switch (errorcode)
            {
                case Contracts.ErrorCodes.Login.NoError:
                    Response.Write("<script>alert('Erfolgreich angemeldet.');</script>");
                    break;
                case Contracts.ErrorCodes.Login.NicknameNotFound:
                    Response.Write("<script>alert('Es wurde kein Account unter dem angegeben Nicknamen gefunden.');</script>");
                    break;
                case Contracts.ErrorCodes.Login.WrongPassword:
                    Response.Write("<script>alert('Falsches Passwort.');</script>");
                    break;
                default:
                    break;
            }
        }
    }
}
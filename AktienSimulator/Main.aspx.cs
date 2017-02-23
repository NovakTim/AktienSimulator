using ApplicationLogic;
using Model;
using Model.AktienSimulatorDataSetTableAdapters;
using System;
using System.Web.UI.WebControls;

namespace AktienSimulator
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrieren_Click(object sender, EventArgs e)
        {
            LogicApplication.RegisterAccount(textNickname.Text, textPassword.Text);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Database.SaveDatabase();
        }
    }
}
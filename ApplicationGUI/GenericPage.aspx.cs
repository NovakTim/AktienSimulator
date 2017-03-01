using ApplicationLogic;
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
    public partial class GenericPage : System.Web.UI.Page
    {
        public AktienSimulatorDataSet.AccountRow Account
        {
            get
            {
                return Session["Account"] as AktienSimulatorDataSet.AccountRow;
            }
            set
            {
                Session["Account"] = value;
                Depots = LogicDepot.GetDepots(value.Nickname);
            }
        }

        public List<AktienSimulatorDataSet.DepotRow> Depots
        {
            get
            {
                return Session["Depots"] as List<AktienSimulatorDataSet.DepotRow>;
            }
            set
            {
                Session["Depots"] = value;
            }
        }

        protected virtual void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
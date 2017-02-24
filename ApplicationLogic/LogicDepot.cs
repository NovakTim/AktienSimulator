using Model;
using Model.AktienSimulatorDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic
{
    public static class LogicDepot
    {
        public static EnumerableRowCollection<AktienSimulatorDataSet.DepotRow> GetDepots(string nickname)
        {
            return Database.DataSet.Depot.Where(x => x.Account == nickname);
        }
    }
}

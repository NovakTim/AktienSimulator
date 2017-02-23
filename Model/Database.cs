using Model.AktienSimulatorDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public static class Database
    {
        public static AktienSimulatorDataSet DataSet { get; set; }
        public static TableAdapterManager TableAdapterManager { get; set; }

        public static void Initialize()
        {
            DataSet = new AktienSimulatorDataSet();

            TableAdapterManager = new TableAdapterManager();
            TableAdapterManager.AccountTableAdapter = new AccountTableAdapter();
            TableAdapterManager.AccountKreditTableAdapter = new AccountKreditTableAdapter();
            TableAdapterManager.AktieTableAdapter = new AktieTableAdapter();
            TableAdapterManager.DepotTableAdapter = new DepotTableAdapter();
            TableAdapterManager.EventTableAdapter = new EventTableAdapter();
            TableAdapterManager.KreditTableAdapter = new KreditTableAdapter();
        }

        public static void CacheDataSetFull()
        {
            TableAdapterManager.AccountTableAdapter.Fill(DataSet.Account);
            TableAdapterManager.AccountKreditTableAdapter.Fill(DataSet.AccountKredit);
            TableAdapterManager.AktieTableAdapter.Fill(DataSet.Aktie);
            TableAdapterManager.DepotTableAdapter.Fill(DataSet.Depot);
            TableAdapterManager.EventTableAdapter.Fill(DataSet.Event);
            TableAdapterManager.KreditTableAdapter.Fill(DataSet.Kredit);
        }

        public static void SaveDatabase()
        {
            TableAdapterManager.UpdateAll(DataSet);
        }
    }
}

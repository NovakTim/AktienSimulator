using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ApplicationLogic
{
    public static class LogicKredit
    {
        public static void KreditAufnehmen(AktienSimulatorDataSet.AccountRow account, decimal amount)
        {
            var row = Database.DataSet.Kredit.NewKreditRow();
            row.Account = account.Nickname;
            row.Höhe = amount;
            row.Rest = amount;
            Database.DataSet.Kredit.Rows.Add(row);
            account.Bilanz += amount;
        }
    }
}

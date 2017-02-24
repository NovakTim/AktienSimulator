using Contracts;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic
{
    public static class LogicAktie
    {
        public static ErrorCodes.BuyAktie BuyAktie(AktienSimulatorDataSet.AccountRow account, EnumerableRowCollection<AktienSimulatorDataSet.DepotRow> depots, AktienSimulatorDataSet.AktieRow aktie, int anzahl)
        {
            var depot = depots.FirstOrDefault(x => x.Aktie == aktie.ID);

            var sum = depot.AktieRow.Kurs * anzahl;
            if(account.Bilanz >= sum)
            {
                if (depot == null)
                {
                    var row = Database.DataSet.Depot.NewRow();
                    row["Account"] = account;
                    row["Aktie"] = aktie;
                    row["Anzahl"] = 0;
                    depot = row as AktienSimulatorDataSet.DepotRow;
                }

                depot.Anzahl += anzahl;
                account.Bilanz -= sum;
                return ErrorCodes.BuyAktie.NoError;
            }

            return ErrorCodes.BuyAktie.NotEnoughMoney;
        }

        public static ErrorCodes.SellAktie SellAktie(AktienSimulatorDataSet.AccountRow account, AktienSimulatorDataSet.DepotRow depot, int anzahl)
        {
            if(depot.Anzahl >= anzahl)
            {
                var sum = depot.AktieRow.Kurs * anzahl;
                account.Bilanz += sum;
                depot.Anzahl -= anzahl;

                return ErrorCodes.SellAktie.NoError;
            }

            return ErrorCodes.SellAktie.NotEnoughAmount;
        }
    }
}

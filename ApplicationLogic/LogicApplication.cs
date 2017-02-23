using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic
{
    public static class LogicApplication
    {
        public static void RegisterAccount(string nickname, string password)
        {
            var row = Database.DataSet.Tables["Account"].NewRow();
            row["Nickname"] = nickname;
            row["Passwort"] = password;
            Database.DataSet.Tables["Account"].Rows.Add(row);
        }
    }
}

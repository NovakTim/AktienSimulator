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
    public static class LogicAccount
    {
        public static ErrorCodes.Register RegisterAccount(string nickname, string password)
        {
            var row = Database.DataSet.Tables["Account"].NewRow();
            row["Nickname"] = nickname;
            row["Passwort"] = password;
            try
            {
                Database.DataSet.Tables["Account"].Rows.Add(row);
                return ErrorCodes.Register.NoError;
            }
            catch (ConstraintException e)
            {
                return ErrorCodes.Register.NameAlreadyTaken;
            }
        }

        public static ErrorCodes.Login Login(string nickname, string password)
        {
            var accounts = Database.DataSet.Tables["Account"];
            var result = from acc in accounts.AsEnumerable()
                         where acc.Field<string>("Nickname") == nickname
                         select acc;

            var account = result.FirstOrDefault();

            if (account == null)
                return ErrorCodes.Login.NicknameNotFound;

            if (account.Field<string>("Passwort") != password)
                return ErrorCodes.Login.WrongPassword;

            return ErrorCodes.Login.NoError;
        }
    }
}

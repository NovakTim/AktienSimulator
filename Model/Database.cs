﻿using Contracts;
using Model.AktienSimulatorDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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
            TableAdapterManager.AktieTableAdapter = new AktieTableAdapter();
            TableAdapterManager.DepotTableAdapter = new DepotTableAdapter();
            TableAdapterManager.EventTableAdapter = new EventTableAdapter();
            TableAdapterManager.KreditTableAdapter = new KreditTableAdapter();
        }

        public static void CacheRelevantTables()
        {
            TableAdapterManager.AktieTableAdapter.Fill(DataSet.Aktie);
            TableAdapterManager.EventTableAdapter.Fill(DataSet.Event);
            TableAdapterManager.DepotTableAdapter.Fill(DataSet.Depot);
            TableAdapterManager.KreditTableAdapter.Fill(DataSet.Kredit);
        }

        public static void SaveDatabase()
        {
            TableAdapterManager.DepotTableAdapter.Update(DataSet.Depot);
            TableAdapterManager.KreditTableAdapter.Update(DataSet.Kredit);
        }

        public static AktienSimulatorDataSet.AccountRow CheckLogIn(string nickname, string password, ref ErrorCodes.Login errorcode)
        {
            OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.AktienSimulatorConnectionString);
            connection.Open();

            //SQL Injection verhindern
            string queryString = "SELECT * FROM Account WHERE Nickname = @Nickname";
            OleDbCommand command = new OleDbCommand(queryString, connection);
            command.Parameters.Add("@Nickname", OleDbType.VarChar, 255);
            command.Parameters["@Nickname"].Value = nickname;

            var reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.HasRows)
            {
                reader.Read();
                AktienSimulatorDataSet.AccountRow row = DataSet.Account.NewAccountRow();
                row.Nickname = reader["Nickname"].ToString();
                row.Passwort = reader["Passwort"].ToString();
                row.Bilanz = Convert.ToDecimal(reader["Bilanz"]);

                if(row.Passwort == password)
                {
                    errorcode = ErrorCodes.Login.NoError;
                    connection.Close();
                    return row;
                }
                else
                {
                    errorcode = ErrorCodes.Login.WrongPassword;
                }
            }
            else
            {
                errorcode = ErrorCodes.Login.NicknameNotFound;
            }

            connection.Close();
            return null;
        }

        public static void FillDepots(string nickname)
        {
            //SQL Injection verhindern
            string queryString = "SELECT * FROM Depot WHERE Account = @Account";
            TableAdapterManager.DepotTableAdapter.Adapter.SelectCommand = new OleDbCommand(queryString, TableAdapterManager.DepotTableAdapter.Connection);
            TableAdapterManager.DepotTableAdapter.Adapter.SelectCommand.Parameters.Add("@Account", OleDbType.VarChar, 255);
            TableAdapterManager.DepotTableAdapter.Adapter.SelectCommand.Parameters["@Account"].Value = nickname;

            TableAdapterManager.DepotTableAdapter.Adapter.Fill(DataSet.Depot);
        }
    }
}

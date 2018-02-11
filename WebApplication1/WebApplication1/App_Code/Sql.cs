using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public static class Sql
    {
        public static string ConnectionString = "Data Source = VOJTAPC\\VSQL;" + "Initial Catalog = CORE;" + "User ID = CERIS;" + "Password = Alpiq123456";
        public static SqlConnection sConn = new SqlConnection(ConnectionString);
        public static string query = "SELECT * FROM [CORE].[dbo].[V_BATCH_SCHEDULER]";
        public static SqlCommand sComm = new SqlCommand(query, sConn);

        public static void SetCommand(string query)
        {
            Sql.query = query;
            Sql.sComm = new SqlCommand(Sql.query, Sql.sConn);
        }

        public static void Open()
        {
            sConn.Close();
            sConn.Open();
        }

        public static void Close()
        {
            sConn.Close();
        }
    }
}
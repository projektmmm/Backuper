﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace WebApplication1
{
    public static class Sql
    {
        public static string ConnectionString = "Server=mysqlstudenti.litv.sssvt.cz; Database=3b2_macekdaniel_db2; Uid=macekdaniel; Pwd=123456";
        public static MySqlConnection sConn = new MySqlConnection(ConnectionString);
        public static string query = "";
        public static MySqlCommand sComm = new MySqlCommand(query, sConn);

        public static void SetCommand(string quer)
        {
            query = quer;
            sComm = new MySqlCommand(query, sConn);
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

        public static MySqlConnection GetConnection()
        {
            sConn = new MySqlConnection(ConnectionString);
            return sConn;
        }
    }
}
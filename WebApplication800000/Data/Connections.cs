using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace WebApplication800000.Data
{
    class Connection
    {
        public static MySqlConnection connection;
        static string server;
        static string database;
        static string uid;
        static string password;

        public static MySqlConnection Initialize()
        {
            server = "145.24.222.102";
            database = "sys";
            uid = "root";
            password = "Project56";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "Port = 3306;" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
            return connection;
        }
    }
}
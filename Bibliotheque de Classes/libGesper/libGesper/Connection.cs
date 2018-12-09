using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace libGesper
{
    static class Connection
    {
        static private MySqlConnection cnx;
        static private string sConnection;

        static Connection()
        {
            Connection.sConnection = "HOST=localhost; DATABASE=gesper; USER=root; PASSWORD=siojjr";
            Connection.cnx = new MySqlConnection(sConnection);
        }

        static public MySqlConnection GetConnection()
        {
            return Connection.cnx;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace tools
{
    class DB
    {
        MySqlConnection _connection;
        MySqlCommand _cmd;

        string _command;

        Dictionary<string,string> table = new Dictionary<string, string>();
        public DB(MySqlConnection connection, string dbName, int create)
        {
            _connection = connection;

            switch (create)
            {
                case 0:
                    CreateDB(dbName);
                    break;

                case 1:
                    DeleteDB(dbName);
                    break;
            }
        }

        public DB(MySqlConnection connection, string dbName, int type, string tableName, Dictionary<string, string> table)
        {
            _connection = connection;
            _command = "USE " + dbName;
            _cmd = new MySqlCommand(_command, _connection);
            _cmd.ExecuteNonQuery();
            CreateTable(dbName,type, tableName,table);
        }

        private void CreateDB(string dbName)
        {
            _command = "CREATE DATABASE " + dbName;
            _cmd = new MySqlCommand(_command, _connection); 
            _cmd.ExecuteNonQuery();
        }

        private void DeleteDB(string dbName)
        {
            _command = "DROP DATABASE " + dbName;
            _cmd = new MySqlCommand(_command, _connection);
            _cmd.ExecuteNonQuery();
        }
        private void CreateTable(string dbName,int type, string tableName, Dictionary<string, string> table)
        {
            _command = "CREATE TABLE " + tableName + "(";

            foreach(var data in table.Keys)
            {
                MessageBox.Show(table[data]);
                _command += table[data];
                _command += ",";
            }
            _command += ")";
            _cmd = new MySqlCommand(_command, _connection);
            _cmd.ExecuteNonQuery();
        }
    }
}
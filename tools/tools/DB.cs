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
        //Variables---

        MySqlConnection _connection;
        MySqlCommand _cmd;

        string _command;




        public DB(MySqlConnection connection)
        {
            _connection = connection;
        }
        /// <summary>
        /// Constructeur qui regarde si l'utilisateur veut créer ou supprimer une base de donnée
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="dbName"></param>
        /// <param name="create"></param>
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

        /// <summary>
        /// Constructeur pour créer une table
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="dbName"></param>
        /// <param name="type"></param>
        /// <param name="tableName"></param>
        public DB(MySqlConnection connection, string dbName, int type, string tableName)
        {
            _connection = connection;
            _command = "USE " + dbName;
            _cmd = new MySqlCommand(_command, _connection);
            _cmd.ExecuteNonQuery();
            CreateTable(dbName);
        }

        /// <summary>
        ///Commande SQWL pour créer une base de donnée
        /// </summary>
        /// <param name="dbName"></param>
        private void CreateDB(string dbName)
        {
            _command = "CREATE DATABASE " + dbName;
            _cmd = new MySqlCommand(_command, _connection); 
            _cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Commande SQL pour supprimer une base de donnée
        /// </summary>
        /// <param name="dbName"></param>
        private void DeleteDB(string dbName)
        {
            _command = "DROP DATABASE " + dbName;
            _cmd = new MySqlCommand(_command, _connection);
            _cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Commande SQL pour créer une table dans une base de donnée
        /// </summary>
        /// <param name="tableName"></param>
        private void CreateTable(string tableName)
        {
            _command = "CREATE TABLE " + tableName + "(id int)";
            _cmd = new MySqlCommand(_command, _connection);
            _cmd.ExecuteNonQuery();
            MessageBox.Show("Table crée");
        }

        /// <summary>
        /// Méthode permettant de retourner toutes les tables
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public List<string> GetAllTable(string dbName)
        {
            _command = "SHOW TABLES FROM " + dbName + ";";

            List<string> liste = new List<string>();

            if(_connection.State == System.Data.ConnectionState.Open)
            {
                _cmd = new MySqlCommand(_command, _connection);
                MySqlDataReader command = _cmd.ExecuteReader();

                while (command.Read())
                {
                    liste.Add(command.GetString(0));

                }
                command.Close();
            }
            return liste;
        }

        /// <summary>
        /// Méthode permettant de retourner la liste de toutes les bases de données
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllDataBase()
        {
            _command = "SHOW DATABASES;";

            List<string> liste = new List<string>();
            
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                _cmd = new MySqlCommand(_command, _connection);
                MySqlDataReader command = _cmd.ExecuteReader();

                while (command.Read())
                {
                    liste.Add(command.GetString(0));
                }
                command.Close();
            }

            return liste;
        }

    }
}
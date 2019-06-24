using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Femalab.Mysql
{
    public class ApiFacturaloConector
    {
        public bool DeleteDocument(int id)
        {

            int idDocument= 0;
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=apifacturalo;";
            
            string query = "delete FROM documents WHERE id ="+ id;
            
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;
            try
            {

                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                idDocument = 1;

                databaseConnection.Close();
            }
            catch (Exception ex)
            {
            }

            return (idDocument!=0);
        }
        public bool ExisteDocument(string Serie,string Number)
        {

            int idDocument = 0;
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=apifacturalo;";
            string query = "SELECT id FROM documents WHERE document_type_id = '"+ Serie + "' AND NUMBER=" + Number;
            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        idDocument = reader.GetInt32(0);
                        break;
                    }
                }
                else
                {
                }
                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                return false;
            }

            return (idDocument != 0);
        }
    }
}
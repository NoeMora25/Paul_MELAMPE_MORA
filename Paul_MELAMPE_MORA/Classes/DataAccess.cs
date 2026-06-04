using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;

namespace Paul_MELAMPE_MORA.Classes
{
    public class DataAccess
    {
        private static string connectionString;
        private static NpgsqlConnection connection;


        public static NpgsqlConnection GetConnection()
        {
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;

            string currentConnectionString = $"Host=srv-peda-new;Port=5433;Username={mainWindow.loginuser};Password={mainWindow.mdp_user};Database=S201_MORA_MELAMPE;Options='-c search_path=melampel'";

            if (connection == null || connectionString != currentConnectionString)
            {
                connectionString = currentConnectionString;
                connection = new NpgsqlConnection(connectionString);
            }

            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    LogError.Log(ex, "Pb à la connexion  \n");

                    // SÉCURITÉ : On vide le cache des connexions de Npgsql pour éviter 
                    // que la base de données ne garde l'erreur en mémoire
                    NpgsqlConnection.ClearAllPools();

                    throw; // On renvoie l'erreur pour que l'UClogin l'affiche en rouge
                }
            }
            return connection;
        }

        public static DataTable ExecuteSelect(NpgsqlCommand cmd)
        {
            DataTable dataTable = new DataTable();
            try
            {
                cmd.Connection = GetConnection();
                using (var adapter = new NpgsqlDataAdapter(cmd))
                {
                    adapter.Fill(dataTable);
                }
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Pb de executeSelect \n" + cmd.CommandText);
                throw;
            }
            return dataTable;
        }

        public static int ExecuteInsert(NpgsqlCommand cmd)
        {
            int nb = 0;
            try
            {
                cmd.Connection = GetConnection();
                nb = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Pb de executeInsert \n" + cmd.CommandText);
                throw;
            }
            return nb;
        }

        public static int ExecuteSet(NpgsqlCommand cmd)
        {
            int nb = 0;
            try
            {
                cmd.Connection = GetConnection();
                nb = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Pb de executeSet \n" + cmd.CommandText);
                throw;
            }
            return nb;
        }

        public static string ExecuteSelectOneValue(NpgsqlCommand cmd)
        {
            object res = null;
            try
            {
                cmd.Connection = GetConnection();
                res = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                LogError.Log(ex, "Pb de ExecuteSelectOneValue \n" + cmd.CommandText);
                throw;
            }
            return res.ToString();
        }

        public static void CloseConnection()
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
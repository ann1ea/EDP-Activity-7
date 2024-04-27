using MySql.Data.MySqlClient;
using System;

namespace AZUL_Bookstore
{
    public class DatabaseManager
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        // Constructor
        public DatabaseManager(string server, string database, string uid, string password)
        {
            this.server = server;
            this.database = database;
            this.uid = uid;
            this.password = password;
            InitializeConnection();
        }

        // Initialize the database connection
        private void InitializeConnection()
        {
            string connectionString;
            connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";

            connection = new MySqlConnection(connectionString);
        }

        // Open the database connection
        public bool OpenConnection()
        {
            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                    connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        // Close the database connection
        public bool CloseConnection()
        {
            try
            {
                if (connection.State != System.Data.ConnectionState.Closed)
                    connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        // Return the connection object
        public MySqlConnection GetConnection()
        {
            return connection;
        }
    }
}
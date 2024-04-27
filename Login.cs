using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace AZUL_Bookstore
{

    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            // Query to check if the username and password combination exists in the database
            string query = $"SELECT * FROM users WHERE username = '{username}' AND password = '{password}'";

            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=127.0.0.1;uid=root;" +
                "pwd=BicolU4851;database=bookstore";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    // Login successful
                    MessageBox.Show("Login successful!");

                    // Hide the login form
                    this.Hide();

                    // Show the Dashboard form
                    Dashboard dashboardForm = new Dashboard();
                    dashboardForm.ShowDialog(); // Use Show() if you don't want to block the Login form

                    // Close the connection
                    conn.Close();
                }
                else
                {
                    // Login failed
                    MessageBox.Show("Invalid username or password!");
                }

                reader.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void connecttoMYSQL_Click(object sender, EventArgs e)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=127.0.0.1;uid=root;" +
                "pwd=BicolU4851;database=bookstore";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                MessageBox.Show("Connected!");
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Password_Recovery pw_recForm = new Password_Recovery();
            pw_recForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            // Check if username or password is empty
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            // Add new user to the database
            string connectionString = "server=127.0.0.1;uid=root;pwd=BicolU4851;database=bookstore";
            string query = $"INSERT INTO users (username, password) VALUES ('{username}', '{password}')";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Sign-up successful!");
                        }
                        else
                        {
                            MessageBox.Show("Failed to sign up. Please try again.");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("An error occurred while signing up: " + ex.Message);
            }
        }
    }
}
using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AZUL_Bookstore
{
    public partial class Password_Recovery : Form
    {
        public Password_Recovery()
        {
            InitializeComponent();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string newPassword = textBox2.Text;
            string confirmPassword = textBox3.Text;

            // Check if the new password and confirm password match
            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirm password do not match.");
                return;
            }

            // Update the password in the database
            string connectionString = "server=127.0.0.1;uid=root;pwd=BicolU4851;database=bookstore";
            string query = $"UPDATE users SET password = '{newPassword}' WHERE username = '{username}'";

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
                            MessageBox.Show("Password reset successful.");
                            this.Close(); // Close the Password_Recovery form after successful password reset
                        }
                        else
                        {
                            MessageBox.Show("Failed to reset password. Username not found.");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("An error occurred while resetting password: " + ex.Message);
            }
        }
    }
}
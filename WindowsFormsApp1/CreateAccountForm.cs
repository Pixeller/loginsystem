using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class CreateAccountForm : Form
    {
        private DatabaseHelper db; // Reference to the DatabaseHelper class to manage user credentials

        public CreateAccountForm(DatabaseHelper databaseHelper)
        {
            InitializeComponent();
            this.db = databaseHelper;
        }

        private void createAccountButton_Click(object sender, EventArgs e)
        {
            string newUsername = newUsernameTextBox.Text;
            string newPassword = newPasswordTextBox.Text;

            // Check if the username is already taken
            if (db.IsValidUser(newUsername, ""))
            {
                MessageBox.Show("Username already exists. Please choose a different username.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Add the new user to the Users table
            db.Insert(newUsername, newPassword);

            // Show a success message to the user
            MessageBox.Show("Account created successfully!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Close the CreateAccountForm
            this.Close();
        }

        private void CreateAccountForm_Load(object sender, EventArgs e)
        {

        }
    }
}

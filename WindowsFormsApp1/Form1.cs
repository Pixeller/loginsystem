using System;
using System.Data.SQLite;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace SimpleLoginSite
{
    public partial class LoginForm : Form
    {
        private DatabaseHelper db; // Create an instance of the DatabaseHelper class
        private const string DatabaseFileName = "users.db";

        public LoginForm()
        {
            InitializeComponent();
            db = new DatabaseHelper(DatabaseFileName); // Initialize the DatabaseHelper with the database file name
            InitializeDatabase(); // Create the "Users" table if it doesn't exist
        }

        private void InitializeDatabase()
        {
            if (!db.TableExists("Users"))
            {
                db.ExecuteNonQuery("CREATE TABLE Users (Id INTEGER PRIMARY KEY AUTOINCREMENT, Username TEXT NOT NULL, Password TEXT NOT NULL);");
                db.Insert("INSERT INTO Users (Username, Password) VALUES ('Admin', 'Secret');");
                db.Insert("INSERT INTO Users (Username, Password) VALUES ('JohnDoe', '123456');");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            bool isValidUser = db.IsValidUser(username, password); // Check if the user is valid

            if (isValidUser)
            {
                MessageBox.Show("Login Successful. Welcome!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Perform actions or open a new form for the authenticated user here.
                Form2 form2 = new Form2();
                form2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid credentials. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DatabaseHelper databaseHelper = new DatabaseHelper(DatabaseFileName);

            // Assuming you have a form for creating new accounts named "CreateAccountForm"
            CreateAccountForm createAccountForm = new CreateAccountForm(databaseHelper); // Pass the 'databaseHelper' instance
            createAccountForm.ShowDialog();

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}

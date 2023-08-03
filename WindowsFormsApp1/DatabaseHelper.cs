using System;
using System.Data.SQLite;
using System.Data;

public class DatabaseHelper
{
    private string connectionString;

    public DatabaseHelper(string databaseFileName)
    {
        connectionString = $"Data Source={databaseFileName};Version=3;";
    }

    public bool TableExists(string tableName)
    {
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string query = $"SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='{tableName}'";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                long count = (long)command.ExecuteScalar();
                return count > 0;
            }
        }
    }

    public void ExecuteNonQuery(string query)
    {
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    public bool IsValidUser(string username, string password)
    {
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string query = $"SELECT COUNT(*) FROM Users WHERE Username = @username AND Password = @password";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                long count = (long)command.ExecuteScalar();
                return count > 0;
            }
        }
    }

    public void Insert(string newUsername, string newPassword)
    {
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            string query = "INSERT INTO Users (Username, Password) VALUES (@username, @password)";
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@username", newUsername);
                command.Parameters.AddWithValue("@password", newPassword);
                command.ExecuteNonQuery();
            }
        }
    }

    // This is the new method to insert a user
    public void InsertUser(string newUsername, string newPassword)
    {
        Insert(newUsername, newPassword);
    }

    internal void Insert(string v)
    {
        throw new NotImplementedException();
    }
}

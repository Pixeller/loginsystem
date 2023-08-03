using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using static Users;

public class Users
{
    // Nested class to hold user credentials
    public class UserCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    // List to store user credentials
    private List<UserCredentials> usersList = new List<UserCredentials>();

    // Method to add new users to the list
    public void AddUser(string username, string password)
    {
        usersList.Add(new UserCredentials { Username = username, Password = password });
    }

    // Method to check if a user is valid
    public bool IsValidUser(string username, string password)
    {
        return usersList.Exists(user => user.Username == username && user.Password == password);
    }

    // Method to save user credentials to a file
    public void SaveUserCredentialsToFile(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (var user in usersList)
            {
                writer.WriteLine($"{user.Username},{user.Password}");
            }
        }
    }

    // Method to load user credentials from a file
    public void LoadUserCredentialsFromFile(string fileName)
    {
        if (File.Exists(fileName))
        {
            string[] lines = File.ReadAllLines(fileName);
            foreach (var line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 2)
                {
                    usersList.Add(new UserCredentials { Username = parts[0], Password = parts[1] });
                }
            }
        }
    }
}

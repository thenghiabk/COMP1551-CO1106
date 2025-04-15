using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace ContactManagementSystemOOP
{
    public class Contact
    {
        public int Id { get; set; } // For database compatibility
        public string Name { get; set; } 
        public string PhoneNumber { get; set; } 
        public string Email { get; set; }

    public Contact(string name, string phone, string email)
        {
            Name = name;
            PhoneNumber = phone;
            Email = email;
        }

        public Contact(int id, string name, string phone, string email)
        {
            Id = id;
            Name = name;
            PhoneNumber = phone;
            Email = email;
        }

        public void Display()
        {
            Console.WriteLine($"  Name: {Name}, Phone: {PhoneNumber}, Email: {Email}");
        }
    }

    public class Program
    {
        private static string connectionString = "Server=localhost;Database=contactmanagementsystem;Uid=root;Pwd=ead8686ba57479778a76e;";
        static int maxContacts = 100;
        static Contact[] contacts = new Contact[maxContacts];
        static int contactCount = 0;

        public static void Main(string[] args)
        {
            // Load existing contacts from database into array at startup
            LoadContactsFromDatabase();

            bool running = true;
            while (running)
            {
                Console.WriteLine("\nContact Management System");
                Console.WriteLine("1. Add Contact");
                Console.WriteLine("2. Display Contacts");
                Console.WriteLine("3. Find a Contact");
                Console.WriteLine("4. Remove a Contact");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddContact();
                        break;
                    case "2":
                        DisplayContacts();
                        break;
                    case "3":
                        FindContact();
                        break;
                    case "4":
                        RemoveContact();
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }

            // Save all contacts to database when exiting
            SaveContactsToDatabase();
            Console.WriteLine("Data saved to database. Goodbye!");
        }

        static void LoadContactsFromDatabase()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id, name, phone_number, email FROM contacts";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    contactCount = 0;
                    while (reader.Read() && contactCount < maxContacts)
                    {
                        contacts[contactCount] = new Contact(
                            reader.GetInt32("id"),
                            reader.GetString("name"),
                            reader.GetString("phone_number"),
                            reader.GetString("email")
                        );
                        contactCount++;
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading contacts from database: {ex.Message}");
                }
            }
        }

        static void SaveContactsToDatabase()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Step 1: Get existing contacts from database
                    List<string> dbContactNames = new List<string>();
                    Dictionary<string, int> dbContactIds = new Dictionary<string, int>();
                    string selectQuery = "SELECT id, name FROM contacts";
                    MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn);
                    MySqlDataReader reader = selectCmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string name = reader.GetString("name");
                        int id = reader.GetInt32("id");
                        dbContactNames.Add(name);
                        dbContactIds[name] = id;
                    }
                    reader.Close();

                    // Step 2: Update or insert contacts from array
                    foreach (Contact contact in contacts)
                    {
                        if (contact == null) continue;

                        if (dbContactNames.Contains(contact.Name, StringComparer.OrdinalIgnoreCase))
                        {
                            // Update existing contact
                            string updateQuery = "UPDATE contacts SET phone_number = @phone, email = @email WHERE name = @name";
                            MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn);
                            updateCmd.Parameters.AddWithValue("@phone", contact.PhoneNumber);
                            updateCmd.Parameters.AddWithValue("@email", contact.Email);
                            updateCmd.Parameters.AddWithValue("@name", contact.Name);
                            updateCmd.ExecuteNonQuery();
                        }
                        else
                        {
                            // Insert new contact
                            string insertQuery = "INSERT INTO contacts (name, phone_number, email) VALUES (@name, @phone, @email)";
                            MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                            insertCmd.Parameters.AddWithValue("@name", contact.Name);
                            insertCmd.Parameters.AddWithValue("@phone", contact.PhoneNumber);
                            insertCmd.Parameters.AddWithValue("@email", contact.Email);
                            insertCmd.ExecuteNonQuery();
                        }
                    }

                    // Step 3: Remove contacts from database that are no longer in the array
                    List<string> arrayContactNames = new List<string>();
                    for (int i = 0; i < contactCount; i++)
                    {
                        if (contacts[i] != null)
                            arrayContactNames.Add(contacts[i].Name);
                    }

                    foreach (string dbName in dbContactNames)
                    {
                        if (!arrayContactNames.Contains(dbName, StringComparer.OrdinalIgnoreCase))
                        {
                            string deleteQuery = "DELETE FROM contacts WHERE name = @name";
                            MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, conn);
                            deleteCmd.Parameters.AddWithValue("@name", dbName);
                            deleteCmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving contacts to database: {ex.Message}");
                }
            }
        }

        static void AddContact()
        {
            if (contactCount < maxContacts)
            {
                Console.Write("Enter contact name: ");
                string name = Console.ReadLine();

                Console.Write("Enter phone number: ");
                string phone = Console.ReadLine();

                Console.Write("Enter email address: ");
                string email = Console.ReadLine();

                contacts[contactCount] = new Contact(name, phone, email);
                contactCount++;
                Console.WriteLine("Contact added!");
            }
            else
            {
                Console.WriteLine("Contact list is full!");
            }
        }

        static void DisplayContacts()
        {
            if (contactCount == 0)
            {
                Console.WriteLine("No contacts to display.");
                return;
            }

            Console.WriteLine("Contacts:");
            for (int i = 0; i < contactCount; i++)
            {
                contacts[i].Display();
            }
        }

        static void FindContact()
        {
            if (contactCount == 0)
            {
                Console.WriteLine("No contacts to find.");
                return;
            }

            Console.Write("Enter the name to search for: ");
            string searchName = Console.ReadLine();

            bool found = false;
            for (int i = 0; i < contactCount; i++)
            {
                if (contacts[i].Name.Equals(searchName, StringComparison.OrdinalIgnoreCase))
                {
                    contacts[i].Display();
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Contact Not Found.");
            }
        }

        static void RemoveContact()
        {
            if (contactCount == 0)
            {
                Console.WriteLine("No contacts to remove.");
                return;
            }

            Console.Write("Enter the name of the contact to remove: ");
            string nameToRemove = Console.ReadLine();

            int indexToRemove = -1;
            for (int i = 0; i < contactCount; i++)
            {
                if (contacts[i].Name.Equals(nameToRemove, StringComparison.OrdinalIgnoreCase))
                {
                    indexToRemove = i;
                    break;
                }
            }

            if (indexToRemove != -1)
            {
                for (int i = indexToRemove; i < contactCount - 1; i++)
                {
                    contacts[i] = contacts[i + 1];
                }
                contacts[contactCount - 1] = null;
                contactCount--;
                Console.WriteLine("Contact removed.");
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace ContactManagementSystemOOP
{
    public class Contact
    {
        public int Id { get; set; } // Added for database primary key
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public Contact(int id, string name, string phone, string email)
        {
            Id = id;
            Name = name;
            PhoneNumber = phone;
            Email = email;
        }

        public Contact(string name, string phone, string email)
        {
            Name = name;
            PhoneNumber = phone;
            Email = email;
        }

        public void Display()
        {
            Console.WriteLine($"  ID: {Id}, Name: {Name}, Phone: {PhoneNumber}, Email: {Email}");
        }
    }

    public class Program
    {
        // MySQL connection string
        private static string connectionString = "Server=localhost;Database=contactmanagementsystem;Uid=root;Pwd=ead8686ba57479778a76e;";

        public static void Main(string[] args)
        {
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

            Console.WriteLine("Goodbye!");
        }

        static void AddContact()
        {
            Console.Write("Enter contact name: ");
            string name = Console.ReadLine();

            Console.Write("Enter phone number: ");
            string phone = Console.ReadLine();

            Console.Write("Enter email address: ");
            string email = Console.ReadLine();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO contacts (name, phone_number, email) VALUES (@name, @phone, @email)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Contact added!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error adding contact: {ex.Message}");
                }
            }
        }

        static void DisplayContacts()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id, name, phone_number, email FROM contacts";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        Console.WriteLine("No contacts to display.");
                        reader.Close();
                        return;
                    }

                    Console.WriteLine("Contacts:");
                    while (reader.Read())
                    {
                        Contact contact = new Contact(
                            reader.GetInt32("id"),
                            reader.GetString("name"),
                            reader.GetString("phone_number"),
                            reader.GetString("email")
                        );
                        contact.Display();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error displaying contacts: {ex.Message}");
                }
            }
        }

        static void FindContact()
        {
            Console.Write("Enter the name to search for: ");
            string searchName = Console.ReadLine();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id, name, phone_number, email FROM contacts WHERE name LIKE @name";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", $"%{searchName}%");
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        Console.WriteLine("Contact Not Found.");
                        reader.Close();
                        return;
                    }

                    while (reader.Read())
                    {
                        Contact contact = new Contact(
                            reader.GetInt32("id"),
                            reader.GetString("name"),
                            reader.GetString("phone_number"),
                            reader.GetString("email")
                        );
                        contact.Display();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error finding contact: {ex.Message}");
                }
            }
        }

        static void RemoveContact()
        {
            Console.Write("Enter the ID of the contact to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int idToRemove))
            {
                Console.WriteLine("Invalid ID format.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM contacts WHERE id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", idToRemove);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Contact removed.");
                    }
                    else
                    {
                        Console.WriteLine("Contact not found.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error removing contact: {ex.Message}");
                }
            }
        }
    }
}
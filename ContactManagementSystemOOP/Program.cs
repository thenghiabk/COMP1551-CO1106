using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementSystemOOP
{
    class Contact
    {
        // fields / attributes
        private string name;
        private string phoneNumber;
        private string email;

        // constructor
        public Contact(string name, string phoneNumber, string email)
        {
            this.name = name;
            this.phoneNumber = phoneNumber;
            this.email = email;
        }

        // methods (helpers)
        public void displayInfo()
        {
            Console.WriteLine($"Name: {this.name}");
            Console.WriteLine($"Phone Number: {this.phoneNumber}");
            Console.WriteLine($"Email: {this.email}");
        }
    }
    public class Program
    {
        // Maximum number of contacts we can store (you can change this)
        static int maxContacts = 100;

        // Create an array to store contact objects instead of individual arrays for names, phone numbers, and emails
        static Contact[] contacts = new Contact[maxContacts];

        // Keep track of how many contacts we have
        static int contactCount = 0;

        public static void Main(string[] args)
        {
            // The main loop of our program
            bool running = true;
            while (running)
            {
                Console.WriteLine("\nContact Management System");
                Console.WriteLine("1. Add Contact"); // JAMES+AN+VINH
                Console.WriteLine("2. Display Contacts"); // KHANH+KING
                Console.WriteLine("3. Find a Contact"); // HAN+NGUYEN+VINH
                Console.WriteLine("4. Remove a Contact");// THUY+HIEU+VU
                Console.WriteLine("5. Edit a Contact"); // DUY+BAO+CUONG+KHANG
                Console.WriteLine("6. Clear Contacts");  // KHANH+KING
                Console.WriteLine("7. Exit");
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
                        //TODO: EditContact
                        break;
                    case "6":
                        //TODO: Clear all contacts
                        break;
                    case "7":
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
            if (contactCount < maxContacts)
            {
                // Create a contact object and add it to the array of contacts

                Console.Write("Enter contact name: ");
                String name = Console.ReadLine();

                Console.Write("Enter phone number: ");
                String phoneNumber = Console.ReadLine();

                Console.Write("Enter email address: ");
                String email = Console.ReadLine();

                contacts[contactCount] = new Contact(name, phoneNumber, email);

                contactCount++; // Important: Increment the count!

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

            // Loop through the array of contacts and display them
            for (int i = 0; i < contactCount; i++)
            {
                Console.WriteLine($"Contact {i + 1}:");
                contacts[i].displayInfo();
                Console.WriteLine();
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

            // TODO: Loop through the array of contacts and find the contact with the matching name

            for (int i = 0; i < contactCount; i++)
            {
                if(contacts[i].name.Equals(searchName, StringComparison.OrdinalIgnoreCase))
                {

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

            int indexToRemove = -1; // -1 means we haven't found it yet

            // TODO: Loop through the array of contacts and find the contact with the matching name

            if (indexToRemove != -1)
            {
                // TODO: Remove the contact from the array based on the index

                contactCount--; // Decrement the count

                Console.WriteLine("Contact removed.");
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }
    }
}

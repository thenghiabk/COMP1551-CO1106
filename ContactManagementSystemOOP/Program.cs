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

        public bool FindContactByName(string searchName)
        {
            if (this.name.Equals(searchName, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        public void UpdateInfo(string newPhoneNumber, string newEmail)
        {
            this.phoneNumber = newPhoneNumber;
            this.email = newEmail;
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
                        EditContact();
                        break;
                    case "6":
                        ClearAllContacts();
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

        private static void ClearAllContacts()
        {
            for(int i = 0; i < contactCount; i++)
            {
                contacts[i] = null; // Set each contact to null
            }

            contactCount = 0; // Reset the count
        }

        private static void EditContact()
        {
            if (contactCount == 0)
            {
                Console.WriteLine("No contacts to edit.");
                return;
            }

            Console.Write("Enter the name of the contact to edit: ");
            string nameToEdit = Console.ReadLine();

            int indexToEdit = -1; // -1 means we haven't found it yet

            for (int i = 0; i < contactCount; i++)
            {
                if (contacts[i].FindContactByName(nameToEdit))
                {
                    indexToEdit = i;
                    break; // Exit the loop once we find the contact
                }
            }

            if (indexToEdit != -1)
            {

                Console.Write("Enter new phone number: ");
                String newPhoneNumber = Console.ReadLine();

                Console.Write("Enter new email address: ");
                String newEmail = Console.ReadLine();

                contacts[indexToEdit].UpdateInfo(newPhoneNumber, newEmail);

                Console.WriteLine("Contact has been edited.");
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
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

            // Loop through the array of contacts and find the contact with the matching name

            for (int i = 0; i < contactCount; i++)
            {
                if(contacts[i].FindContactByName(searchName))
                {
                    contacts[i].displayInfo();
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

            int indexToRemove = -1; // -1 means we haven't found it yet

            for (int i = 0; i < contactCount; i++)
            {
                if (contacts[i].FindContactByName(nameToRemove))
                {
                    indexToRemove = i;
                    break; // Exit the loop once we find the contact
                }
            }

            if (indexToRemove != -1)
            {
                // Remove the contact from the array based on the index

                contacts[indexToRemove] = null; // Set the contact to null

                // Shift elements to fill the gap
                for (int i = indexToRemove; i < contactCount - 1; i++)
                {
                    contacts[i] = contacts[i + 1];
                }

                contacts[contactCount - 1] = null;

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

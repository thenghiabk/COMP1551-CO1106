using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ContactManagementSystem
{
    public class Contact
    {
        // attributes / fields
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        // contructor
        public Contact(string name, string phoneNumber, string email, string address)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            Address = address;
        }

        // methods
    }

    public class PersonalContact : Contact
    {
        public string Relationship { get; set; }

        public PersonalContact(string name, string phoneNumber, string email, string address, string relationship)
            : base(name, phoneNumber, email, address)
        {
            Relationship = relationship;
        }
    }

    public class BusinessContact : Contact
    {
        public string Company { get; set; }
        public string JobTitle { get; set; }

        public BusinessContact(string name, string phoneNumber, string email, string address, string company, string jobTitle)
            : base(name, phoneNumber, email, address)
        {
            Company = company;
            JobTitle = jobTitle;
        }
    }

    public class ContactBook
    {
        private List<Contact> contacts = new List<Contact>();

        public void AddContact(Contact contact)
        {
            contacts.Add(contact);
        }

        public void RemoveContact(Contact contact)
        {
            contacts.Remove(contact);
        }
        public void EditContact(Contact contactToEdit)
        {
            // Find the index of the contact to edit
            int index = contacts.IndexOf(contactToEdit);

            if (index != -1)
            {
                Console.WriteLine("Enter new name (leave empty to keep current): ");
                string newName = Console.ReadLine();
                if (!string.IsNullOrEmpty(newName))
                {
                    contacts[index].Name = newName;
                }

                // Similar logic for other contact properties (phone, email, address, etc.)

                Console.WriteLine("Contact updated successfully.");
            }
            else
            {
                Console.WriteLine("Contact not found.");
            }
        }
        public List<Contact> GetContacts()
        {
            return new List<Contact>(contacts); // Return a copy to prevent modification
        }

        public List<Contact> GetContactsByType(string type)
        {
            List<Contact> results = new List<Contact>();
            foreach (var contact in contacts)
            {
                if (type == "personal" && contact is PersonalContact)
                {
                    results.Add(contact);
                }
                else if (type == "business" && contact is BusinessContact)
                {
                    results.Add(contact);
                }
            }
            return results;
        }

        public Contact FindContactByName(string name)
        {
            foreach (var contact in contacts)
            {
                if (contact.Name == name)
                {
                    return contact;
                }
            }
            return null; // Or throw an exception if preferred
        }

        public List<Contact> FindContactsBySimilarName(string searchTerm)
        {
            List<Contact> results = new List<Contact>();
            foreach (var contact in contacts)
            {
                if (contact.Name.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    results.Add(contact);
                }
            }
            return results;
        }

        // Other methods for searching, sorting, etc.
    }

    class Program
    {
        static void Main(string[] args)
        {
            ContactBook contactBook = new ContactBook();

            // Add sample data
            contactBook.AddContact(new PersonalContact("John Doe", "1234567890", "johndoe@example.com", "123 Main St", "Friend"));
            contactBook.AddContact(new BusinessContact("Jane Smith", "9876543210", "janesmith@example.com", "456 Elm St", "Acme Corp", "CEO"));
            contactBook.AddContact(new PersonalContact("Bob Johnson", "5551212", "bobjohnson@example.com", "789 Oak St", "Family"));
            contactBook.AddContact(new BusinessContact("Michael Brown", "4445555", "michaelbrown@example.com", "101 Pine St", "TechCo", "Engineer"));
            contactBook.AddContact(new PersonalContact("Emily Davis", "7778888", "emilydavis@example.com", "222 Cedar St", "Neighbor"));

            bool running = true;

            while (running)
            {
                Console.WriteLine("/-------------------");
                Console.WriteLine("1. Add Contact");
                Console.WriteLine("2. View Contacts");
                Console.WriteLine("3. View Contacts (by type)");
                Console.WriteLine("4. Find Contact");
                Console.WriteLine("5. Edit Contact");
                Console.WriteLine("6. Remove Contact");
                Console.WriteLine("7. Exit");
                Console.WriteLine("-------------------/");
                Console.Write("Enter your choice: ");


                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // Add contact logic
                        Console.Write("Enter name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter phone number: ");
                        string phoneNumber = Console.ReadLine();
                        Console.Write("Enter email: ");
                        string email = Console.ReadLine();
                        Console.Write("Enter address: ");
                        string address = Console.ReadLine();

                        // Determine contact type (Personal or Business)
                        Console.Write("Is this a personal or business contact (P/B)? ");
                        string contactType = Console.ReadLine().ToLower();

                        if (contactType == "p")
                        {
                            Console.Write("Enter relationship: ");
                            string relationship = Console.ReadLine();
                            contactBook.AddContact(new PersonalContact(name, phoneNumber, email, address, relationship));
                        }
                        else if (contactType == "b")
                        {
                            Console.Write("Enter company: ");
                            string company = Console.ReadLine();
                            Console.Write("Enter job title: ");
                            string jobTitle = Console.ReadLine();
                            contactBook.AddContact(new BusinessContact(name, phoneNumber, email, address, company, jobTitle));
                        }
                        else
                        {
                            Console.WriteLine("Invalid contact type.");
                        }
                        break;

                    case "2":
                        // View contacts logic
                        var contacts = contactBook.GetContacts();
                        if (contacts.Count == 0)
                        {
                            Console.WriteLine("No contacts found.");
                        }
                        else
                        {
                            Console.WriteLine("Contacts:");
                            foreach (var contact in contacts)
                            {
                                Console.WriteLine($"Name: {contact.Name}, Phone: {contact.PhoneNumber}, Email: {contact.Email}, Address: {contact.Address}");
                                if (contact is PersonalContact personalContact)
                                {
                                    Console.WriteLine($"Relationship: {personalContact.Relationship}");
                                }
                                else if (contact is BusinessContact businessContact)
                                {
                                    Console.WriteLine($"Company: {businessContact.Company}, Job Title: {businessContact.JobTitle}");
                                }
                                Console.WriteLine("---");
                            }
                        }
                        break;

                    case "3":
                        Console.Write("View personal or business contacts (P/B)? ");
                        string typeOfContact = Console.ReadLine().ToLower();
                        typeOfContact = typeOfContact == "p" ? "personal" : "business";

                        var filteredContacts = contactBook.GetContactsByType(typeOfContact);
                        if (filteredContacts.Count > 0)
                        {
                            foreach (var contact in filteredContacts)
                            {
                                Console.WriteLine($"Name: {contact.Name}, Phone: {contact.PhoneNumber}, Email: {contact.Email}, Address: {contact.Address}");
                                if (contact is PersonalContact personalContact)
                                {
                                    Console.WriteLine($"Relationship: {personalContact.Relationship}");
                                }
                                else if (contact is BusinessContact businessContact)
                                {
                                    Console.WriteLine($"Company: {businessContact.Company}, Job Title: {businessContact.JobTitle}");
                                }
                                Console.WriteLine("---");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No contacts found for the specified type.");
                        }
                        break;

                    case "4":
                        // Find contact logic
                        Console.Write("Enter contact name: ");
                        string searchName = Console.ReadLine();
                        var foundContact = contactBook.FindContactByName(searchName);
                        if (foundContact != null)
                        {
                            Console.WriteLine($"Name: {foundContact.Name}, Phone: {foundContact.PhoneNumber}, Email: {foundContact.Email}, Address: {foundContact.Address}");
                            if (foundContact is PersonalContact personalContact)
                            {
                                Console.WriteLine($"Relationship: {personalContact.Relationship}");
                            }
                            else if (foundContact is BusinessContact businessContact)
                            {
                                Console.WriteLine($"Company: {businessContact.Company}, Job Title: {businessContact.JobTitle}");
                            }
                            Console.WriteLine("---");
                        }
                        else
                        {
                            Console.WriteLine("Contact not found. But we have found some contacts whose names are similar to a given search term.");

                            var foundSimimarContacts = contactBook.FindContactsBySimilarName(searchName);
                            foreach (var contact in foundSimimarContacts)
                            {
                                Console.WriteLine($"Name: {contact.Name}, Phone: {contact.PhoneNumber}, Email: {contact.Email}, Address: {contact.Address}");
                                if (contact is PersonalContact personalContact)
                                {
                                    Console.WriteLine($"Relationship: {personalContact.Relationship}");
                                }
                                else if (contact is BusinessContact businessContact)
                                {
                                    Console.WriteLine($"Company: {businessContact.Company}, Job Title: {businessContact.JobTitle}");
                                }
                                Console.WriteLine("---");
                            }

                        }
                        break;

                    case "5":
                        // Edit contact logic
                        Console.Write("Enter contact name to edit: ");
                        string editName = Console.ReadLine();
                        var contactToEdit = contactBook.FindContactByName(editName);
                        if (contactToEdit != null)
                        {
                            contactBook.EditContact(contactToEdit);
                        }
                        else
                        {
                            Console.WriteLine("Contact not found.");
                        }
                        break;

                    case "6":
                        // Remove contact logic
                        Console.Write("Enter contact name to remove: ");
                        string removeName = Console.ReadLine();
                        var contactToRemove = contactBook.FindContactByName(removeName);
                        if (contactToRemove != null)
                        {
                            contactBook.RemoveContact(contactToRemove);
                            Console.WriteLine("Contact removed.");
                        }
                        else
                        {
                            Console.WriteLine("Contact not found.");
                        }
                        break;

                    case "7":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

    }
}
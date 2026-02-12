using CONTACTS.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CONTACTS.Controllers
{
    public class ContactController
    {
        public List<Contact> Contacts;
        public int nextId = 1;
        public string filePath = "contacts.txt";


        public ContactController()
        {
            this.Contacts = new List<Contact>();

            if (!File.Exists(filePath))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                }
            }
            else
            {
                LoadFromFile();
            }
        }


        public List<Contact> GetContact()
        {
            return this.Contacts;
        }

        public void ShowAllContacts()
        {
            if (this.Contacts.Count == 0)
            {
                Console.WriteLine("There is no contact!");
            }
            else
            {
                foreach (Contact contact in this.Contacts)
                {
                    Console.WriteLine($"ID: {contact.id}" +
                                      $"\nName: {contact.name}" +
                                      $"\nNumber: {contact.number}\n");
                }
            }
        }

        public void AddContact(string name, string number)
        {
            string checkedNumber = CheckNumber(number);

            if (checkedNumber == null)
            {
                Console.WriteLine("Number format is invalid!");
                return;
            }

            this.Contacts.Add(new Contact(nextId, name, checkedNumber));
            nextId++;
            SaveAllToFile();
        }

        public void RemoveContact(int id)
        {
            Contact contact = this.Contacts.Find(c => c.id == id);

            if (contact == null)
            {
                Console.WriteLine("Contact not found!");
                return;
            }

            this.Contacts.Remove(contact);
            SaveAllToFile() ;
        }

        public void EditContact(int id)
        {
            Contact contact = this.Contacts.Find(c => c.id == id);

            if (contact == null)
            {
                Console.WriteLine("Contact not found!");
                return;
            }

            Console.WriteLine("Which property do you want to edit?");
            Console.WriteLine("1. Name\n2. Number");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input!");
                return;
            }

            if (choice == 1)
            {
                Console.WriteLine("Enter new name:");
                contact.name = Console.ReadLine();
            }
            else if (choice == 2)
            {
                Console.WriteLine("Enter new number:");
                string newNumber = CheckNumber(Console.ReadLine());

                if (newNumber == null)
                {
                    Console.WriteLine("Number format is invalid!");
                    return;
                }

                contact.number = newNumber;
            }
            else
            {
                Console.WriteLine("There is no option like that");
            }
        }
        private void LoadFromFile()
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            using (StreamReader sr = new StreamReader(fs))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split('|');
                    if (parts.Length == 3)
                    {
                        int id = int.Parse(parts[0]);
                        string name = parts[1];
                        string number = parts[2];

                        this.Contacts.Add(new Contact(id, name, number));

                        if (id >= nextId)
                            nextId = id + 1;
                    }
                }
            }
        }
        private void SaveAllToFile()
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                foreach (var contact in this.Contacts)
                {
                    sw.WriteLine($"{contact.id}|{contact.name}|{contact.number}");
                }
            }
        }



        private static string CheckNumber(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                return null;

            if (Regex.IsMatch(number, @"^\+994\d{9}$"))
            {
                return number;
            }

            if (Regex.IsMatch(number, @"^0(70|50|60|55|51|99)\d{7}$"))
            {
                return "+994" + number.Substring(1);
            }

            return null;
        }
    }
}

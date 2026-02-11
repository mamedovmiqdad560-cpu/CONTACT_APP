using CONTACTS.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CONTACTS.Controllers
{
    public class ContactController
    {
        public Contact[] Contacts;
        private int nextId = 1;


        public ContactController()
        {
            this.Contacts = new Contact[1];
        }
        public Contact[] GetContact()
        {
            return this.Contacts;
        }
        public void ShowAllContacts()
        {
            if (this.Contacts.Length == 1 && this.Contacts[0] == null)
            {
                Console.WriteLine("There is no contact!");
            }
            else
            {
                foreach (Contact contact in this.Contacts)
                {
                    if (contact == null)
                    {
                        continue;
                    }
                    else
                    {
                        Console.WriteLine($"ID: {contact.id}" + $"\nName: {contact.name}" +
                            $"\nNumber: {contact.number}");
                    }
                }
            }
        }
        public void AddContact(string name, string number)
        {
            this.Contacts[this.Contacts.Length - 1] =
                new Contact(nextId, name, CheckNumber(number));

            nextId++;
            Array.Resize(ref this.Contacts, this.Contacts.Length + 1);
        }

        public void RemoveContact(int id)
        {
            this.Contacts[id - 1] = null;
        }
        public void EditContact(int id)
        {
            Console.WriteLine("Which property do you want to edit?");
            Console.WriteLine("1. Name" +
                                "\n2. Number");
            int choice = int.Parse(Console.ReadLine());
            if (choice == 1)
            {
                Console.WriteLine("Enter new name");
                Contacts[id - 1].name = Console.ReadLine();
            }
            else if (choice == 2)
            {
                Console.WriteLine("Enter new number");
                Contacts[id - 1].number = CheckNumber(Console.ReadLine());

            }
            else
            {
                Console.WriteLine("There is no option like that");
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

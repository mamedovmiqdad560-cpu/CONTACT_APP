using CONTACTS.Controllers;
using CONTACTS.Models;
using System.Numerics;

namespace Var
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ProfileController profileController = new ProfileController();

            while (true)
            {
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Show all contacts");
                Console.WriteLine("0. Exit");
                

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Write("Nickname: ");
                    string nickname = Console.ReadLine();

                    Console.Write("Password: ");
                    string password = Console.ReadLine();

                    ContactController contactController = new ContactController();

                    Profile profile = new Profile(
                        nickname,
                        password,
                        contactController.Contacts
                    );

                    profileController.Addprofile(profile);
                }
                else if (choice == "2")
                {
                    Console.Write("Nickname: ");
                    string nickname = Console.ReadLine();

                    Console.Write("Password: ");
                    string password = Console.ReadLine();

                    Profile loggedProfile =
                        profileController.GetProfile(nickname, password);

                    if (loggedProfile == null)
                        continue;

                    ContactController contactController = new ContactController();
                    contactController.Contacts = loggedProfile.Contacts;

                    ProfileMenu(loggedProfile, contactController);
                }
                else if (choice == "3")
                {
                    profileController.ShowAllprofiles();
                    
                }
                else if (choice == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong option!");
                }
            }
        }

        static void ProfileMenu(Profile profile, ContactController contactController)
        {
            while (true)
            {
                Console.WriteLine($"\n=== {profile.nickname}'s CONTACT MENU ===");
                Console.WriteLine("1. Add Contact");
                Console.WriteLine("2. Show Contacts");
                Console.WriteLine("3. Remove Contact");
                Console.WriteLine("4. Edit Contact");
                Console.WriteLine("0. Logout");

                string choice = Console.ReadLine();

                if (choice == "1")
                {


                    Console.Write("Name: ");
                    string name = Console.ReadLine();

                    Console.Write("Number: ");
                    string number = Console.ReadLine();

                    contactController.AddContact( name, number);
                }
                else if (choice == "2")
                {
                    contactController.ShowAllContacts();
                }
                else if (choice == "3")
                {
                    Console.Write("Enter ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int id))
                        continue;

                    if (id <= 0 || id > contactController.Contacts.Length ||
                        contactController.Contacts[id - 1] == null)
                    {
                        Console.WriteLine("Bele bir contact yoxdur!");
                        continue;
                    }

                    contactController.RemoveContact(id);
                }
                else if (choice == "4")
                {
                    Console.Write("Enter ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int id))
                        continue;

                    if (id <= 0 || id > contactController.Contacts.Length ||
                        contactController.Contacts[id - 1] == null)
                    {
                        Console.WriteLine("Bele bir contact yoxdur!");
                        continue;
                    }

                    contactController.EditContact(id);
                }
                else if (choice == "0")
                {
                    // logout olanda contactlari profile geri yaziriq
                    profile.Contacts = contactController.Contacts;
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong option!");
                }
            }
        }
    }
}




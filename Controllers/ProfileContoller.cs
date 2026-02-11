using CONTACTS.Models;
using CONTACTS.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CONTACTS.Controllers
{
    public class ProfileController
    {
        public Profile[] profiles;

        public ProfileController()
        {
            this.profiles = new Profile[1];
        }

        public Profile[] Getprofiles()
        {
            return this.profiles;
        }

        public void ShowAllprofiles()
        {
            if (this.profiles.Length == 1 && this.profiles[0] == null)
            {
                Console.WriteLine("There is no profile!");
            }
            else
            {
                foreach (Profile profile in this.profiles)
                {
                    if (profile == null)
                    {
                        continue;
                    }

                    Console.WriteLine(
                        $"Profile -> {Array.IndexOf(this.profiles, profile) + 1}\n" +
                        $"Name: {profile.nickname}\n" +
                        $"Password: {profile.password}\n" +
                        $"====================");
                }
            }
        }

        public void Addprofile(Profile profile)
        {
            if (CheckProfileData(profile.nickname, profile.password)&& CheckProfileUniqueness(profile.nickname,profile.password))
            {
                this.profiles[this.profiles.Length - 1] = profile;

                Array.Resize(ref this.profiles, this.profiles.Length + 1);
            }
        }

        public void Addprofile(string nickname, string password, Contact[] Contacts)
        {
            if (CheckProfileData(nickname, password)&& CheckProfileUniqueness(nickname,password))
            {
                this.profiles[this.profiles.Length - 1] =
                            new Profile(nickname, password,Contacts);

                Array.Resize(ref this.profiles, this.profiles.Length + 1);
            }
        }

        public Profile GetProfile(string nickname, string password)
        {
            foreach (Profile profile in this.profiles)
            {
                if (nickname.ToLower().Equals(profile.nickname.ToLower())
                    && password.Equals(profile.password))
                {
                    return profile;
                }
                else
                {
                    Console.WriteLine("Daxil olunan melumatlar uzre istifadeci tapilmadi!");
                }
            }

            return null;
        }

        
        public bool CheckProfileId(int profileId)
        {
            if (profileId <= 0 || profileId > this.profiles.Length)
            {
                Console.WriteLine("Daxil edilen ID yalnisdir!");
                return false;
            }
            else if (this.profiles[profileId - 1] == null)
            {
                Console.WriteLine("Bele bir profile tapilmadi");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool CheckProfileData(string nickname, string password)
        {
            if (Regex.IsMatch(nickname, @"^[A-Za-z]{6,}$")) 
            {
                if (Regex.IsMatch(password, @"^(?=.*[A-Z])(?=.*[a-z])(?=.*[^A-Za-z0-9]).{4,}$")) 
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Daxil edilen sifre standartlara uygun deyil!");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Daxil edilen profil adi standartlara uygun deyil!");
                return false;
            }

            
        }
        private bool CheckProfileUniqueness(string nickname, string password)
        {
            foreach (Profile profile in this.profiles)
            {
                if (profile == null)
                    continue;

                if (profile.nickname.ToLower() == nickname.ToLower())
                {
                    Console.WriteLine("Bu nickname artiq istifade olunur!");
                    return false;
                }

                if (profile.password == password)
                {
                    Console.WriteLine("Bu sifre ile artiq bir profil movcuddur!");
                    return false;
                }
            }

            return true;
        }

    }
}


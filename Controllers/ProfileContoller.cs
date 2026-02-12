using CONTACTS.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CONTACTS.Controllers
{
    public class ProfileController
    {
        private List<Profile> profiles;

        public ProfileController()
        {
            this.profiles = new List<Profile>();
        }

        public List<Profile> GetProfiles()
        {
            return this.profiles;
        }

        public void ShowAllProfiles()
        {
            if (this.profiles.Count == 0)
            {
                Console.WriteLine("There is no profile!");
            }
            else
            {
                for (int i = 0; i < this.profiles.Count; i++)
                {
                    Profile profile = this.profiles[i];

                    Console.WriteLine(
                        $"Profile -> {i + 1}\n" +
                        $"Name: {profile.nickname}\n" +
                        $"Password: {profile.password}\n" +
                        $"====================");
                }
            }
        }

        public void AddProfile(Profile profile)
        {
            if (CheckProfileData(profile.nickname, profile.password) &&
                CheckProfileUniqueness(profile.nickname, profile.password))
            {
                this.profiles.Add(profile);
            }
        }

        public void AddProfile(string nickname, string password, Contact[] contacts)
        {
            if (CheckProfileData(nickname, password) &&
                CheckProfileUniqueness(nickname, password))
            {
                this.profiles.Add(new Profile(nickname, password, contacts));
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
            }

            Console.WriteLine("Daxil olunan melumatlar uzre istifadeci tapilmadi!");
            return null;
        }

        public bool CheckProfileId(int profileId)
        {
            if (profileId <= 0 || profileId > this.profiles.Count)
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

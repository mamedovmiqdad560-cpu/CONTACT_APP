using System;
using System.Collections.Generic;
using System.Text;

namespace CONTACTS.Models
{
    public class Profile
    {
        public string nickname, password;
        public List<Contact> Contacts;


        public Profile(string nickname, string password, List<Contact> contacts)
        {
            this.nickname = nickname;
            this.password = password;
            this.Contacts = contacts;
        }
    }
}

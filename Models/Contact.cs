using System;
using System.Collections.Generic;
using System.Text;

namespace CONTACTS.Models
{
    public class Contact
    {
        public int id;
        public string name;
        public string number;

        public Contact(int id, string name, string number)
        {
            this.id = id;
            this.name = name;
            this.number = number;

        }
    }

}

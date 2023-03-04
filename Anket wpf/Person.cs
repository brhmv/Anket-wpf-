using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Anket_wpf
{
    public class Person
    {
        #region Properties
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? BDate { get; set; }
        #endregion

        public Person() { }

        public Person(string? name, string? surname, string? email, string? phone, string? dbdate)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Phone = phone;
            BDate = dbdate;
        }
    }
}

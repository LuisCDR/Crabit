using System;

namespace Crabit_API.Model
{
    public class Person
    {
        public string DNI { get; set; }
        public int CUI { get; set; }
        public string paternalSurname { get; set; }
        public string maternalSurname { get; set; }
        public string ftName { get; set; }
        public string sdName { get; set; }
        public DateTime birthday { get; set; }
        public char gender { get; set; }
        public string location { get; set; }
        public string maritalStatus { get; set; }
    }

    public class CreatePerson
    {
        public string DNI { get; set; }
        public int CUI { get; set; }
        public string paternalSurname { get; set; }
        public string maternalSurname { get; set; }
        public string ftName { get; set; }
        public string sdName { get; set; }
        public DateTime birthday { get; set; }
        public char gender { get; set; }
        public string location { get; set; }
        public string maritalStatus { get; set; }
    }

    public class UpdatePerson
    {
        public string location { get; set; }
        public string maritalStatus { get; set; }
    }
}

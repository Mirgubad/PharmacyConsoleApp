using System;

namespace PharmacyConsoleApp
{
    public class Employee
    {
        public int ID { get; set; }
        private static int _ID = 1;
        public string Name { get; set; }
        public string SurName { get; set; }
        public double Salary { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public RoleType roleType { get; set; }
        public DateTime BirthDate;
        public Employee()
        {
            ID = _ID;
            ++_ID;
        }
        public enum RoleType
        {
            Admin,
            Stuff
        }
    }
}

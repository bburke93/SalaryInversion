using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryInversion
{
    class Employee
    {
        public string College { get; set; }
        public string Department { get; set; }
        public string Name { get; set; }
        public string Rank { get; set; }
        public int Salary { get; set; }

        public Employee(string college, string department, string name, string rank, int salary)
        {
            College = college;
            Department = department;
            Name = name;
            Rank = rank;
            Salary = salary;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryInversion
{
    class Employee
    {
        private string College { get; set; }
        private string Department { get; set; }
        private string Name { get; set; }
        private string Rank { get; set; }
        private int Salary { get; set; }

        public Employee(string college, string department, string name, string rank, int salary)
        {
            this.College = college;
            this.Department = department;
            this.Name = name;
            this.Rank = rank;
            this.Salary = salary;
        }
    }
}

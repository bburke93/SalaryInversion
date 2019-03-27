using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryInversion
{
    class Department
    {
        public string Name { get; set; }
        public List<Employee> employees = new List<Employee>();

        public Department(string name)
        {
            Name = name;
        }
    }
}

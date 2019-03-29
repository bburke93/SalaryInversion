using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryInversion
{
    class College
    {
        public string Name { get; set; }
        public List<Department> departments = new List<Department>();

        public College(string name)
        {
            Name = name;
        }
    }
}

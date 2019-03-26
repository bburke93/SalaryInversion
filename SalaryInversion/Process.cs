using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SalaryInversion
{
    class Process
    {
        /// <summary>
        /// Contains all inverted employees, after calling GetEmployees
        /// </summary>
        private List<Employee> employees = new List<Employee>();

        /// <summary>
        /// Loads the list of inverted employees from csv file
        /// </summary>
        public List<Employee> GetEmployees()
        {
            var input = File.ReadAllLines("../../Data/2019eqmodelabbreviated.csv");
            foreach (string line in input)
            {
                var values = line.Split(',');

                // If statement simply skips the header row from the CSV file
                if (values[0] == "CLG")
                {
                    continue;
                }

                // Seperate values out for easier reading
                string college = values[0];
                string department = values[1];
                string name = values[2].Replace("\"", "") + ", " + values[3].Replace("\"", "");
                string rank = values[4];
                int salary = int.Parse(values[5]);

                employees.Add(new Employee(college, department, name, rank, salary));
            }
            return employees;
        }

        // Used for testing
        //public static void Main()
        //{
        //    GetEmployees();
        //    Console.WriteLine("Yeah, I'm right here.");
        //    Console.Read();
        //}
    }
}

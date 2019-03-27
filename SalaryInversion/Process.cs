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
        /// Get's a list containing all employees from the sample databse. Used for demonstration only.
        /// </summary>
        /// <returns>A list of Employee objects</returns>
        public List<Employee> GetEmployees()
        {
            return GetEmployees("../../Data/2019eqmodelabbreviated.csv");
        }

        /// <summary>
        /// Get's a list containg all employees.
        /// </summary>
        /// <param name="filePath">Filepath to a CSV containing all employees.</param>
        /// <returns>A list of Employee objects.</returns>
        public List<Employee> GetEmployees(string filePath)
        {
            List<Employee> employees = new List<Employee>();
            var input = File.ReadAllLines(filePath);

            // Get lines from input file and generate employee objects
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

        /// <summary>
        /// creates a DataGrid and set the ItemSource to the the function GetEmployees
        /// </summary>
        public DataGrid create_datagrid()
        {
            initial_datagrid = DataGrid()
            initial_datagrid.ItemSource(GetEmployees)
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

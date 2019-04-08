using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace SalaryInversion
{
    class Process
    {
        private DataAccess db;
        private SQLQueries query;

        public Process(string filePath)
        {
            db = new DataAccess(filePath);
            query = new SQLQueries();
        }

        public DataSet CountAndTotalCostInversionByDepartment()
        {
            return null;
        }

        /// <summary>
        /// Gets a DataSet object for the Inversion Count report.
        /// </summary>
        /// <returns>A DataSet containing a single table with columns for inverison types count and grouped by department.</returns>
        public DataSet CountInversionTypeByDepartment()
        {
            int returnedRows = 0;
            return db.ExecuteSQLStatement(query.CountInversionDepartmentSQL(), ref returnedRows);
        }

        /// <summary>
        /// Gets a DataSet object for the Inversion Cost report.
        /// </summary>
        /// <returns>A DataSet containing a single table with columns for inversion types cost and grouped by deparment.</returns>
        public DataSet CostInversionTypeByDepartment()
        {
            int returnedRows = 0;
            return db.ExecuteSQLStatement(query.CostInversionSQL(), ref returnedRows);
        }

        /// <summary>
        /// Gets a DataSet object for the Inverted Employees report.
        /// </summary>
        /// <returns>A DataSet containing a single table with data showing all inverted employees.</returns>
        public DataSet InvertedEmployees()
        {
            int returnedRows = 0;
            return db.ExecuteSQLStatement(query.InvertedEmployeesSQL(), ref returnedRows);
        }

        #region Deprecated Code For Employee objects/CSV
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
        #endregion
    }
}

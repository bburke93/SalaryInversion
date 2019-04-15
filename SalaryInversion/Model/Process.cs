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
        
        /// <summary>
        /// Gets a DataSet object for the Inversion Count by Department report.
        /// </summary>
        /// <returns>A DataSet containing a single table with columns for inverison types count and grouped by department.</returns>
        public DataSet CountInversionTypeByDepartment()
        {
            int returnedRows = 0;
            return db.ExecuteSQLStatement(query.CountInversionDepartmentSQL(), ref returnedRows);
        }

        /// <summary>
        /// Gets a DataSet object for the Inversion Count by College report.
        /// </summary>
        /// <returns>A DataSet containing a single table with columns for inverison types count and grouped by college.</returns>
        public DataSet CountInversionTypeByCollege()
        {
            int returnedRows = 0;
            return db.ExecuteSQLStatement(query.CountInversionCollegeSQL(), ref returnedRows);
        }

        /// <summary>
        /// Gets a DataSet object for the Inversion Cost by Department report.
        /// </summary>
        /// <returns>A DataSet containing a single table with columns for inversion types cost and grouped by deparment.</returns>
        public DataSet CostInversionTypeByDepartment()
        {
            int returnedRows = 0;
            return db.ExecuteSQLStatement(query.CostInversionDepartmentSQL(), ref returnedRows);
        }

        /// <summary>
        /// Gets a DataSet object for the Inversion Cost by College report.
        /// </summary>
        /// <returns>A DataSet containing a single table with columns for inversion types cost and grouped by college.</returns>
        public DataSet CostInversionTypeByCollege()
        {
            int returnedRows = 0;
            return db.ExecuteSQLStatement(query.CostInversionCollegeSQL(), ref returnedRows);
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

        public DataSet SummaryReport()
        {
            int returnedRows = 0;
            return db.ExecuteSQLStatement(query.numAndDolInv(), ref returnedRows);
        }

        public DataSet SummaryReportTotals()
        {
            int returnedRows = 0;
            return db.ExecuteSQLStatement(query.numAndDolInvTotals(), ref returnedRows);
        }
    }
}

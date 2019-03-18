using System;
using System.Data;
using System.IO;
using System.Data.OleDb;
using System.Reflection;

namespace SalaryInversion
{
    class DataAccess
    {
        /// <summary>
        /// Connection string to the database.
        /// </summary>
        private string connectionString;

        /// <summary>
        /// Constructor that sets the connection string to the database
        /// </summary>
        public DataAccess()
        {
            connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data source= " + Directory.GetCurrentDirectory() + "\\ReservationSystem.mdb";
        }

        /// <summary>
        /// This method takes an SQL statement that is passed in and executes it.  The resulting values
        /// are returned in a DataSet.  The number of rows returned from the query will be put into
        /// the reference parameter retVal.
        /// </summary>
        /// <param name="sqlStatement">The SQL statement to be executed.</param>
        /// <param name="retVal">Reference parameter that returns the number of selected rows.</param>
        /// <returns>Returns a DataSet that contains the data from the SQL statement.</returns>
        public DataSet ExecuteSQLStatement(string sqlStatement, ref int retVal)
        {
            try
            {
                //Create a new DataSet
                DataSet ds = new DataSet();

                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {

                        //Open the connection to the database
                        conn.Open();

                        //Add the information for the SelectCommand using the SQL statement and the connection object
                        adapter.SelectCommand = new OleDbCommand(sqlStatement, conn);
                        adapter.SelectCommand.CommandTimeout = 0;

                        //Fill up the DataSet with data
                        adapter.Fill(ds);
                    }
                }

                //Set the number of values returned
                retVal = ds.Tables[0].Rows.Count;

                //return the DataSet
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method takes an SQL statement that is passed in and executes it.  The resulting single 
        /// value is returned.
        /// </summary>
        /// <param name="sqlStatement">The SQL statement to be executed.</param>
        /// <returns>Returns a string from the scalar SQL statement.</returns>
        public string ExecuteScalarSQL(string sqlStatement)
        {
            try
            {
                //Holds the return value
                object obj;

                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                    {

                        //Open the connection to the database
                        conn.Open();

                        //Add the information for the SelectCommand using the SQL statement and the connection object
                        adapter.SelectCommand = new OleDbCommand(sqlStatement, conn);
                        adapter.SelectCommand.CommandTimeout = 0;

                        //Execute the scalar SQL statement
                        obj = adapter.SelectCommand.ExecuteScalar();
                    }
                }

                //See if the object is null
                if (obj == null)
                {
                    //Return a blank
                    return "";
                }
                else
                {
                    //Return the value
                    return obj.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This method takes an SQL statement that is a non query and executes it.
        /// </summary>
        /// <param name="sqlStatement">The SQL statement to be executed.</param>
        /// <returns>Returns the number of rows affected by the SQL statement.</returns>
        public int ExecuteNonQuery(string sqlStatement)
        {
            try
            {
                //Number of rows affected
                int numRows;

                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    //Open the connection to the database
                    conn.Open();

                    //Add the information for the SelectCommand using the SQL statement and the connection object
                    OleDbCommand cmd = new OleDbCommand(sqlStatement, conn);
                    cmd.CommandTimeout = 0;

                    //Execute the non query SQL statement
                    numRows = cmd.ExecuteNonQuery();
                }

                //return the number of rows affected
                return numRows;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}

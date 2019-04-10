using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Reflection;

/// <summary>
/// Class used to access the database.
/// </summary>
public class DataAccess
{
    /// <summary>
    /// Connection string to the database.
    /// </summary>
    private string ConnectionString;

    /// <summary>
    /// Constructor that sets the connection string to the database
    /// </summary>
    /// <param name="filePath">The path to an access database file.</param>
    public DataAccess(string filePath)
    {
        ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath;
    }

    /// <summary>
    /// This method takes an SQL statment that is passed in and executes it.  The resulting values
    /// are returned in a DataSet.  The number of rows returned from the query will be put into
    /// the reference parameter RetVal.
    /// </summary>
    /// <param name="SQL">The SQL statement to be executed.</param>
    /// <param name="RetVal">Reference parameter that returns the number of selected rows.</param>
    /// <returns>Returns a DataSet that contains the data from the SQL statement.</returns>
    public DataSet ExecuteSQLStatement(string SQL, ref int RetVal)
    {
        try
        {
            //Create a new DataSet
            DataSet ds = new DataSet();

            using (OleDbConnection conn = new OleDbConnection(ConnectionString))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                {

                    //Open the connection to the database
                    conn.Open();

                    //Add the information for the SelectCommand using the SQL statement and the connection object
                    adapter.SelectCommand = new OleDbCommand(SQL, conn);
                    adapter.SelectCommand.CommandTimeout = 0;

                    //Fill up the DataSet with data
                    adapter.Fill(ds);
                }
            }

            //Set the number of values returned
            RetVal = ds.Tables[0].Rows.Count;

            //return the DataSet
            return ds;
        }
        catch (Exception ex)
        {
            throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
        }
    }
}
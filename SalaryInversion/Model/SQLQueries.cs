using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryInversion
{
    class SQLQueries
    {
        #region SalaryAmountInverted

        //Inverted Profs

        public string getProfInstSalary()
        {
            return "SELECT t1.DEPT, t1.NAME, t2.maxInstrSal - t1.[9MSALARY] AS \"Inversion $ Amount\" " +
                    "FROM MAIN as t1 " +
                    "INNER JOIN " +
                    "(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxInstrSal " +
                    "FROM MAIN " +
                    "WHERE RNK = 'Instr' " +
                    "GROUP BY DEPT, RNK) AS t2 " +
                    "ON t1.DEPT = t2.DEPT " +
                    "WHERE t1.RNK = 'Prof' AND t1.[9MSALARY] < t2.maxInstrSal;";
        }

        public string getProfAsstSalary()
        {
            return "SELECT t1.DEPT, t1.NAME, t2.maxAsstSal - t1.[9MSALARY] AS \"Inversion $ Amount\" " +
                    "FROM MAIN as t1 " +
                    "INNER JOIN " +
                    "(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAsstSal " +
                    "FROM MAIN " +
                    "WHERE RNK = 'Asst' " +
                    "GROUP BY DEPT, RNK) AS t2 " +
                    "ON t1.DEPT = t2.DEPT " +
                    "WHERE t1.RNK = 'Prof' AND t1.[9MSALARY] < t2.maxAsstSal;";
        }

        public string getProfAssoSalary()
        {
            return "SELECT t1.DEPT, t1.NAME, t2.maxAssoSal - t1.[9MSALARY] AS \"Inversion $ Amount\" " +
                    "FROM MAIN as t1 " +
                    "INNER JOIN " +
                    "(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAssoSal " +
                    "FROM MAIN " +
                    "WHERE RNK = 'Asso' " +
                    "GROUP BY DEPT, RNK) AS t2 " +
                    "ON t1.DEPT = t2.DEPT " +
                    "WHERE t1.RNK = 'Prof' AND t1.[9MSALARY] < t2.maxAssoSal;";
        }

        //Inverted Asso

        public string getAssoInstSalary()
        {
            return "SELECT t1.DEPT, t1.NAME, t2.maxInstSal - t1.[9MSALARY] AS \"Inversion $ Amount\" " +
                    "FROM MAIN as t1 " +
                    "INNER JOIN " +
                    "(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxInstSal " +
                    "FROM MAIN " +
                    "WHERE RNK = 'Inst' " +
                    "GROUP BY DEPT, RNK) AS t2 " +
                    "ON t1.DEPT = t2.DEPT " +
                    "WHERE t1.RNK = 'Asso' AND t1.[9MSALARY] < t2.maxInstSal;";
        }

        public string getAssoAsstSalary()
        {
            return "SELECT t1.DEPT, t1.NAME, t2.maxAsstSal - t1.[9MSALARY] AS \"Inversion $ Amount\" " +
                    "FROM MAIN as t1 " +
                    "INNER JOIN " +
                    "(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAsstSal " +
                    "FROM MAIN " +
                    "WHERE RNK = 'Asst' " +
                    "GROUP BY DEPT, RNK) AS t2 " +
                    "ON t1.DEPT = t2.DEPT " +
                    "WHERE t1.RNK = 'Asso' AND t1.[9MSALARY] < t2.maxAsstSal;";
        }

        //Inverted Asst

        public string getAsstInstSalary()
        {
            return "SELECT t1.DEPT, t1.NAME, t2.maxInstSal - t1.[9MSALARY] AS \"Inversion $ Amount\" " +
                    "FROM MAIN as t1 " +
                    "INNER JOIN " +
                    "(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxInstSal " +
                    "FROM MAIN " +
                    "WHERE RNK = 'Inst' " +
                    "GROUP BY DEPT, RNK) AS t2 " +
                    "ON t1.DEPT = t2.DEPT " +
                    "WHERE t1.RNK = 'Asst' AND t1.[9MSALARY] < t2.maxInstSal;";
        }

        #endregion





    }
}

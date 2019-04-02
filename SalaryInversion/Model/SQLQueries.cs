using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryInversion
{
    class SQLQueries
    {

        #region Cost Report

        /// <summary>
        /// SQL query to generate the cost of inversion by department.
        /// </summary>
        /// <returns>A string with the SQL query.</returns>
        public string CostInversionSQL()
        {
            return "SELECT m.CLG, t.DEPT, FORMAT(SUM(FullInst),'$#,###,##0') AS [Full<Inst], FORMAT(SUM(FullAsst),'$#,###,##0') AS [Full<Asst], FORMAT(SUM(FullAsso),'$#,###,##0') AS [Full<Asso], FORMAT(SUM(AssoInst),'$#,###,##0') AS [Asso<Inst], " +
                "FORMAT(SUM(AssoAsst),'$#,###,##0') AS [Asso<Asst], FORMAT(SUM(AsstInst),'$#,###,##0') AS [Asst<Inst], FORMAT(SUM(FullInst) + SUM(FullAsst) + SUM(FullAsso) + SUM(AssoInst) + SUM(AssoAsst) + SUM(AsstInst),'$#,###,##0') AS Total " +
                "FROM(SELECT t1.DEPT AS DEPT, SUM(t2.maxInstrSal - t1.[9MSALARY]) AS FullInst, 0 AS FullAsst, 0 AS FullAsso, 0 AS AssoInst, 0 AS AssoAsst, 0 AS AsstInst " +
                "FROM MAIN as t1 " +
                "INNER JOIN " +
                "(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxInstrSal " +
                "FROM MAIN WHERE RNK = 'Instr' GROUP BY DEPT, RNK) AS t2 ON t1.DEPT = t2.DEPT WHERE t1.RNK = 'Prof' AND t1.[9MSALARY] < t2.maxInstrSal GROUP BY t1.DEPT " +
                "UNION " +
                "SELECT tb1.DEPT AS DEPT, 0 AS FullInst, SUM(tb1.MaxAsstSal - tb1.salary) AS FullAsst, 0 AS FullAsso, 0 AS AssoInst, 0 AS AssoAsst, 0 AS AsstInst " +
                "FROM (SELECT t1.DEPT AS DEPT, t1.NAME AS NAME, t1.[9MSALARY] AS salary, t2.maxAsstSal AS MaxAsstSal FROM MAIN as t1 INNER JOIN " +
                "(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAsstSal FROM MAIN WHERE RNK = 'Asst' GROUP BY DEPT, RNK) AS t2 ON t1.DEPT = t2.DEPT " +
                "WHERE t1.RNK = 'Prof' AND t1.[9MSALARY] < t2.maxAsstSal) AS tb1 " +
                "LEFT OUTER JOIN " +
                "(SELECT t1.DEPT, t1.NAME, t2.maxInstrSal - t1.[9MSALARY] AS \"Inversion $ Amount\" FROM MAIN as t1 " +
                "INNER JOIN (SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxInstrSal FROM MAIN WHERE RNK = 'Instr' GROUP BY DEPT, RNK) AS t2 ON t1.DEPT = t2.DEPT " +
                "WHERE t1.RNK = 'Prof' AND t1.[9MSALARY] < t2.maxInstrSal) AS tb2 ON tb1.DEPT = tb2.DEPT AND tb1.NAME = tb2.NAME WHERE tb2.NAME IS NULL " +
                "GROUP BY tb1.DEPT " +
                "UNION " +
                "SELECT table1.DEPT AS DEPT, 0 AS FullInst, 0 AS FullAsst, SUM(table1.MaxAssoSal - table1.salary) AS FullAsso, 0 AS AssoInst, 0 AS AssoAsst, 0 AS AsstInst " +
                "FROM (SELECT t1.DEPT, t1.NAME, t2.maxAssoSal AS MaxAssoSal, t1.[9MSALARY] AS salary FROM MAIN as t1 INNER JOIN " +
                "(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAssoSal FROM MAIN WHERE RNK = 'Asso' GROUP BY DEPT, RNK) AS t2 ON t1.DEPT = t2.DEPT " +
                "WHERE t1.RNK = 'Prof' AND t1.[9MSALARY] < t2.maxAssoSal) AS table1 LEFT OUTER JOIN (SELECT tb1.DEPT, tb1.NAME, tb1.MaxAsstSal - tb1.salary AS \"Inversion Amount\" " +
                "FROM (SELECT t1.DEPT AS DEPT, t1.NAME AS NAME, t1.[9MSALARY] AS salary, t2.maxAsstSal AS MaxAsstSal FROM MAIN as t1 INNER JOIN " +
                "(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAsstSal FROM MAIN WHERE RNK = 'Asst' GROUP BY DEPT, RNK) AS t2 ON t1.DEPT = t2.DEPT " +
                "WHERE t1.RNK = 'Prof' AND t1.[9MSALARY] < t2.maxAsstSal) AS tb1 LEFT OUTER JOIN " +
                "(SELECT t1.DEPT, t1.NAME AS NAME, t2.maxInstrSal - t1.[9MSALARY] AS \"Inversion $ Amount\" FROM MAIN as t1 INNER JOIN " +
                "(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxInstrSal FROM MAIN WHERE RNK = 'Instr' GROUP BY DEPT, RNK) AS t2 ON t1.DEPT = t2.DEPT " +
                "WHERE t1.RNK = 'Prof' AND t1.[9MSALARY] < t2.maxInstrSal) AS tb2 ON tb1.DEPT = tb2.DEPT AND tb1.NAME = tb2.NAME " +
                "WHERE tb2.NAME IS NULL) as table2 ON table1.DEPT = table2.DEPT AND table1.NAME = table2.NAME WHERE table2.NAME IS NULL " +
                "GROUP BY table1.DEPT UNION SELECT t1.DEPT AS DEPT, 0 AS FullInst, 0 AS FullAsst, 0 AS FullAsso, SUM(t2.maxInstSal - t1.[9MSALARY]) AS AssoInst, 0 AS AssoAsst, 0 AS AsstInst " +
                "FROM MAIN as t1 INNER JOIN (SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxInstSal FROM MAIN WHERE RNK = 'Inst' GROUP BY DEPT, RNK) AS t2 " +
                "ON t1.DEPT = t2.DEPT WHERE t1.RNK = 'Asso' AND t1.[9MSALARY] < t2.maxInstSal GROUP BY t1.DEPT UNION " +
                "SELECT tb1.DEPT AS DEPT, 0 AS FullInst, 0 AS FullAsst, 0 AS FullAsso, 0 AS AssoInst, SUM(tb1.maxAsstSal - tb1.salary) AS AssoAsst, 0 AS AsstInst " +
                "FROM (SELECT t1.DEPT AS DEPT, t1.NAME AS NAME, t1.[9MSALARY] AS salary, t2.maxAsstSal AS maxAsstSal FROM MAIN as t1 INNER JOIN " +
                "(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAsstSal FROM MAIN WHERE RNK = 'Asst' GROUP BY DEPT, RNK) AS t2 ON t1.DEPT = t2.DEPT " +
                "WHERE t1.RNK = 'Asso' AND t1.[9MSALARY] < t2.maxAsstSal) AS tb1 LEFT OUTER JOIN (SELECT t1.DEPT AS DEPT, t1.NAME FROM MAIN as t1 INNER JOIN " +
                "(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxInstSal FROM MAIN WHERE RNK = 'Inst' GROUP BY DEPT, RNK) AS t2 ON t1.DEPT = t2.DEPT " +
                "WHERE t1.RNK = 'Asso' AND t1.[9MSALARY] < t2.maxInstSal) AS tb2 ON tb1.DEPT = tb2.DEPT AND tb1.NAME = tb2.NAME WHERE tb2.NAME IS NULL " +
                "GROUP BY tb1.DEPT UNION SELECT t1.DEPT AS DEPT, 0 AS FullInst, 0 AS FullAsst, 0 AS FullAsso, 0 AS AssoInst, 0 AS AssoAsst, SUM(t2.maxInstSal - t1.[9MSALARY]) AS AsstInst " +
                "FROM MAIN as t1 INNER JOIN (SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxInstSal FROM MAIN WHERE RNK = 'Inst' GROUP BY DEPT, RNK) AS t2 ON t1.DEPT = t2.DEPT " +
                "WHERE t1.RNK = 'Asst' AND t1.[9MSALARY] < t2.maxInstSal GROUP BY t1.DEPT) AS t INNER JOIN MAIN AS m ON m.DEPT = t.DEPT GROUP BY t.DEPT, m.CLG";
        }

        /// <summary>
        /// SQL query to generate the cost of inversion by college.
        /// </summary>
        /// <returns>A string with the SQL query.</returns>
        public string CostInversionTotalSQL()
        {
            return "SELECT CLG, FORMAT(SUM([Full<Inst1]),'$#,###,##0') AS [Full<Inst], FORMAT(SUM([Full<Asst1]),'$#,###,##0') AS [Full<Asst], FORMAT(SUM([Full<Asso1]),'$#,###,##0') AS [Full<Asso], FORMAT(SUM([Asso<Inst1]),'$#,###,##0') AS [Asso<Inst], " +
                "FORMAT(SUM([Asso<Asst1]),'$#,###,##0') AS [Asso<Asst], FORMAT(SUM([Asst<Inst1]),'$#,###,##0') AS [Asst<Inst], FORMAT(SUM( Total1),'$#,###,##0') AS Total " +
                "FROM ( " +
                "SELECT m.CLG AS CLG, t.DEPT AS DEPT, FORMAT(SUM(FullInst),'$#,###,##0') AS [Full<Inst1], FORMAT(SUM(FullAsst),'$#,###,##0') AS [Full<Asst1], FORMAT(SUM(FullAsso),'$#,###,##0') AS [Full<Asso1], FORMAT(SUM(AssoInst),'$#,###,##0') AS [Asso<Inst1], " +
                "FORMAT(SUM(AssoAsst),'$#,###,##0') AS [Asso<Asst1], FORMAT(SUM(AsstInst),'$#,###,##0') AS [Asst<Inst1], FORMAT(SUM(FullInst) + SUM(FullAsst) + SUM(FullAsso) + SUM(AssoInst) + SUM(AssoAsst) + SUM(AsstInst),'$#,###,##0') AS Total1 " +
                "FROM(SELECT t1.DEPT AS DEPT, SUM(t2.maxInstrSal - t1.[9MSALARY]) AS FullInst, 0 AS FullAsst, 0 AS FullAsso, 0 AS AssoInst, 0 AS AssoAsst, 0 AS AsstInst " +
                "FROM MAIN as t1 " +
                "INNER JOIN " +
                "(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxInstrSal " +
                "FROM MAIN WHERE RNK = 'Instr' GROUP BY DEPT, RNK) AS t2 ON t1.DEPT = t2.DEPT WHERE t1.RNK = 'Prof' AND t1.[9MSALARY] < t2.maxInstrSal GROUP BY t1.DEPT " +
                "UNION " +
                "SELECT tb1.DEPT AS DEPT, 0 AS FullInst, SUM(tb1.MaxAsstSal - tb1.salary) AS FullAsst, 0 AS FullAsso, 0 AS AssoInst, 0 AS AssoAsst, 0 AS AsstInst " +
                "FROM (SELECT t1.DEPT AS DEPT, t1.NAME AS NAME, t1.[9MSALARY] AS salary, t2.maxAsstSal AS MaxAsstSal FROM MAIN as t1 INNER JOIN " +
                "(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAsstSal FROM MAIN WHERE RNK = 'Asst' GROUP BY DEPT, RNK) AS t2 ON t1.DEPT = t2.DEPT " +
                "WHERE t1.RNK = 'Prof' AND t1.[9MSALARY] < t2.maxAsstSal) AS tb1 " +
                "LEFT OUTER JOIN " +
                "(SELECT t1.DEPT, t1.NAME, t2.maxInstrSal - t1.[9MSALARY] AS \"Inversion $ Amount\" FROM MAIN as t1 " +
                "INNER JOIN (SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxInstrSal FROM MAIN WHERE RNK = 'Instr' GROUP BY DEPT, RNK) AS t2 ON t1.DEPT = t2.DEPT " +
                "WHERE t1.RNK = 'Prof' AND t1.[9MSALARY] < t2.maxInstrSal) AS tb2 ON tb1.DEPT = tb2.DEPT AND tb1.NAME = tb2.NAME WHERE tb2.NAME IS NULL " +
                "GROUP BY tb1.DEPT " +
                "UNION " +
                "SELECT table1.DEPT AS DEPT, 0 AS FullInst, 0 AS FullAsst, SUM(table1.MaxAssoSal - table1.salary) AS FullAsso, 0 AS AssoInst, 0 AS AssoAsst, 0 AS AsstInst " +
                "FROM (SELECT t1.DEPT, t1.NAME, t2.maxAssoSal AS MaxAssoSal, t1.[9MSALARY] AS salary FROM MAIN as t1 INNER JOIN " +
                "(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAssoSal FROM MAIN WHERE RNK = 'Asso' GROUP BY DEPT, RNK) AS t2 ON t1.DEPT = t2.DEPT " +
                "WHERE t1.RNK = 'Prof' AND t1.[9MSALARY] < t2.maxAssoSal) AS table1 LEFT OUTER JOIN (SELECT tb1.DEPT, tb1.NAME, tb1.MaxAsstSal - tb1.salary AS \"Inversion Amount\" " +
                "FROM (SELECT t1.DEPT AS DEPT, t1.NAME AS NAME, t1.[9MSALARY] AS salary, t2.maxAsstSal AS MaxAsstSal FROM MAIN as t1 INNER JOIN " +
                "(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAsstSal FROM MAIN WHERE RNK = 'Asst' GROUP BY DEPT, RNK) AS t2 ON t1.DEPT = t2.DEPT " +
                "WHERE t1.RNK = 'Prof' AND t1.[9MSALARY] < t2.maxAsstSal) AS tb1 LEFT OUTER JOIN " +
                "(SELECT t1.DEPT, t1.NAME AS NAME, t2.maxInstrSal - t1.[9MSALARY] AS \"Inversion $ Amount\" FROM MAIN as t1 INNER JOIN " +
                "(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxInstrSal FROM MAIN WHERE RNK = 'Instr' GROUP BY DEPT, RNK) AS t2 ON t1.DEPT = t2.DEPT " +
                "WHERE t1.RNK = 'Prof' AND t1.[9MSALARY] < t2.maxInstrSal) AS tb2 ON tb1.DEPT = tb2.DEPT AND tb1.NAME = tb2.NAME " +
                "WHERE tb2.NAME IS NULL) as table2 ON table1.DEPT = table2.DEPT AND table1.NAME = table2.NAME WHERE table2.NAME IS NULL " +
                "GROUP BY table1.DEPT UNION SELECT t1.DEPT AS DEPT, 0 AS FullInst, 0 AS FullAsst, 0 AS FullAsso, SUM(t2.maxInstSal - t1.[9MSALARY]) AS AssoInst, 0 AS AssoAsst, 0 AS AsstInst " +
                "FROM MAIN as t1 INNER JOIN (SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxInstSal FROM MAIN WHERE RNK = 'Inst' GROUP BY DEPT, RNK) AS t2 " +
                "ON t1.DEPT = t2.DEPT WHERE t1.RNK = 'Asso' AND t1.[9MSALARY] < t2.maxInstSal GROUP BY t1.DEPT UNION " +
                "SELECT tb1.DEPT AS DEPT, 0 AS FullInst, 0 AS FullAsst, 0 AS FullAsso, 0 AS AssoInst, SUM(tb1.maxAsstSal - tb1.salary) AS AssoAsst, 0 AS AsstInst " +
                "FROM (SELECT t1.DEPT AS DEPT, t1.NAME AS NAME, t1.[9MSALARY] AS salary, t2.maxAsstSal AS maxAsstSal FROM MAIN as t1 INNER JOIN " +
                "(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAsstSal FROM MAIN WHERE RNK = 'Asst' GROUP BY DEPT, RNK) AS t2 ON t1.DEPT = t2.DEPT " +
                "WHERE t1.RNK = 'Asso' AND t1.[9MSALARY] < t2.maxAsstSal) AS tb1 LEFT OUTER JOIN (SELECT t1.DEPT AS DEPT, t1.NAME FROM MAIN as t1 INNER JOIN " +
                "(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxInstSal FROM MAIN WHERE RNK = 'Inst' GROUP BY DEPT, RNK) AS t2 ON t1.DEPT = t2.DEPT " +
                "WHERE t1.RNK = 'Asso' AND t1.[9MSALARY] < t2.maxInstSal) AS tb2 ON tb1.DEPT = tb2.DEPT AND tb1.NAME = tb2.NAME WHERE tb2.NAME IS NULL " +
                "GROUP BY tb1.DEPT UNION SELECT t1.DEPT AS DEPT, 0 AS FullInst, 0 AS FullAsst, 0 AS FullAsso, 0 AS AssoInst, 0 AS AssoAsst, SUM(t2.maxInstSal - t1.[9MSALARY]) AS AsstInst " +
                "FROM MAIN as t1 INNER JOIN (SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxInstSal FROM MAIN WHERE RNK = 'Inst' GROUP BY DEPT, RNK) AS t2 ON t1.DEPT = t2.DEPT " +
                "WHERE t1.RNK = 'Asst' AND t1.[9MSALARY] < t2.maxInstSal GROUP BY t1.DEPT) AS t INNER JOIN MAIN AS m ON m.DEPT = t.DEPT GROUP BY t.DEPT, m.CLG " +
                " ) GROUP BY CLG";
        }
        #endregion
    }
}

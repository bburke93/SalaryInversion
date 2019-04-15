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
        /// SQL query to generate the cost of inversions by department.
        /// </summary>
        /// <returns>A string with the SQL query.</returns>
        public string CostInversionDepartmentSQL()
        {
            return "SELECT DISTINCT m.CLG AS College, " +
                "t.DEPT AS Department, " +
                "FORMAT(SUM(AsstInst),'$#,###,##0') AS [Asst<Inst], " +
                "FORMAT(SUM(AssoInst),'$#,###,##0') AS [Asso<Inst], " + 
                "FORMAT(SUM(FullInst),'$#,###,##0') AS [Full<Inst], " +
                "FORMAT(SUM(AssoAsst),'$#,###,##0') AS [Asso<Asst], " +
                "FORMAT(SUM(FullAsst),'$#,###,##0') AS [Full<Asst], " +
                "FORMAT(SUM(FullAsso),'$#,###,##0') AS [Full<Asso], " +
                "FORMAT(SUM(FullInst) + SUM(FullAsst) + SUM(FullAsso) + SUM(AssoInst) + SUM(AssoAsst) + SUM(AsstInst),'$#,###,##0') AS Total " +
                "FROM (SELECT DISTINCT CLG, DEPT FROM MAIN) as m LEFT JOIN " +
                "(SELECT t1.DEPT AS DEPT, SUM(t2.maxInstrSal - t1.[9MSALARY]) AS FullInst, 0 AS FullAsst, 0 AS FullAsso, 0 AS AssoInst, 0 AS AssoAsst, 0 AS AsstInst " +
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
                "FROM MAIN as t1 INNER JOIN (SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxInstSal FROM MAIN WHERE RNK = 'Instr' GROUP BY DEPT, RNK) AS t2 " +
                "ON t1.DEPT = t2.DEPT WHERE t1.RNK = 'Asso' AND t1.[9MSALARY] < t2.maxInstSal GROUP BY t1.DEPT UNION " +
                "SELECT tb1.DEPT AS DEPT, 0 AS FullInst, 0 AS FullAsst, 0 AS FullAsso, 0 AS AssoInst, SUM(tb1.maxAsstSal - tb1.salary) AS AssoAsst, 0 AS AsstInst " +
                "FROM (SELECT t1.DEPT AS DEPT, t1.NAME AS NAME, t1.[9MSALARY] AS salary, t2.maxAsstSal AS maxAsstSal FROM MAIN as t1 INNER JOIN " +
                "(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAsstSal FROM MAIN WHERE RNK = 'Asst' GROUP BY DEPT, RNK) AS t2 ON t1.DEPT = t2.DEPT " +
                "WHERE t1.RNK = 'Asso' AND t1.[9MSALARY] < t2.maxAsstSal) AS tb1 LEFT OUTER JOIN (SELECT t1.DEPT AS DEPT, t1.NAME FROM MAIN as t1 INNER JOIN " +
                "(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxInstSal FROM MAIN WHERE RNK = 'Instr' GROUP BY DEPT, RNK) AS t2 ON t1.DEPT = t2.DEPT " +
                "WHERE t1.RNK = 'Asso' AND t1.[9MSALARY] < t2.maxInstSal) AS tb2 ON tb1.DEPT = tb2.DEPT AND tb1.NAME = tb2.NAME WHERE tb2.NAME IS NULL " +
                "GROUP BY tb1.DEPT UNION SELECT t1.DEPT AS DEPT, 0 AS FullInst, 0 AS FullAsst, 0 AS FullAsso, 0 AS AssoInst, 0 AS AssoAsst, SUM(t2.maxInstSal - t1.[9MSALARY]) AS AsstInst " +
                "FROM MAIN as t1 INNER JOIN (SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxInstSal FROM MAIN WHERE RNK = 'Instr' GROUP BY DEPT, RNK) AS t2 ON t1.DEPT = t2.DEPT " +
                "WHERE t1.RNK = 'Asst' AND t1.[9MSALARY] < t2.maxInstSal GROUP BY t1.DEPT) AS t ON t.DEPT = m.DEPT WHERE t.DEPT IS NOT NULL GROUP BY t.DEPT, m.CLG";
        }

        /// <summary>
        /// SQL query to generate the cost of inversions by college.
        /// </summary>
        /// <returns>A string with the SQL query.</returns>
        public string CostInversionCollegeSQL()
        {
            return "SELECT DISTINCT m.CLG AS College, " +
                "FORMAT(SUM(AsstInst),'$#,###,##0') AS [Asst<Inst], " +
                "FORMAT(SUM(AssoInst),'$#,###,##0') AS [Asso<Inst], " +
                "FORMAT(SUM(FullInst),'$#,###,##0') AS [Full<Inst], " +
                "FORMAT(SUM(AssoAsst),'$#,###,##0') AS [Asso<Asst], " +
                "FORMAT(SUM(FullAsst),'$#,###,##0') AS [Full<Asst], " +
                "FORMAT(SUM(FullAsso),'$#,###,##0') AS [Full<Asso], " +
                "FORMAT(SUM(FullInst) + SUM(FullAsst) + SUM(FullAsso) + SUM(AssoInst) + SUM(AssoAsst) + SUM(AsstInst),'$#,###,##0') AS Total " +
                "FROM (SELECT DISTINCT CLG, DEPT FROM MAIN) as m LEFT JOIN " +
                "(SELECT t1.DEPT AS DEPT, SUM(t2.maxInstrSal - t1.[9MSALARY]) AS FullInst, 0 AS FullAsst, 0 AS FullAsso, 0 AS AssoInst, 0 AS AssoAsst, 0 AS AsstInst " +
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
                "FROM MAIN as t1 INNER JOIN (SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxInstSal FROM MAIN WHERE RNK = 'Instr' GROUP BY DEPT, RNK) AS t2 " +
                "ON t1.DEPT = t2.DEPT WHERE t1.RNK = 'Asso' AND t1.[9MSALARY] < t2.maxInstSal GROUP BY t1.DEPT UNION " +
                "SELECT tb1.DEPT AS DEPT, 0 AS FullInst, 0 AS FullAsst, 0 AS FullAsso, 0 AS AssoInst, SUM(tb1.maxAsstSal - tb1.salary) AS AssoAsst, 0 AS AsstInst " +
                "FROM (SELECT t1.DEPT AS DEPT, t1.NAME AS NAME, t1.[9MSALARY] AS salary, t2.maxAsstSal AS maxAsstSal FROM MAIN as t1 INNER JOIN " +
                "(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAsstSal FROM MAIN WHERE RNK = 'Asst' GROUP BY DEPT, RNK) AS t2 ON t1.DEPT = t2.DEPT " +
                "WHERE t1.RNK = 'Asso' AND t1.[9MSALARY] < t2.maxAsstSal) AS tb1 LEFT OUTER JOIN (SELECT t1.DEPT AS DEPT, t1.NAME FROM MAIN as t1 INNER JOIN " +
                "(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxInstSal FROM MAIN WHERE RNK = 'Instr' GROUP BY DEPT, RNK) AS t2 ON t1.DEPT = t2.DEPT " +
                "WHERE t1.RNK = 'Asso' AND t1.[9MSALARY] < t2.maxInstSal) AS tb2 ON tb1.DEPT = tb2.DEPT AND tb1.NAME = tb2.NAME WHERE tb2.NAME IS NULL " +
                "GROUP BY tb1.DEPT UNION SELECT t1.DEPT AS DEPT, 0 AS FullInst, 0 AS FullAsst, 0 AS FullAsso, 0 AS AssoInst, 0 AS AssoAsst, SUM(t2.maxInstSal - t1.[9MSALARY]) AS AsstInst " +
                "FROM MAIN as t1 INNER JOIN (SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxInstSal FROM MAIN WHERE RNK = 'Instr' GROUP BY DEPT, RNK) AS t2 ON t1.DEPT = t2.DEPT " +
                "WHERE t1.RNK = 'Asst' AND t1.[9MSALARY] < t2.maxInstSal GROUP BY t1.DEPT) AS t ON t.DEPT = m.DEPT WHERE t.DEPT IS NOT NULL GROUP BY m.CLG";
        }

        #endregion

        #region Inverted Employees

        /// <summary>
        /// SQL query to generate the inverted employees report.
        /// </summary>
        /// <returns>A string with the SQL query.</returns>
        public string InvertedEmployeesSQL()
        {
            // FORMAT(SUM(AsstInst),'$#,###,##0')
            return "SELECT t1.DEPT AS [Department], t1.NAME AS [Name], t1.RNK AS [Rank], " +
                "FORMAT(t1.[9MSALARY],'$#,###,##0') AS [Salary], " +
                "FORMAT(t2.MaxProfSal,'$#,###,##0') AS [Max Prof Salary], " +
                "FORMAT(t2.MaxAssoSal,'$#,###,##0') AS [Max Asso Salary], " +
                "FORMAT(t2.MaxAsstSal,'$#,###,##0') AS [Max Asst Salary], " +
                "FORMAT(t2.MaxInstrSal,'$#,###,##0') AS [Max Instr Salary] " +
                "FROM MAIN as t1 " +
                "INNER JOIN " +
                "(SELECT DEPT, SUM(MaxProfSal1) AS MaxProfSal, SUM(MaxAssoSal1) AS MaxAssoSal, " +
                "SUM(MaxAsstSal1) AS MaxAsstSal, SUM(MaxInstrSal1) AS MaxInstrSal " +
                "FROM " +
                "(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS MaxProfSal1, 0 AS MaxAssoSal1, 0 AS MaxAsstSal1, 0 AS MaxInstrSal1 " +
                "FROM MAIN " +
                "WHERE RNK = 'Prof' " +
                "GROUP BY DEPT, RNK " +
                "UNION " +
                "SELECT DEPT, RNK,  0 AS MaxProfSal1, MAX(MAIN.[9MSALARY]) AS MaxAssoSal1, 0 AS MaxAsstSal1, 0 AS MaxInstrSal1 " +
                "FROM MAIN " +
                "WHERE RNK = 'Asso' " +
                "GROUP BY DEPT, RNK " +
                "UNION " +
                "SELECT DEPT, RNK,  0 AS MaxProfSal1, 0 AS MaxAssoSal1, MAX(MAIN.[9MSALARY]) AS maxAsstSal1, 0 AS MaxInstrSal1 " +
                "FROM MAIN " +
                "WHERE RNK = 'Asst' " +
                "GROUP BY DEPT, RNK " +
                "UNION " +
                "SELECT DEPT, RNK,  0 AS MaxProfSal1, 0 AS MaxAssoSal1, 0 AS maxAsstSal1, MAX(MAIN.[9MSALARY]) AS MaxInstrSal1 " +
                "FROM MAIN " +
                "WHERE RNK = 'Instr' " +
                "GROUP BY DEPT, RNK) " +
                "GROUP BY DEPT) AS t2 " +
                "ON t1.DEPT = t2.DEPT " +
                "WHERE(RNK = 'Prof' AND[9MSALARY] <= MaxAssoSal) " +
                "OR(RNK = 'Prof' AND[9MSALARY] <= MaxAsstSal) " +
                "OR(RNK = 'Prof' AND[9MSALARY] <= MaxInstrSal) " +
                "OR(RNK = 'Asso' AND[9MSALARY] <= MaxAsstSal) " +
                "OR(RNK = 'Asso' AND[9MSALARY] <= MaxInstrSal) " +
                "OR(RNK = 'Asst' AND[9MSALARY] <= MaxInstrSal) " +
                "ORDER BY t1.DEPT, RNK";
        }

        #endregion

        #region Count Report

        /// <summary>
        /// SQL query to generate the count of inversions by department.
        /// </summary>
        /// <returns>A string with the SQL query.</returns>
        public string CountInversionDepartmentSQL()
        {
            return "SELECT DISTINCT t1.CLG AS College, t1.DEPT AS Department, " +
                "t2.[ASST < INSTR] AS [Asst<Inst], " +
                "t2.[ASSO < INSTR] AS [Asso<Inst], " +
                "t2.[FULL < INSTR] AS [Full<Inst], " +
                "t2.[ASSO < ASST] AS [Asso<Asst], " +
                "t2.[FULL < ASST] AS [Full<Asst], " +
                "t2.[FULL < ASSO] AS [Full<Asso],\n"
                + " t2.[ASST < INSTR] + t2.[ASSO < INSTR] + t2.[FULL < INSTR] + t2.[ASSO < ASST] + t2.[FULL < ASST] + t2.[FULL < ASSO] AS [Total] \n"
                + "FROM MAIN AS t1\n"
                + "\n"
                + "INNER JOIN\n"
                + "\n"
                + "(SELECT DEPT, SUM([ASST<INSTR1]) AS [ASST < INSTR], SUM([ASSO<INSTR1]) AS [ASSO < INSTR], SUM([FULL<INSTR1]) AS [FULL < INSTR], SUM([ASSO<ASST1]) AS [ASSO < ASST],\n"
                + "SUM([FULL<ASST1]) AS [FULL < ASST], SUM([FULL<ASSO1]) AS [FULL < ASSO]\n"
                + "\n"
                + "FROM\n"
                + "\n"
                + "(SELECT DEPT, COUNT(*) AS [ASST<INSTR1], 0 AS [ASSO<INSTR1], 0 AS [FULL<INSTR1], 0 AS [ASSO<ASST1], 0 AS [FULL<ASST1], 0 AS [FULL<ASSO1]\n"
                + "FROM (SELECT DEPT, NAME\n"
                + "FROM\n"
                + "(SELECT DEPT, NAME, InvTable.Rank, MIN(invRankNum) AS MinInvRank\n"
                + "	FROM \n"
                + "		(SELECT *\n"
                + "		FROM \n"
                + "			(SELECT ID, CLG, MAIN.DEPT, NAME, Main.RNK as Rank, Switch( MAIN.RNK='Prof', 4,\n"
                + "											 MAIN.RNK='Asso', 3,\n"
                + "											  MAIN.RNK='Asst', 2,\n"
                + "											 MAIN.RNK='Instr', 1,\n"
                + "										   true, 0) AS RankNum,\n"
                + "										   [9MSALARY], maxTable.RNK,\n"
                + "											Switch( maxTable.RNK='Prof', 4,\n"
                + "											 maxTable.RNK='Asso', 3,\n"
                + "											  maxTable.RNK='Asst', 2,\n"
                + "											 maxTable.RNK='Instr', 1,\n"
                + "										   true, 0) AS InvRankNum, maxRankSal\n"
                + "			FROM MAIN\n"
                + "			INNER JOIN\n"
                + "				(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxRankSal\n"
                + "				FROM MAIN\n"
                + "				WHERE RNK = 'Instr'\n"
                + "				GROUP BY DEPT, RNK\n"
                + "				UNION\n"
                + "				SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAssoSal\n"
                + "				FROM MAIN\n"
                + "				WHERE RNK = 'Asso'\n"
                + "				GROUP BY DEPT, RNK\n"
                + "				UNION\n"
                + "				SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAsstSal\n"
                + "				FROM MAIN\n"
                + "				WHERE RNK = 'Asst'\n"
                + "				GROUP BY DEPT, RNK) AS maxTable\n"
                + "			ON MAIN.DEPT = maxTable.DEPT\n"
                + "			WHERE MAIN.RNK <> 'Instr' AND MAIN.RNK <> maxTable.RNK AND MAIN.[9MSALARY] < maxRankSal\n"
                + "			ORDER BY MAIN.DEPT, MAIN.NAME) AS InvTable\n"
                + "	WHERE RankNum > InvRankNum)\n"
                + "	GROUP BY DEPT, NAME, InvTable.Rank)\n"
                + "WHERE InvTable.Rank = 'Asst' AND MinInvRank = 1)  AS [%$##@_Alias]\n"
                + "GROUP BY DEPT\n"
                + "\n"
                + "\n"
                + "UNION\n"
                + "\n"
                + "\n"
                + "SELECT DEPT, 0 AS [ASST<INSTR1], COUNT(*) AS [ASSO<INSTR1],  0 AS [FULL<INSTR1], 0 AS [ASSO<ASST1], 0 AS [FULL<ASST1], 0 AS [FULL<ASSO1]\n"
                + "FROM (SELECT DEPT, NAME\n"
                + "FROM\n"
                + "(SELECT DEPT, NAME, InvTable.Rank, MIN(invRankNum) AS MinInvRank\n"
                + "	FROM \n"
                + "		(SELECT *\n"
                + "		FROM \n"
                + "			(SELECT ID, CLG, MAIN.DEPT, NAME, Main.RNK as Rank, Switch( MAIN.RNK='Prof', 4,\n"
                + "											 MAIN.RNK='Asso', 3,\n"
                + "											  MAIN.RNK='Asst', 2,\n"
                + "											 MAIN.RNK='Instr', 1,\n"
                + "										   true, 0) AS RankNum,\n"
                + "										   [9MSALARY], maxTable.RNK,\n"
                + "											Switch( maxTable.RNK='Prof', 4,\n"
                + "											 maxTable.RNK='Asso', 3,\n"
                + "											  maxTable.RNK='Asst', 2,\n"
                + "											 maxTable.RNK='Instr', 1,\n"
                + "										   true, 0) AS InvRankNum, maxRankSal\n"
                + "			FROM MAIN\n"
                + "			INNER JOIN\n"
                + "				(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxRankSal\n"
                + "				FROM MAIN\n"
                + "				WHERE RNK = 'Instr'\n"
                + "				GROUP BY DEPT, RNK\n"
                + "				UNION\n"
                + "				SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAssoSal\n"
                + "				FROM MAIN\n"
                + "				WHERE RNK = 'Asso'\n"
                + "				GROUP BY DEPT, RNK\n"
                + "				UNION\n"
                + "				SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAsstSal\n"
                + "				FROM MAIN\n"
                + "				WHERE RNK = 'Asst'\n"
                + "				GROUP BY DEPT, RNK) AS maxTable\n"
                + "			ON MAIN.DEPT = maxTable.DEPT\n"
                + "			WHERE MAIN.RNK <> 'Instr' AND MAIN.RNK <> maxTable.RNK AND MAIN.[9MSALARY] < maxRankSal\n"
                + "			ORDER BY MAIN.DEPT, MAIN.NAME) AS InvTable\n"
                + "	WHERE RankNum > InvRankNum)\n"
                + "	GROUP BY DEPT, NAME, InvTable.Rank)\n"
                + "WHERE InvTable.Rank = 'Asso' AND MinInvRank = 1)  AS [%$##@_Alias]\n"
                + "GROUP BY DEPT\n"
                + "\n"
                + "\n"
                + "UNION\n"
                + "\n"
                + "\n"
                + "SELECT DEPT,  0 AS [ASST<INSTR1], 0 AS [ASSO<INSTR1], COUNT(*) AS [FULL<INSTR1], 0 AS [ASSO<ASST1], 0 AS [FULL<ASST1], 0 AS [FULL<ASSO1]\n"
                + "FROM (SELECT DEPT, NAME\n"
                + "FROM\n"
                + "(SELECT DEPT, NAME, InvTable.Rank, MIN(invRankNum) AS MinInvRank\n"
                + "	FROM \n"
                + "		(SELECT *\n"
                + "		FROM \n"
                + "			(SELECT ID, CLG, MAIN.DEPT, NAME, Main.RNK as Rank, Switch( MAIN.RNK='Prof', 4,\n"
                + "											 MAIN.RNK='Asso', 3,\n"
                + "											  MAIN.RNK='Asst', 2,\n"
                + "											 MAIN.RNK='Instr', 1,\n"
                + "										   true, 0) AS RankNum,\n"
                + "										   [9MSALARY], maxTable.RNK,\n"
                + "											Switch( maxTable.RNK='Prof', 4,\n"
                + "											 maxTable.RNK='Asso', 3,\n"
                + "											  maxTable.RNK='Asst', 2,\n"
                + "											 maxTable.RNK='Instr', 1,\n"
                + "										   true, 0) AS InvRankNum, maxRankSal\n"
                + "			FROM MAIN\n"
                + "			INNER JOIN\n"
                + "				(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxRankSal\n"
                + "				FROM MAIN\n"
                + "				WHERE RNK = 'Instr'\n"
                + "				GROUP BY DEPT, RNK\n"
                + "				UNION\n"
                + "				SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAssoSal\n"
                + "				FROM MAIN\n"
                + "				WHERE RNK = 'Asso'\n"
                + "				GROUP BY DEPT, RNK\n"
                + "				UNION\n"
                + "				SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAsstSal\n"
                + "				FROM MAIN\n"
                + "				WHERE RNK = 'Asst'\n"
                + "				GROUP BY DEPT, RNK) AS maxTable\n"
                + "			ON MAIN.DEPT = maxTable.DEPT\n"
                + "			WHERE MAIN.RNK <> 'Instr' AND MAIN.RNK <> maxTable.RNK AND MAIN.[9MSALARY] < maxRankSal\n"
                + "			ORDER BY MAIN.DEPT, MAIN.NAME) AS InvTable\n"
                + "	WHERE RankNum > InvRankNum)\n"
                + "	GROUP BY DEPT, NAME, InvTable.Rank)\n"
                + "WHERE InvTable.Rank = 'Prof' AND MinInvRank = 1)  AS [%$##@_Alias]\n"
                + "GROUP BY DEPT\n"
                + "\n"
                + "\n"
                + "UNION\n"
                + "\n"
                + "\n"
                + "SELECT DEPT, 0 AS [ASST<INSTR1], 0 AS [ASSO<INSTR1], 0 AS [FULL<INSTR1], COUNT(*) AS [ASSO<ASST1], 0 AS [FULL<ASST1], 0 AS [FULL<ASSO1]\n"
                + "FROM (SELECT DEPT, NAME\n"
                + "FROM\n"
                + "(SELECT DEPT, NAME, InvTable.Rank, MIN(invRankNum) AS MinInvRank\n"
                + "	FROM \n"
                + "		(SELECT *\n"
                + "		FROM \n"
                + "			(SELECT ID, CLG, MAIN.DEPT, NAME, Main.RNK as Rank, Switch( MAIN.RNK='Prof', 4,\n"
                + "											 MAIN.RNK='Asso', 3,\n"
                + "											  MAIN.RNK='Asst', 2,\n"
                + "											 MAIN.RNK='Instr', 1,\n"
                + "										   true, 0) AS RankNum,\n"
                + "										   [9MSALARY], maxTable.RNK,\n"
                + "											Switch( maxTable.RNK='Prof', 4,\n"
                + "											 maxTable.RNK='Asso', 3,\n"
                + "											  maxTable.RNK='Asst', 2,\n"
                + "											 maxTable.RNK='Instr', 1,\n"
                + "										   true, 0) AS InvRankNum, maxRankSal\n"
                + "			FROM MAIN\n"
                + "			INNER JOIN\n"
                + "				(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxRankSal\n"
                + "				FROM MAIN\n"
                + "				WHERE RNK = 'Instr'\n"
                + "				GROUP BY DEPT, RNK\n"
                + "				UNION\n"
                + "				SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAssoSal\n"
                + "				FROM MAIN\n"
                + "				WHERE RNK = 'Asso'\n"
                + "				GROUP BY DEPT, RNK\n"
                + "				UNION\n"
                + "				SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAsstSal\n"
                + "				FROM MAIN\n"
                + "				WHERE RNK = 'Asst'\n"
                + "				GROUP BY DEPT, RNK) AS maxTable\n"
                + "			ON MAIN.DEPT = maxTable.DEPT\n"
                + "			WHERE MAIN.RNK <> 'Instr' AND MAIN.RNK <> maxTable.RNK AND MAIN.[9MSALARY] < maxRankSal\n"
                + "			ORDER BY MAIN.DEPT, MAIN.NAME) AS InvTable\n"
                + "	WHERE RankNum > InvRankNum)\n"
                + "	GROUP BY DEPT, NAME, InvTable.Rank)\n"
                + "WHERE InvTable.Rank = 'Asso' AND MinInvRank = 2)  AS [%$##@_Alias]\n"
                + "GROUP BY DEPT\n"
                + "\n"
                + "\n"
                + "UNION\n"
                + "\n"
                + "\n"
                + "\n"
                + "SELECT DEPT, 0 AS [ASST<INSTR1], 0 AS [ASSO<INSTR1], 0 AS [FULL<INSTR1], 0 AS [ASSO<ASST1], COUNT(*) AS [FULL<ASST1], 0 AS [FULL<ASSO1]\n"
                + "FROM (SELECT DEPT, NAME\n"
                + "FROM\n"
                + "(SELECT DEPT, NAME, InvTable.Rank, MIN(invRankNum) AS MinInvRank\n"
                + "	FROM \n"
                + "		(SELECT *\n"
                + "		FROM \n"
                + "			(SELECT ID, CLG, MAIN.DEPT, NAME, Main.RNK as Rank, Switch( MAIN.RNK='Prof', 4,\n"
                + "											 MAIN.RNK='Asso', 3,\n"
                + "											  MAIN.RNK='Asst', 2,\n"
                + "											 MAIN.RNK='Instr', 1,\n"
                + "										   true, 0) AS RankNum,\n"
                + "										   [9MSALARY], maxTable.RNK,\n"
                + "											Switch( maxTable.RNK='Prof', 4,\n"
                + "											 maxTable.RNK='Asso', 3,\n"
                + "											  maxTable.RNK='Asst', 2,\n"
                + "											 maxTable.RNK='Instr', 1,\n"
                + "										   true, 0) AS InvRankNum, maxRankSal\n"
                + "			FROM MAIN\n"
                + "			INNER JOIN\n"
                + "				(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxRankSal\n"
                + "				FROM MAIN\n"
                + "				WHERE RNK = 'Instr'\n"
                + "				GROUP BY DEPT, RNK\n"
                + "				UNION\n"
                + "				SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAssoSal\n"
                + "				FROM MAIN\n"
                + "				WHERE RNK = 'Asso'\n"
                + "				GROUP BY DEPT, RNK\n"
                + "				UNION\n"
                + "				SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAsstSal\n"
                + "				FROM MAIN\n"
                + "				WHERE RNK = 'Asst'\n"
                + "				GROUP BY DEPT, RNK) AS maxTable\n"
                + "			ON MAIN.DEPT = maxTable.DEPT\n"
                + "			WHERE MAIN.RNK <> 'Instr' AND MAIN.RNK <> maxTable.RNK AND MAIN.[9MSALARY] < maxRankSal\n"
                + "			ORDER BY MAIN.DEPT, MAIN.NAME) AS InvTable\n"
                + "	WHERE RankNum > InvRankNum)\n"
                + "	GROUP BY DEPT, NAME, InvTable.Rank)\n"
                + "WHERE InvTable.Rank = 'Prof' AND MinInvRank = 2)  AS [%$##@_Alias]\n"
                + "GROUP BY DEPT\n"
                + "\n"
                + "\n"
                + "UNION\n"
                + "\n"
                + "\n"
                + "SELECT [%$##@_Alias].DEPT, 0 AS [ASST<INSTR1], 0 AS [ASSO<INSTR1], 0 AS [FULL<INSTR1], 0 AS [ASSO<ASST1], 0 AS [FULL<ASST1], COUNT(*) AS [FULL<ASSO1]\n"
                + "FROM (SELECT DEPT, NAME\n"
                + "FROM\n"
                + "(SELECT DEPT, NAME, InvTable.Rank, MIN(invRankNum) AS MinInvRank\n"
                + "	FROM \n"
                + "		(SELECT *\n"
                + "		FROM \n"
                + "			(SELECT ID, CLG, MAIN.DEPT, NAME, Main.RNK as Rank, Switch( MAIN.RNK='Prof', 4,\n"
                + "											 MAIN.RNK='Asso', 3,\n"
                + "											  MAIN.RNK='Asst', 2,\n"
                + "											 MAIN.RNK='Instr', 1,\n"
                + "										   true, 0) AS RankNum,\n"
                + "										   [9MSALARY], maxTable.RNK,\n"
                + "											Switch( maxTable.RNK='Prof', 4,\n"
                + "											 maxTable.RNK='Asso', 3,\n"
                + "											  maxTable.RNK='Asst', 2,\n"
                + "											 maxTable.RNK='Instr', 1,\n"
                + "										   true, 0) AS InvRankNum, maxRankSal\n"
                + "			FROM MAIN\n"
                + "			INNER JOIN\n"
                + "				(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxRankSal\n"
                + "				FROM MAIN\n"
                + "				WHERE RNK = 'Instr'\n"
                + "				GROUP BY DEPT, RNK\n"
                + "				UNION\n"
                + "				SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAssoSal\n"
                + "				FROM MAIN\n"
                + "				WHERE RNK = 'Asso'\n"
                + "				GROUP BY DEPT, RNK\n"
                + "				UNION\n"
                + "				SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAsstSal\n"
                + "				FROM MAIN\n"
                + "				WHERE RNK = 'Asst'\n"
                + "				GROUP BY DEPT, RNK) AS maxTable\n"
                + "			ON MAIN.DEPT = maxTable.DEPT\n"
                + "			WHERE MAIN.RNK <> 'Instr' AND MAIN.RNK <> maxTable.RNK AND MAIN.[9MSALARY] < maxRankSal\n"
                + "			ORDER BY MAIN.DEPT, MAIN.NAME) AS InvTable\n"
                + "	WHERE RankNum > InvRankNum)\n"
                + "	GROUP BY DEPT, NAME, InvTable.Rank)\n"
                + "WHERE InvTable.Rank = 'Prof' AND MinInvRank = 3)  AS [%$##@_Alias]\n"
                + "GROUP BY [%$##@_Alias].DEPT)\n"
                + "GROUP BY DEPT) AS t2\n"
                + "\n"
                + "\n"
                + "ON t1.DEPT = t2.DEPT\n"
                + "ORDER BY CLG;";
        }

        /// <summary>
        /// SQL query to generate the count of inversions by college.
        /// </summary>
        /// <returns>A string with the SQL query.</returns>
        public string CountInversionCollegeSQL()
        {
            return "SELECT CLG AS College, " +
                "SUM(ft.[ASST<INSTR]) AS [Asst<Inst], " +
                "SUM(ft.[ASSO<INSTR]) AS [Asso<Inst]," +
                "SUM(ft.[FULL<INSTR]) AS [Full<Inst], " +
                "SUM(ft.[ASSO<ASST]) AS [Asso<Asst], " +
                "SUM(ft.[FULL<ASST]) AS [Full<Asst], " +
                "SUM(ft.[FULL<ASSO]) AS [Full<Asso], " +
                "SUM(ft.[TOTAL]) AS [Total] \n"
               + "FROM\n"
               + "\n"
               + "(SELECT DISTINCT t1.CLG, t1.DEPT, t2.[ASST<INSTR], t2.[ASSO<INSTR], t2.[FULL<INSTR], t2.[ASSO<ASST], t2.[FULL<ASST], t2.[FULL<ASSO],\n"
               + " t2.[ASST<INSTR] + t2.[ASSO<INSTR] + t2.[FULL<INSTR] + t2.[ASSO<ASST] + t2.[FULL<ASST] + t2.[FULL<ASSO] AS [TOTAL] \n"
               + "FROM MAIN AS t1\n"
               + "\n"
               + "INNER JOIN\n"
               + "\n"
               + "(SELECT DEPT, SUM([ASST<INSTR1]) AS [ASST<INSTR], SUM([ASSO<INSTR1]) AS [ASSO<INSTR], SUM([FULL<INSTR1]) AS [FULL<INSTR], SUM([ASSO<ASST1]) AS [ASSO<ASST],\n"
               + "SUM([FULL<ASST1]) AS [FULL<ASST], SUM([FULL<ASSO1]) AS [FULL<ASSO]\n"
               + "\n"
               + "FROM\n"
               + "\n"
               + "(SELECT DEPT, COUNT(*) AS [ASST<INSTR1], 0 AS [ASSO<INSTR1], 0 AS [FULL<INSTR1], 0 AS [ASSO<ASST1], 0 AS [FULL<ASST1], 0 AS [FULL<ASSO1]\n"
               + "FROM (SELECT DEPT, NAME\n"
               + "FROM\n"
               + "(SELECT DEPT, NAME, InvTable.Rank, MIN(invRankNum) AS MinInvRank\n"
               + "	FROM \n"
               + "		(SELECT *\n"
               + "		FROM \n"
               + "			(SELECT ID, CLG, MAIN.DEPT, NAME, Main.RNK as Rank, Switch( MAIN.RNK='Prof', 4,\n"
               + "											 MAIN.RNK='Asso', 3,\n"
               + "											  MAIN.RNK='Asst', 2,\n"
               + "											 MAIN.RNK='Instr', 1,\n"
               + "										   true, 0) AS RankNum,\n"
               + "										   [9MSALARY], maxTable.RNK,\n"
               + "											Switch( maxTable.RNK='Prof', 4,\n"
               + "											 maxTable.RNK='Asso', 3,\n"
               + "											  maxTable.RNK='Asst', 2,\n"
               + "											 maxTable.RNK='Instr', 1,\n"
               + "										   true, 0) AS InvRankNum, maxRankSal\n"
               + "			FROM MAIN\n"
               + "			INNER JOIN\n"
               + "				(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxRankSal\n"
               + "				FROM MAIN\n"
               + "				WHERE RNK = 'Instr'\n"
               + "				GROUP BY DEPT, RNK\n"
               + "				UNION\n"
               + "				SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAssoSal\n"
               + "				FROM MAIN\n"
               + "				WHERE RNK = 'Asso'\n"
               + "				GROUP BY DEPT, RNK\n"
               + "				UNION\n"
               + "				SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAsstSal\n"
               + "				FROM MAIN\n"
               + "				WHERE RNK = 'Asst'\n"
               + "				GROUP BY DEPT, RNK) AS maxTable\n"
               + "			ON MAIN.DEPT = maxTable.DEPT\n"
               + "			WHERE MAIN.RNK <> 'Instr' AND MAIN.RNK <> maxTable.RNK AND MAIN.[9MSALARY] < maxRankSal\n"
               + "			ORDER BY MAIN.DEPT, MAIN.NAME) AS InvTable\n"
               + "	WHERE RankNum > InvRankNum)\n"
               + "	GROUP BY DEPT, NAME, InvTable.Rank)\n"
               + "WHERE InvTable.Rank = 'Asst' AND MinInvRank = 1)  AS [%$##@_Alias]\n"
               + "GROUP BY DEPT\n"
               + "\n"
               + "\n"
               + "UNION\n"
               + "\n"
               + "\n"
               + "SELECT DEPT, 0 AS [ASST<INSTR1], COUNT(*) AS [ASSO<INSTR1],  0 AS [FULL<INSTR1], 0 AS [ASSO<ASST1], 0 AS [FULL<ASST1], 0 AS [FULL<ASSO1]\n"
               + "FROM (SELECT DEPT, NAME\n"
               + "FROM\n"
               + "(SELECT DEPT, NAME, InvTable.Rank, MIN(invRankNum) AS MinInvRank\n"
               + "	FROM \n"
               + "		(SELECT *\n"
               + "		FROM \n"
               + "			(SELECT ID, CLG, MAIN.DEPT, NAME, Main.RNK as Rank, Switch( MAIN.RNK='Prof', 4,\n"
               + "											 MAIN.RNK='Asso', 3,\n"
               + "											  MAIN.RNK='Asst', 2,\n"
               + "											 MAIN.RNK='Instr', 1,\n"
               + "										   true, 0) AS RankNum,\n"
               + "										   [9MSALARY], maxTable.RNK,\n"
               + "											Switch( maxTable.RNK='Prof', 4,\n"
               + "											 maxTable.RNK='Asso', 3,\n"
               + "											  maxTable.RNK='Asst', 2,\n"
               + "											 maxTable.RNK='Instr', 1,\n"
               + "										   true, 0) AS InvRankNum, maxRankSal\n"
               + "			FROM MAIN\n"
               + "			INNER JOIN\n"
               + "				(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxRankSal\n"
               + "				FROM MAIN\n"
               + "				WHERE RNK = 'Instr'\n"
               + "				GROUP BY DEPT, RNK\n"
               + "				UNION\n"
               + "				SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAssoSal\n"
               + "				FROM MAIN\n"
               + "				WHERE RNK = 'Asso'\n"
               + "				GROUP BY DEPT, RNK\n"
               + "				UNION\n"
               + "				SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAsstSal\n"
               + "				FROM MAIN\n"
               + "				WHERE RNK = 'Asst'\n"
               + "				GROUP BY DEPT, RNK) AS maxTable\n"
               + "			ON MAIN.DEPT = maxTable.DEPT\n"
               + "			WHERE MAIN.RNK <> 'Instr' AND MAIN.RNK <> maxTable.RNK AND MAIN.[9MSALARY] < maxRankSal\n"
               + "			ORDER BY MAIN.DEPT, MAIN.NAME) AS InvTable\n"
               + "	WHERE RankNum > InvRankNum)\n"
               + "	GROUP BY DEPT, NAME, InvTable.Rank)\n"
               + "WHERE InvTable.Rank = 'Asso' AND MinInvRank = 1)  AS [%$##@_Alias]\n"
               + "GROUP BY DEPT\n"
               + "\n"
               + "\n"
               + "UNION\n"
               + "\n"
               + "\n"
               + "SELECT DEPT,  0 AS [ASST<INSTR1], 0 AS [ASSO<INSTR1], COUNT(*) AS [FULL<INSTR1], 0 AS [ASSO<ASST1], 0 AS [FULL<ASST1], 0 AS [FULL<ASSO1]\n"
               + "FROM (SELECT DEPT, NAME\n"
               + "FROM\n"
               + "(SELECT DEPT, NAME, InvTable.Rank, MIN(invRankNum) AS MinInvRank\n"
               + "	FROM \n"
               + "		(SELECT *\n"
               + "		FROM \n"
               + "			(SELECT ID, CLG, MAIN.DEPT, NAME, Main.RNK as Rank, Switch( MAIN.RNK='Prof', 4,\n"
               + "											 MAIN.RNK='Asso', 3,\n"
               + "											  MAIN.RNK='Asst', 2,\n"
               + "											 MAIN.RNK='Instr', 1,\n"
               + "										   true, 0) AS RankNum,\n"
               + "										   [9MSALARY], maxTable.RNK,\n"
               + "											Switch( maxTable.RNK='Prof', 4,\n"
               + "											 maxTable.RNK='Asso', 3,\n"
               + "											  maxTable.RNK='Asst', 2,\n"
               + "											 maxTable.RNK='Instr', 1,\n"
               + "										   true, 0) AS InvRankNum, maxRankSal\n"
               + "			FROM MAIN\n"
               + "			INNER JOIN\n"
               + "				(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxRankSal\n"
               + "				FROM MAIN\n"
               + "				WHERE RNK = 'Instr'\n"
               + "				GROUP BY DEPT, RNK\n"
               + "				UNION\n"
               + "				SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAssoSal\n"
               + "				FROM MAIN\n"
               + "				WHERE RNK = 'Asso'\n"
               + "				GROUP BY DEPT, RNK\n"
               + "				UNION\n"
               + "				SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAsstSal\n"
               + "				FROM MAIN\n"
               + "				WHERE RNK = 'Asst'\n"
               + "				GROUP BY DEPT, RNK) AS maxTable\n"
               + "			ON MAIN.DEPT = maxTable.DEPT\n"
               + "			WHERE MAIN.RNK <> 'Instr' AND MAIN.RNK <> maxTable.RNK AND MAIN.[9MSALARY] < maxRankSal\n"
               + "			ORDER BY MAIN.DEPT, MAIN.NAME) AS InvTable\n"
               + "	WHERE RankNum > InvRankNum)\n"
               + "	GROUP BY DEPT, NAME, InvTable.Rank)\n"
               + "WHERE InvTable.Rank = 'Prof' AND MinInvRank = 1)  AS [%$##@_Alias]\n"
               + "GROUP BY DEPT\n"
               + "\n"
               + "\n"
               + "UNION\n"
               + "\n"
               + "\n"
               + "SELECT DEPT, 0 AS [ASST<INSTR1], 0 AS [ASSO<INSTR1], 0 AS [FULL<INSTR1], COUNT(*) AS [ASSO<ASST1], 0 AS [FULL<ASST1], 0 AS [FULL<ASSO1]\n"
               + "FROM (SELECT DEPT, NAME\n"
               + "FROM\n"
               + "(SELECT DEPT, NAME, InvTable.Rank, MIN(invRankNum) AS MinInvRank\n"
               + "	FROM \n"
               + "		(SELECT *\n"
               + "		FROM \n"
               + "			(SELECT ID, CLG, MAIN.DEPT, NAME, Main.RNK as Rank, Switch( MAIN.RNK='Prof', 4,\n"
               + "											 MAIN.RNK='Asso', 3,\n"
               + "											  MAIN.RNK='Asst', 2,\n"
               + "											 MAIN.RNK='Instr', 1,\n"
               + "										   true, 0) AS RankNum,\n"
               + "										   [9MSALARY], maxTable.RNK,\n"
               + "											Switch( maxTable.RNK='Prof', 4,\n"
               + "											 maxTable.RNK='Asso', 3,\n"
               + "											  maxTable.RNK='Asst', 2,\n"
               + "											 maxTable.RNK='Instr', 1,\n"
               + "										   true, 0) AS InvRankNum, maxRankSal\n"
               + "			FROM MAIN\n"
               + "			INNER JOIN\n"
               + "				(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxRankSal\n"
               + "				FROM MAIN\n"
               + "				WHERE RNK = 'Instr'\n"
               + "				GROUP BY DEPT, RNK\n"
               + "				UNION\n"
               + "				SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAssoSal\n"
               + "				FROM MAIN\n"
               + "				WHERE RNK = 'Asso'\n"
               + "				GROUP BY DEPT, RNK\n"
               + "				UNION\n"
               + "				SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAsstSal\n"
               + "				FROM MAIN\n"
               + "				WHERE RNK = 'Asst'\n"
               + "				GROUP BY DEPT, RNK) AS maxTable\n"
               + "			ON MAIN.DEPT = maxTable.DEPT\n"
               + "			WHERE MAIN.RNK <> 'Instr' AND MAIN.RNK <> maxTable.RNK AND MAIN.[9MSALARY] < maxRankSal\n"
               + "			ORDER BY MAIN.DEPT, MAIN.NAME) AS InvTable\n"
               + "	WHERE RankNum > InvRankNum)\n"
               + "	GROUP BY DEPT, NAME, InvTable.Rank)\n"
               + "WHERE InvTable.Rank = 'Asso' AND MinInvRank = 2)  AS [%$##@_Alias]\n"
               + "GROUP BY DEPT\n"
               + "\n"
               + "\n"
               + "UNION\n"
               + "\n"
               + "\n"
               + "\n"
               + "SELECT DEPT, 0 AS [ASST<INSTR1], 0 AS [ASSO<INSTR1], 0 AS [FULL<INSTR1], 0 AS [ASSO<ASST1], COUNT(*) AS [FULL<ASST1], 0 AS [FULL<ASSO1]\n"
               + "FROM (SELECT DEPT, NAME\n"
               + "FROM\n"
               + "(SELECT DEPT, NAME, InvTable.Rank, MIN(invRankNum) AS MinInvRank\n"
               + "	FROM \n"
               + "		(SELECT *\n"
               + "		FROM \n"
               + "			(SELECT ID, CLG, MAIN.DEPT, NAME, Main.RNK as Rank, Switch( MAIN.RNK='Prof', 4,\n"
               + "											 MAIN.RNK='Asso', 3,\n"
               + "											  MAIN.RNK='Asst', 2,\n"
               + "											 MAIN.RNK='Instr', 1,\n"
               + "										   true, 0) AS RankNum,\n"
               + "										   [9MSALARY], maxTable.RNK,\n"
               + "											Switch( maxTable.RNK='Prof', 4,\n"
               + "											 maxTable.RNK='Asso', 3,\n"
               + "											  maxTable.RNK='Asst', 2,\n"
               + "											 maxTable.RNK='Instr', 1,\n"
               + "										   true, 0) AS InvRankNum, maxRankSal\n"
               + "			FROM MAIN\n"
               + "			INNER JOIN\n"
               + "				(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxRankSal\n"
               + "				FROM MAIN\n"
               + "				WHERE RNK = 'Instr'\n"
               + "				GROUP BY DEPT, RNK\n"
               + "				UNION\n"
               + "				SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAssoSal\n"
               + "				FROM MAIN\n"
               + "				WHERE RNK = 'Asso'\n"
               + "				GROUP BY DEPT, RNK\n"
               + "				UNION\n"
               + "				SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAsstSal\n"
               + "				FROM MAIN\n"
               + "				WHERE RNK = 'Asst'\n"
               + "				GROUP BY DEPT, RNK) AS maxTable\n"
               + "			ON MAIN.DEPT = maxTable.DEPT\n"
               + "			WHERE MAIN.RNK <> 'Instr' AND MAIN.RNK <> maxTable.RNK AND MAIN.[9MSALARY] < maxRankSal\n"
               + "			ORDER BY MAIN.DEPT, MAIN.NAME) AS InvTable\n"
               + "	WHERE RankNum > InvRankNum)\n"
               + "	GROUP BY DEPT, NAME, InvTable.Rank)\n"
               + "WHERE InvTable.Rank = 'Prof' AND MinInvRank = 2)  AS [%$##@_Alias]\n"
               + "GROUP BY DEPT\n"
               + "\n"
               + "\n"
               + "UNION\n"
               + "\n"
               + "\n"
               + "SELECT [%$##@_Alias].DEPT, 0 AS [ASST<INSTR1], 0 AS [ASSO<INSTR1], 0 AS [FULL<INSTR1], 0 AS [ASSO<ASST1], 0 AS [FULL<ASST1], COUNT(*) AS [FULL<ASSO1]\n"
               + "FROM (SELECT DEPT, NAME\n"
               + "FROM\n"
               + "(SELECT DEPT, NAME, InvTable.Rank, MIN(invRankNum) AS MinInvRank\n"
               + "	FROM \n"
               + "		(SELECT *\n"
               + "		FROM \n"
               + "			(SELECT ID, CLG, MAIN.DEPT, NAME, Main.RNK as Rank, Switch( MAIN.RNK='Prof', 4,\n"
               + "											 MAIN.RNK='Asso', 3,\n"
               + "											  MAIN.RNK='Asst', 2,\n"
               + "											 MAIN.RNK='Instr', 1,\n"
               + "										   true, 0) AS RankNum,\n"
               + "										   [9MSALARY], maxTable.RNK,\n"
               + "											Switch( maxTable.RNK='Prof', 4,\n"
               + "											 maxTable.RNK='Asso', 3,\n"
               + "											  maxTable.RNK='Asst', 2,\n"
               + "											 maxTable.RNK='Instr', 1,\n"
               + "										   true, 0) AS InvRankNum, maxRankSal\n"
               + "			FROM MAIN\n"
               + "			INNER JOIN\n"
               + "				(SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxRankSal\n"
               + "				FROM MAIN\n"
               + "				WHERE RNK = 'Instr'\n"
               + "				GROUP BY DEPT, RNK\n"
               + "				UNION\n"
               + "				SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAssoSal\n"
               + "				FROM MAIN\n"
               + "				WHERE RNK = 'Asso'\n"
               + "				GROUP BY DEPT, RNK\n"
               + "				UNION\n"
               + "				SELECT DEPT, RNK, MAX(MAIN.[9MSALARY]) AS maxAsstSal\n"
               + "				FROM MAIN\n"
               + "				WHERE RNK = 'Asst'\n"
               + "				GROUP BY DEPT, RNK) AS maxTable\n"
               + "			ON MAIN.DEPT = maxTable.DEPT\n"
               + "			WHERE MAIN.RNK <> 'Instr' AND MAIN.RNK <> maxTable.RNK AND MAIN.[9MSALARY] < maxRankSal\n"
               + "			ORDER BY MAIN.DEPT, MAIN.NAME) AS InvTable\n"
               + "	WHERE RankNum > InvRankNum)\n"
               + "	GROUP BY DEPT, NAME, InvTable.Rank)\n"
               + "WHERE InvTable.Rank = 'Prof' AND MinInvRank = 3)  AS [%$##@_Alias]\n"
               + "GROUP BY [%$##@_Alias].DEPT)\n"
               + "GROUP BY DEPT) AS t2\n"
               + "\n"
               + "\n"
               + "ON t1.DEPT = t2.DEPT\n"
               + "ORDER BY CLG) AS ft\n"
               + "\n"
               + "GROUP BY CLG;";
        }

        #endregion
    }
}

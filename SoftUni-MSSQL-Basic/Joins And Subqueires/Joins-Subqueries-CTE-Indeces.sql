USE SoftUni;

-- Ex. 01

SELECT TOP 5
       e.EmployeeID,
       e.JobTitle,
       e.AddressID,
       a.AddressText
  FROM Employees AS e
 INNER JOIN Addresses AS a
    ON a.AddressID = e.AddressID
 ORDER BY a.AddressID;

-- Ex. 02

SELECT TOP 50
       e.FirstName,
       e.LastName,
       t.Name AS Town,
       a.AddressText
  FROM Employees AS e
 INNER JOIN Addresses AS a
    ON a.AddressID = e.AddressID
 INNER JOIN Towns t
    ON t.TownID = a.TownID
 ORDER BY FirstName, LastName;

-- Ex. 03

SELECT e.EmployeeID,
       e.FirstName,
       e.LastName,
       d.Name AS DepartmentName
  FROM Employees AS e
 INNER JOIN Departments AS d
    ON d.DepartmentID = e.DepartmentID
 WHERE d.Name = 'Sales'
 ORDER BY EmployeeID;

-- Ex. 04

SELECT TOP 5
      e.EmployeeID,
      e.FirstName,
      e.Salary,
      d.Name AS DepartmentName
  FROM Employees AS e
 INNER JOIN Departments AS d
    ON d.DepartmentID = e.DepartmentID
 WHERE e.Salary > 15000
 ORDER BY d.DepartmentID;

-- Ex. 05

SELECT TOP 3
       e.EmployeeID,
       e.FirstName
  FROM Employees AS e
  LEFT OUTER JOIN EmployeesProjects AS ep
    ON ep.EmployeeID = e.EmployeeID
  LEFT OUTER JOIN Projects AS p
    ON p.ProjectID = ep.ProjectID
 WHERE ep.ProjectID IS NULL
 ORDER BY e.EmployeeID;

-- Ex. 06

SELECT e.FirstName,
       e.LastName,
       e.HireDate,
       d.Name AS DeptName
  FROM Employees AS e
 INNER JOIN Departments AS d
    ON d.DepartmentID = e.DepartmentID
 WHERE d.Name IN ('Sales', 'Finance')
   AND e.HireDate > '01/01/1999'
 ORDER BY e.HireDate;

-- Ex. 07

SELECT TOP 5
       e.EmployeeID,
       e.FirstName,
       p.Name AS ProjectName
  FROM Employees AS e
 INNER JOIN EmployeesProjects AS ep
    ON ep.EmployeeID = e.EmployeeID
 INNER JOIN Projects AS p
    ON p.ProjectID = ep.ProjectID
 WHERE p.StartDate > 13/08/2002 AND p.EndDate IS NULL
 ORDER BY e.EmployeeID;

-- Ex. 08

SELECT e.EmployeeID,
       e.FirstName,
  CASE
	    WHEN DATEPART (YEAR, p.StartDate) >= 2005 THEN NULL
	    ELSE p.Name
	END
  AS ProjectName
  FROM Employees AS e
 INNER JOIN EmployeesProjects AS ep
    ON ep.EmployeeID = e.EmployeeID
 INNER JOIN Projects AS p
    ON p.ProjectID = ep.ProjectID
 WHERE ep.EmployeeID = 24;

-- Ex. 09

SELECT e.EmployeeID,
       e.FirstName,
       e.ManagerID,
       m.FirstName AS ManagerName
  FROM Employees AS e
  JOIN Employees AS m
    ON e.ManagerID = m.EmployeeID
 WHERE e.ManagerID IN (3, 7)
 ORDER BY e.EmployeeID;

-- Ex. 10

SELECT TOP 50
       e.EmployeeID,
       e.FirstName + ' ' + e.LastName AS EmployeeName,
       m.FirstName + ' ' + m.LastName AS ManagerName,
       d.Name as DepartmentName
  FROM Employees AS e
 INNER JOIN Employees AS m
    ON m.EmployeeID = e.ManagerID
 INNER JOIN Departments AS d
    ON e.DepartmentID = d.DepartmentID
 ORDER BY e.EmployeeID;

-- Ex. 11

SELECT MIN(MinAverageSalary)
  FROM
  (
    SELECT DepartmentID,
      AVG(Salary) AS MinAverageSalary
      FROM Employees
     GROUP BY DepartmentID
  )
    AS MinAverageSalary;

USE Geography;

-- Ex. 12

SELECT mc.CountryCode,
       m.MountainRange,
       p.PeakName,
       p.Elevation
  FROM Peaks AS p
 INNER JOIN Mountains AS m
    ON p.MountainId = m.Id
 INNER JOIN MountainsCountries AS mc
    ON m.Id = mc.MountainId
 WHERE mc.CountryCode = 'BG' AND p.Elevation > 2835
 ORDER BY p.Elevation DESC;

-- Ex. 13

SELECT cc.CountryCode,
  COUNT(MountainRange) AS MountainRanges
  FROM
    (
      SELECT mc.CountryCode AS CountryCode,
       	     m.MountainRange AS MountainRange
        FROM MountainsCountries AS mc
       INNER JOIN Mountains AS m
          ON mc.MountainId = m.Id
       WHERE mc.CountryCode IN ('BG', 'US', 'RU')
    ) AS cc
    GROUP BY cc.CountryCode;

-- Ex. 14

SELECT TOP 5
       c.CountryName,
       r.RiverName
  FROM Countries AS c
 INNER JOIN Continents AS con
    ON con.ContinentCode = c.ContinentCode
 LEFT JOIN CountriesRivers AS cr
    ON cr.CountryCode = c.CountryCode
 LEFT JOIN Rivers AS r
    ON cr.RiverId = r.Id
  WHERE con.ContinentName = 'Africa'
ORDER BY CountryName ASC;

-- Ex. 15

SELECT ContinentCode, CurrencyCode, CurrencyUsage

  FROM (

  SELECT ContinentCode, CurrencyCode, CurrencyUsage,
    DENSE_RANK() OVER(PARTITION BY(ContinentCode) ORDER BY CurrencyUsage DESC) AS Rank
    FROM
      (
       SELECT ContinentCode,
              CurrencyCode,
              COUNT(CurrencyCode) AS CurrencyUsage
        FROM Countries
        GROUP BY CurrencyCode, ContinentCode
    ) AS RankedCurrencies ) AS Currencies
    WHERE Rank = 1 AND CurrencyUsage > 1
    ORDER BY ContinentCode;

-- Ex. 16

SELECT COUNT(c.CountryCode)
  FROM Countries AS c
  LEFT OUTER JOIN MountainsCountries AS mc
    ON c.CountryCode = mc.CountryCode
 WHERE mc.CountryCode IS NULL;

-- Ex. 17

SELECT TOP 5
     Sorted.CountryName,
	   MAX(Sorted.PeakElevation) AS HighestPeakElevation,
	   MAX(Sorted.RiverLength) AS LongestRiverLength
  FROM
    (
      SELECT
             c.CountryName,
             p.Elevation AS PeakElevation,
             r.Length AS RiverLength
        FROM Countries AS c
   LEFT JOIN MountainsCountries mc
          ON c.CountryCode = mc.CountryCode
   LEFT JOIN Mountains m
          ON mc.MountainId = m.Id
   LEFT JOIN Peaks AS p
          ON m.Id = p.MountainId
   LEFT JOIN CountriesRivers cr
          ON c.CountryCode = cr.CountryCode
   LEFT JOIN Rivers r
          ON cr.RiverId = r.Id
    ) AS Sorted

GROUP BY Sorted.CountryName
ORDER BY MAX(Sorted.PeakElevation) DESC,
	       MAX(Sorted.RiverLength) DESC,
		     Sorted.CountryName;
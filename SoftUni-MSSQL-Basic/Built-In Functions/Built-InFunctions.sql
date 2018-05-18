USE SoftUni;
GO
-- Ex. 01
SELECT FirstName, LastName
  FROM Employees
  WHERE FirstName LIKE 'SA%'

-- Ex. 02
SELECT FirstName, LastName
  FROM Employees
  WHERE LastName LIKE '%ei%';

-- Ex. 03
SELECT FirstName
  FROM Employees
  WHERE DepartmentID = 3
  OR DepartmentID = 10
  AND HireDate >= 1995
  OR HireDate <= 2005;

-- Ex. 04
SELECT FirstName, LastName
  FROM Employees
  WHERE JobTitle NOT LIKE '%engineer%';

-- Ex. 05
SELECT Name
  FROM Towns
  WHERE DATALENGTH(Name) = 5
  OR DATALENGTH(Name) = 6
  ORDER BY Name ASC;

-- Ex. 06
SELECT TownID, Name
  FROM Towns
  WHERE Name LIKE 'M%'
  OR Name LIKE 'K%'
  OR Name LIKE 'B%'
  OR Name LIKE 'E%'
  ORDER BY Name ASC;

-- Ex. 07
SELECT TownID, Name
  FROM Towns
  WHERE Name LIKE '[^RBD]%'
  ORDER BY Name ASC;

-- Ex. 08
CREATE VIEW V_EmployeesHiredAfter2000 AS
SELECT FirstName, LastName
  FROM Employees
  WHERE DATEPART(YEAR, HireDate) > 2000

SELECT * FROM V_EmployeesHiredAfter2000;

-- Ex. 09
SELECT FirstName, LastName
  FROM Employees
  WHERE DATALENGTH(LastName) = 5;

-- Ex.10
USE Geography;
GO

SELECT CountryName, IsoCode
  FROM Countries
  WHERE CountryName LIKE '%A%A%A%'
  ORDER BY IsoCode ASC;

-- Ex. 11
SELECT PeakName,RiverName, LOWER(PeakName + RIGHT(RiverName, LEN(RiverName) - 1))
  AS Mix
  FROM Peaks, Rivers
  WHERE RIGHT(PeakName,1) = LEFT(RiverName,1)
  ORDER BY Mix

USE Diablo;
GO

-- Ex. 12
SELECT TOP 50 Name, FORMAT(Start, 'yyyy-MM-dd') AS Start
  FROM Games
  WHERE YEAR(Start) BETWEEN 2011 AND 20112
  ORDER BY Start, Name;

-- Ex. 13
SELECT Username,
RIGHT(Email, LEN(Email) - CHARINDEX('@', Email, 1))
	  AS [Email Provider]
    FROM Users
    ORDER BY [Email Provider], Username

-- Ex. 14
SELECT Username, IpAddress
  FROM Users
  WHERE IpAddress LIKE '___.1%.%.___'
  ORDER BY Username;

-- Ex. 14

SELECT * FROM Games;

SELECT [Name] AS Game,
   CASE
         WHEN DATEPART(HOUR, Start) BETWEEN 0 AND 11 THEN 'Morning'
         WHEN DATEPART(HOUR, Start) BETWEEN 12 AND 17 THEN 'Afternoon'
         WHEN DATEPART(HOUR, Start) BETWEEN 18 AND 23 THEN 'Evening'
   END
    AS [Part of the Day],
  CASE
    WHEN Duration <= 3 THEN 'Extra Short'
    WHEN Duration BETWEEN 4 AND 6 THEN 'Short'
    WHEN Duration > 6 THEN 'Long'
    ELSE 'Extra Long'
  END
    AS Duration
  FROM Games
  ORDER BY [Name], [Duration], [Part of the Day]

-- Ex. 16

USE Orders;

SELECT ProductName,
       OrderDate,
       DATEADD(DAY, 3, OrderDate) AS [PayDue],
       DATEADD(MONTH, 1, OrderDate) AS [Deliver Due]
  FROM Orders;
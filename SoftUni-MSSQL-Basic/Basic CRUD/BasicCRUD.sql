-- Ex. 01
USE SoftUni;

-- Ex. 02
SELECT * FROM Departments;

-- Ex. 03
SELECT Name
FROM Departments;

-- Ex. 04
SELECT FirstName, LastName, Salary FROM Employees

-- Ex. 05
SELECT FirstName, MiddleName, LastName
FROM Employees;

-- Ex. 06
SELECT FirstName + '.'  + LastName + '@softuni.bg'
AS [Full Email Address]
FROM Employees;

-- Ex. 07
SELECT DISTINCT Salary FROM Employees;

-- Ex. 08
SELECT FirstName, LastName, JobTitle
FROM Employees
WHERE Salary >= 20000 AND Salary <= 30000;

-- Ex. 09
SELECT FirstName + ' ' + MiddleName + ' ' + LastName
AS [Full Name]
FROM Employees
WHERE Salary = 25000 OR Salary = 14000 OR Salary = 12500 OR Salary = 23600;

-- Ex. 10
SELECT FirstName, LastName
FROM Employees
WHERE ManagerID IS NULL;

-- Ex. 11
SELECT FirstName, LastName, Salary
FROM Employees
WHERE Salary > 50000
ORDER BY Salary DESC;

-- Ex. 12
SELECT TOP 5 FirstName, LastName
FROM Employees
ORDER BY Salary DESC;

-- Ex. 13
SELECT FirstName, LastName
FROM Employees
WHERE DepartmentID != 4;

-- Ex. 14
SELECT * FROM Employees
ORDER BY Salary DESC, FirstName ASC, LastName DESC, MiddleName ASC;


-- Ex. 15
CREATE VIEW V_EmployeesSalaries AS
SELECT FirstName, LastName, Salary
FROM Employees;


SELECT * FROM V_EmployeesSalaries;

-- Ex. 16
CREATE VIEW V_EmployeeNameJobTitle AS
SELECT FirstName + ' ' + ISNULL(MiddleName, '') + ' ' + LastName AS [Full Name], JobTitle AS [Job Title]
FROM Employees;

SELECT * FROM V_EmployeeNameJobTitle;

-- Ex. 17
SELECT DISTINCT JobTitle FROM Employees;

-- Ex. 18
SELECT TOP 10 * FROM Projects ORDER BY StartDate, Name;

-- Ex. 19
SELECT TOP 7 FirstName, LastName, HireDate
FROM Employees
ORDER BY HireDate DESC;

-- Ex. 20
UPDATE Employees
SET Salary *= 1.12
WHERE DepartmentID = 1 OR DepartmentID = 2 OR DepartmentID = 4 OR DepartmentID = 11;

SELECT Salary
FROM Employees;

USE Geography;
GO;

-- Ex. 21
SELECT PeakName
FROM Peaks
ORDER BY PeakName;

-- Ex. 22
SELECT TOP 30 CountryName, Population FROM Countries
WHERE ContinentCode = 'EU'
ORDER BY Population DESC, CountryName ASC;

-- Ex. 23
SELECT CountryName,
	     CountryCode,
	   (CASE
			WHEN CurrencyCode = 'EUR'
			THEN 'Euro'
			ELSE 'Not Euro'
			END)
		  AS [Currency]
	    FROM Countries
 ORDER BY CountryName;

USE Diablo;

-- Ex. 24
SELECT Name FROM Characters ORDER BY Name;
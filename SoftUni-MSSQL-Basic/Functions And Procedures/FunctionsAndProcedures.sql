USE SoftUni;

-- Ex. 01

CREATE PROC dbo.usp_GetEmployeesSalaryAbove35000
AS
  SELECT FirstName, LastName
    FROM Employees
   WHERE Salary > 35000;

EXEC dbo.usp_GetEmployeesSalaryAbove35000;

-- Ex. 02

CREATE PROC dbo.usp_GetEmployeesSalaryAboveNumber(@salaryToCompareWith DECIMAL(18, 4))
AS
  SELECT FirstName, LastName
    FROM Employees
   WHERE Salary >= @salaryToCompareWith;

EXEC dbo.usp_GetEmployeesSalaryAboveNumber 48100.00;

-- Ex. 03

CREATE PROC dbo.usp_GetTownsStartingWith(@queryLetter VARCHAR(10))
AS
  SELECT Name
    FROM Towns
   WHERE LEFT(Name, LEN(@queryLetter)) LIKE @queryLetter;

EXEC dbo.usp_GetTownsStartingWith 'sa';

-- Ex. 04

CREATE PROC dbo.usp_GetEmployeesFromTown(@townName VARCHAR(50))
AS
  SELECT e.FirstName,
         e.LastName
    FROM Employees AS e
   INNER JOIN Addresses AS a
      ON e.AddressID = a.AddressID
   INNER JOIN Towns AS t
      ON a.TownID = t.TownID
   WHERE t.Name LIKE @townName;

EXEC dbo.usp_GetEmployeesFromTown Sofia;

-- Ex. 05

CREATE FUNCTION dbo.ufn_GetSalaryLevel(@salary DECIMAL(18,4))
RETURNS NVARCHAR(10)
AS
BEGIN
  DECLARE @SalaryLevel VARCHAR(10)
  IF(@salary < 30000)
    SET @SalaryLevel = 'Low'
  ELSE IF(@salary BETWEEN 30000 AND 50000)
    SET @SalaryLevel = 'Average'
  ELSE
  SET @SalaryLevel = 'High'
  RETURN @SalaryLevel
END


SELECT Salary, dbo.ufn_GetSalaryLevel(Salary) AS SalaryLevel
  FROM Employees
 ORDER BY Salary DESC;

-- Ex. 06

CREATE PROC dbo.usp_EmployeesBySalaryLevel(@salaryLevel NVARCHAR(10))
AS
  SELECT FirstName AS [First Name],
         LastName AS [Last Name]
    FROM Employees
    WHERE dbo.ufn_GetSalaryLevel(Salary) = @salaryLevel;

EXEC dbo.usp_EmployeesBySalaryLevel 'High';

-- Ex. 07

CREATE FUNCTION ufn_IsWordComprised(@setOfLetters VARCHAR(max), @word VARCHAR(max))
RETURNS BIT
  AS
  BEGIN
    DECLARE @isComprised BIT = 0;
    DECLARE @currentIndex INT = 1;
    DECLARE @currentChar CHAR;

    WHILE(@currentIndex <= LEN(@word))
    BEGIN

      SET @currentChar = SUBSTRING(@word, @currentIndex, 1);
      IF(CHARINDEX(@currentChar, @setOfLetters) = 0)
        RETURN @isComprised;
      SET @currentIndex += 1;
    END
    RETURN @isComprised + 1;
  END

SELECT dbo.ufn_IsWordComprised('oistmiahf', 'Sofia')

SELECT dbo.ufn_IsWordComprised('oistmiahf', 'halves')

-- Ex. 09

USE Bank;

CREATE PROC dbo.usp_GetHoldersFullName
AS
  SELECT FirstName + ' ' + LastName
  FROM AccountHolders;

EXEC dbo.usp_GetHoldersFullName;

-- Ex. 10

CREATE PROC dbo.usp_GetHoldersWithBalanceHigherThan(@amoumtComparator MONEY)
AS
  SELECT ah.FirstName AS [First Name],
         ah.LastName AS [Last Name]
    FROM AccountHolders AS ah
   INNER JOIN Accounts AS acc
      ON ah.Id = acc.AccountHolderId
    GROUP BY ah.FirstName, ah.LastName
    HAVING SUM(acc.Balance) > @amoumtComparator;


EXEC dbo.usp_GetHoldersWithBalanceHigherThan 12345.67;

-- Ex. 11

CREATE FUNCTION dbo.ufn_CalculateFutureValue(@sum MONEY, @yearlyInterestRate FLOAT, @years INT)
RETURNS MONEY
AS
  BEGIN
    
  END


SELECT dbo.ufn_CalculateFutureValue(1000, 0.1, 5) AS FutureValue;

-- Ex. 12

CREATE PROC dbo.usp_CalculateFutureValueForAccount(@accountId INT, @interestRate FLOAT)
AS
  BEGIN
    DECLARE @years INT = 5;

      SELECT ah.Id AS [Account Id],
             ah.FirstName AS [First Name],
             ah.LastName AS [Last Name],
             acc.Balance AS [Current Balance],
              dbo.ufn_CalculateFutureValue(acc.Balance, @interestRate, @years) AS [Balance in 5 years]
        FROM AccountHolders AS ah
       INNER JOIN Accounts AS acc
          ON ah.Id = acc.AccountHolderId
       WHERE acc.Id = @accountId;
  END

EXEC dbo.usp_CalculateFutureValueForAccount 1, 0.1;


SELECT * FROM WizzardDeposits;

-- Ex. 01

SELECT COUNT(*) FROM WizzardDeposits;

-- Ex. 02

SELECT MAX(MagicWandSize)
		AS [LongestMagicWand]
	FROM WizzardDeposits;

-- Ex.03

SELECT DepositGroup,
	MAX(MagicWandSize) AS [LongestMagicWand]
	FROM WizzardDeposits
	GROUP BY DepositGroup;

-- Ex. 04

SELECT TOP 2 DepositGroup
	FROM WizzardDeposits
	GROUP BY DepositGroup
	ORDER BY AVG(MagicWandSize);

-- Ex. 05

SELECT DepositGroup,
	SUM(DepositAmount) AS TotalSum
	FROM WizzardDeposits
	GROUP BY DepositGroup;

-- Ex. 06

SELECT DepositGroup,
	SUM(DepositAmount) AS TotalSum
	FROM WizzardDeposits
	WHERE MagicWandCreator = 'Ollivander family'
	GROUP BY DepositGroup;

-- Ex. 07

SELECT DepositGroup,
	SUM(DepositAmount) AS TotalSum
	FROM WizzardDeposits
	WHERE MagicWandCreator = 'Ollivander family'
	GROUP BY DepositGroup
	HAVING SUM(DepositAmount) < 150000
	ORDER BY TotalSum DESC;

-- Ex. 08

SELECT DepositGroup, MagicWandCreator,
	MIN(DepositCharge) AS MinDepositCharge
	FROM WizzardDeposits
	GROUP BY DepositGroup, MagicWandCreator
	ORDER BY MagicWandCreator, DepositGroup;

-- Ex. 09

SELECT
	CASE
			WHEN Age BETWEEN 0 AND 10 THEN '[0-10]'
			WHEN Age BETWEEN 11 AND 20 THEN '[11-20]'
			WHEN Age BETWEEN 21 AND 30 THEN '[21-30]'
			WHEN Age BETWEEN 31 AND 40 THEN '[31-40]'
			WHEN Age BETWEEN 41 AND 50 THEN '[41-50]'
			WHEN Age BETWEEN 51 AND 60 THEN '[51-60]'
			WHEN Age > 60 THEN '[61+]'
	END AS AgeGroup
	FROM WizzardDeposits) AS AgeGroups
GROUP BY AgeGroups.AgeGroup;

-- Ex. 10

SELECT DISTINCT LEFT(FirstName, 1) AS FirstLetter
	FROM WizzardDeposits
	WHERE DepositGroup = 'Troll Chest'
	GROUP BY LEFT(FirstName, 1)
	ORDER BY FirstLetter;

-- Ex. 11

SELECT DepositGroup, IsDepositExpired,
	AVG(DepositInterest) AS AverageInterest
	FROM WizzardDeposits
	WHERE DepositStartDate > '01-01-1985'
	GROUP BY DepositGroup, IsDepositExpired
	ORDER BY DepositGroup DESC, IsDepositExpired ASC;

-- Ex. 12

SELECT SUM(WizzDeposit.Difference) AS SumDifference
	FROM(
	SELECT FirstName AS HostWizard,
			 	 DepositAmount AS [Host Wizard Deposit],
				 LEAD(FirstName) OVER(ORDER BY Id) AS [Guest Wizard],
				 LEAD(DepositAmount) OVER(ORDER BY Id) AS [Guest Wizard Deposit],
				 DepositAmount - LEAD(DepositAmount) OVER(ORDER BY Id) AS [Difference]
		FROM WizzardDeposits) AS WizzDeposit;

USE SoftUni;

-- Ex. 13

SELECT DepartmentID,
	SUM(Salary) AS TotalSalary
	FROM Employees
	GROUP BY DepartmentID
	ORDER BY DepartmentID;

-- Ex. 14

SELECT DepartmentID,
	MIN(Salary) AS MinimumSalary
	FROM Employees
	WHERE DepartmentID = 2
		 OR DepartmentID = 5
		 OR DepartmentID = 7
		AND HireDate > '01/01/2000'
	GROUP BY DepartmentID;

-- Ex. 15

SELECT * INTO TempTable
	FROM Employees
	WHERE Salary > 30000;

	DELETE FROM TempTable WHERE ManagerID = 42;

UPDATE TempTable
	 SET Salary += 5000
 WHERE DepartmentID = 1;

SELECT DepartmentID,
	AVG(Salary) AS AverageSalary
	FROM TempTable
	GROUP BY DepartmentID;

-- Ex. 16

SELECT DepartmentID, MAX(Salary) AS MaxSalary
	FROM Employees
	GROUP BY DepartmentID
	HAVING MAX(Salary) NOT BETWEEN 30000 AND 70000;


-- Ex. 17

SELECT COUNT(*) AS Count
	FROM Employees
	WHERE ManagerID IS NULL;


-- Ex. 18

SELECT DepartmentID,
			 Salary
	FROM
	(
		SELECT DepartmentID,
					 Salary,
					 DENSE_RANK() OVER(PARTITION BY DepartmentID ORDER BY Salary DESC) AS Rank
			FROM Employees
			GROUP BY DepartmentID,
							 Salary
	) 	AS RankedSalaries
	WHERE Rank = 3;


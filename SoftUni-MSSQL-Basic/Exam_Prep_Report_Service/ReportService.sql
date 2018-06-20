-- Ex. 01 DDL

CREATE TABLE Users (
  Id INT IDENTITY NOT NULL,
  Username NVARCHAR(30) NOT NULL UNIQUE,
  Password NVARCHAR(50) NOT NULL,
  Name NVARCHAR(50),
  Gender CHAR(1),
  Birthdate DATETIME,
  Age INT,
  Email NVARCHAR(50) NOT NULL,
  CONSTRAINT PK_Users PRIMARY KEY(Id),
  CONSTRAINT CH_Users_Gender CHECK (Gender IN ('F', 'M'))

)

CREATE TABLE Departments (
  Id INT IDENTITY NOT NULL,
  Name NVARCHAR(50) NOT NULL,
  CONSTRAINT PK_Departments PRIMARY KEY(Id)
)

CREATE TABLE Employees (
  Id INT IDENTITY NOT NULL,
  FirstName NVARCHAR(25),
  LastName NVARCHAR(25),
  Gender CHAR(1),
  Birthdate DATETIME,
  Age INT,
  DepartmentId INT NOT NULL,
  CONSTRAINT PK_Employees PRIMARY KEY(Id),
  CONSTRAINT FK_Employees_Departments FOREIGN KEY(DepartmentId) REFERENCES Departments(Id),
  CONSTRAINT CH_Employees_Gender CHECK(Gender IN('M', 'F'))
)

CREATE TABLE Categories (
  Id INT IDENTITY NOT NULL,
  Name NVARCHAR(50) NOT NULL,
  DepartmentId INT NOT NULL,
  CONSTRAINT PK_Categories PRIMARY KEY(Id),
  CONSTRAINT FK_Categories_Departments FOREIGN KEY(DepartmentId) REFERENCES Departments(Id)
)

CREATE TABLE Status (
  Id INT IDENTITY NOT NULL,
  Label NVARCHAR(30) NOT NULL,
  CONSTRAINT PK_Status PRIMARY KEY(Id)
)

CREATE TABLE Reports (
  Id INT IDENTITY NOT NULL,
  CategoryId INT NOT NULL,
  StatusId INT NOT NULL,
  OpenDate DATETIME NOT NULL,
  CloseDate DATETIME,
  Description NVARCHAR(200),
  UserId INT NOT NULL,
  EmployeeId INT,
  CONSTRAINT PK_Reports PRIMARY KEY(Id),
  CONSTRAINT FK_Reports_Categories FOREIGN KEY(CategoryId) REFERENCES Categories(Id),
  CONSTRAINT FK_Reports_Employees FOREIGN KEY(EmployeeId) REFERENCES Employees(Id),
  CONSTRAINT FK_Reports_Status FOREIGN KEY(StatusId) REFERENCES Status(Id),
  CONSTRAINT FK_Reports_Users FOREIGN KEY(UserId) REFERENCES Users(Id)
)

-- 30/30

-- Ex. 02 DML

INSERT INTO Employees (FirstName, LastName, Gender, Birthdate, DepartmentId) VALUES
  ('Marlo', 'O’Malley', 'M', '9/21/1958', 1),
  ('Niki', 'Stanaghan', 'F', '11/26/1969', 4),
  ('Ayrton', 'Senna', 'M', '03/21/1960', 9),
  ('Ronnie', 'Peterson', 'M', '02/14/1944', 9),
  ('Giovanna', 'Amati', 'F', '07/20/1959', 5)

INSERT INTO Reports (CategoryId, StatusId, OpenDate, CloseDate, Description, UserId, EmployeeId) VALUES
  (1, 1, '04/13/2017', NULL, 'Stuck Road on Str.133', 6, 2),
  (6, 3, '09/05/2015', '12/06/2015', 'Charity trail running', 3, 5),
  (14, 2, '09/07/2015', NULL, 'Falling bricks on Str.58', 5, 2),
  (4, 3, '07/03/2017', '07/06/2017', 'Cut off streetlight on Str. 11', 1, 1)

-- Ex. 03 Update

UPDATE Reports
   SET StatusId = 2
WHERE StatusId = 1 AND CategoryId = 4;

-- Ex. 04 Delete

DELETE FROM Reports WHERE StatusId = 4;

-- 10/10

-- Ex. 05 Users By Age

SELECT Username, Age
  FROM Users
 ORDER BY Age, Username DESC;

-- Ex. 06 Unassigned Reports

SELECT Description, OpenDate
  FROM Reports
 WHERE EmployeeId IS NULL
 ORDER BY OpenDate, Description;

-- Ex. 07 Employees And Reports

SELECT e.FirstName,
       e.LastName,
       r.Description,
       FORMAT(r.OpenDate, 'yyyy-MM-dd') AS OpenDate
  FROM Employees AS e
  JOIN Reports AS r
    ON e.Id = r.EmployeeId
 WHERE r.EmployeeId IS NOT NULL
 ORDER BY r.EmployeeId, r.OpenDate, r.Id;

-- Ex. 08 Most Reported Category

SELECT c.Name AS CategoryName,
       COUNT(r.Id) AS ReportsNumber
  FROM Categories AS c
  JOIN Reports AS r
    ON c.Id = r.CategoryId
 GROUP BY c.Name
 ORDER BY ReportsNumber DESC, CategoryName

-- Ex. 09 Employees In Category

SELECT c.Name AS CategoryName,
       COUNT(e.Id) AS [Employees Number]
  FROM Categories AS c
  JOIN Departments AS d
    ON c.DepartmentId = d.Id
  JOIN Employees AS e
    ON d.Id = e.DepartmentId
 GROUP BY c.Name
 ORDER BY CategoryName;

-- Ex. 10 Users per Employee

SELECT e.FirstName + ' ' +  e.LastName AS Name,
       COUNT(DISTINCT r.UserId) AS [Users Number]
  FROM Employees AS e
  LEFT JOIN Reports AS r
    ON e.Id = r.EmployeeId
 GROUP BY e.FirstName, e.LastName
 ORDER BY [Users Number] DESC, Name ASC

-- Ex. 11 Emergency Patrol

SELECT r.OpenDate,
       r.Description,
       u.Email AS [Reporter Email]
  FROM Reports AS r
  JOIN Categories AS c
    ON r.CategoryId = c.Id
  JOIN Departments AS d
    ON c.DepartmentId = d.Id
  JOIN Users AS u
    ON r.UserId = u.Id
 WHERE CloseDate IS NULL
   AND Description LIKE '%str%'
   AND LEN(Description) > 20
   AND d.Id IN(1, 4, 5)
 ORDER BY OpenDate, [Reporter Email], u.Id

-- Ex. 12 Birthday Report

SELECT c.Name AS [Category Name]
  FROM Categories AS c
  JOIN Reports AS r
    ON c.Id = r.CategoryId
  JOIN Users AS u
    ON r.UserId = u.Id
 WHERE DATEPART(MONTH, u.Birthdate) = DATEPART(MONTH, r.OpenDate)
   AND DATEPART(DAY, u.Birthdate) = DATEPART(DAY, r.OpenDate)
 GROUP BY c.Name
 ORDER BY c.Name

-- Ex. 13 Numbers Coincidence

SELECT DISTINCT Username
  FROM
    (
      SELECT Username,
             LEFT(Username, 1) AS [FirstChar],
             RIGHT(Username, 1) AS [LastChar],
             r.CategoryId AS [CategoryId]
        FROM Users AS u
        JOIN Reports AS r
          ON u.Id = r.UserId
       WHERE Username LIKE '[0-9]%' OR Username LIKE '%[0-9]'
    ) AS Test
 WHERE TRY_PARSE(FirstChar AS INT) = CategoryId OR TRY_PARSE(LastChar AS INT) = CategoryId
 ORDER BY Username;



-- Ex. 14 Open/Closed Statistics

-- no solution found

-- Ex. 15 Average Closing Time

SELECT d.Name AS [Department Name],
       ISNULL(CONVERT(VARCHAR, AVG(DATEDIFF(DAY, R.Opendate, R.Closedate))), 'no info') AS [Average Duration]
  FROM Departments AS d
  JOIN Categories AS C ON C.DepartmentId = D.Id
  LEFT JOIN Reports AS R ON R.CategoryId = C.Id
 GROUP BY d.Name
 ORDER BY d.Name


-- Ex. 16 Favorite Categories

SELECT [Department Name], [Category Name], Percentage
  FROM
    (
      SELECT d.Name AS [Department Name],
             c.Name AS [Category Name],
             CAST(ROUND(COUNT(1) OVER(PARTITION BY c.Id) * 100.00 / COUNT(1) OVER(PARTITION BY d.Id), 0) AS INT) AS Percentage
        FROM Departments AS d
        JOIN Categories AS c
          ON d.Id = c.DepartmentId
        JOIN Reports R on c.Id = R.CategoryId
  ) AS Report
  GROUP BY [Department Name], [Category Name], Percentage
  ORDER BY [Department Name], [Category Name], Percentage

-- Ex. 17 Employee's Load

CREATE FUNCTION dbo.udf_GetReportsCount(@employeeId INT, @statusId INT)
  RETURNS INT
  AS
  BEGIN
    DECLARE @result INT =
    (
      SELECT ISNULL(COUNT(Id), 0)
        FROM Reports
       WHERE Id = @employeeId AND StatusId = @statusId
    )
    RETURN @result
  END

SELECT Id, FirstName, Lastname, dbo.udf_GetReportsCount(1, 2) AS ReportsCount
FROM Employees
ORDER BY Id

-- Ex. 18 Assign Employee

CREATE PROCEDURE usp_AssignEmployeeToReport(@employeeId int, @reportId int)
AS
BEGIN TRANSACTION

	DECLARE @employeeDepartmentId int = (SELECT DepartmentId
					     FROM Employees
					     WHERE Id = @employeeId);

  DECLARE @reportDepartmentId int = (SELECT c.DepartmentId
				           FROM Reports AS r
				           INNER JOIN Categories AS c ON c.Id = r.CategoryId
				           WHERE r.Id = @reportId)

   	IF(@employeeDepartmentId = @reportDepartmentId)
	BEGIN
		UPDATE Reports
		SET EmployeeId = @employeeId
		WHERE Id = @reportId
		COMMIT
	END
	ELSE
	BEGIN
		ROLLBACK;
		THROW 51010, 'Employee doesn''t belong to the appropriate department!', 1;
		RETURN
	END




CREATE DATABASE TableRelations;

USE TableRelations;

-- Ex. 01
CREATE TABLE Persons (
  PersonID INT PRIMARY KEY,
  FirstName VARCHAR(50),
  Salary DECIMAL,
  PassportID INT UNIQUE
)

CREATE TABLE Passports (
  PassportID INT PRIMARY KEY,
  PassportNumber VARCHAR(50)
);

INSERT INTO Persons VALUES
  (1, 'Roberto', 43300.00, 102),
  (2, 'Tom', 56100.00, 103),
  (3, 'Yana', 60200.00, 101);

INSERT INTO Passports VALUES
    (101, 'N34FG21B'),
    (102, 'K65LO4R7'),
    (103, 'ZE657QP2');

ALTER TABLE Persons
      ADD CONSTRAINT FK_Persons_Passports
      FOREIGN KEY (PassportID) REFERENCES Passports(PassportID);

-- Ex. 02

CREATE TABLE Manufacturers (
  ManufacturerID INT PRIMARY KEY IDENTITY,
  Name VARCHAR(50) NOT NULL,
  EstablishedOn DATE
)

CREATE TABLE Models (
  ModelID INT PRIMARY KEY IDENTITY,
  Name VARCHAR(50) NOT NULL,
  ManufacturerID INT,
  CONSTRAINT FK_Models_Manufacturers
  FOREIGN KEY (ManufacturerID)
  REFERENCES Manufacturers(ManufacturerID)
);

INSERT INTO Manufacturers VALUES
  ('BMW', '07/03/1916'),
  ('Tesla', '01/01/2003'),
  ('Lada', '01/05/1966')

INSERT INTO Models VALUES
  ('X1', 1),
  ('i6', 1),
  ('Model S', 2),
  ('Model X', 2),
  ('Model 3', 2),
  ('Nova', 3)

-- Ex. 03

CREATE TABLE Students (
  StudentId INT PRIMARY KEY IDENTITY,
  Name VARCHAR(50)
)

CREATE TABLE Exams (
  ExamID INT PRIMARY KEY IDENTITY(101, 1),
  Name VARCHAR(50)
)

INSERT INTO Students VALUES
  ('Mila'),
  ('Toni'),
  ('Ron')

INSERT INTO Exams VALUES
  ('SpringMVC'),
  ('Neo4j'),
  ('Oracle 11g')

CREATE TABLE StudentsExams (
  StudentID INT,
  ExamID INT,

  CONSTRAINT PK_StudentsExams
  PRIMARY KEY(StudentID, ExamID),

  CONSTRAINT FK_StudentsExams_Students
  FOREIGN KEY(StudentID)
  REFERENCES Students(StudentID),

  CONSTRAINT FK_StudentsExams_Exams
  FOREIGN KEY(ExamID)
  REFERENCES Exams(ExamID)
)

INSERT INTO StudentsExams VALUES
  (1, 101),
  (1, 102),
  (2, 101),
  (3, 103),
  (2, 102),
  (2, 103)

-- Ex. 04

CREATE TABLE Teachers (
  TeacherID INT PRIMARY KEY,
  Name VARCHAR(50),
  ManagerID INT,

  CONSTRAINT FK_ManagerID_TeacherID
  FOREIGN KEY(ManagerID)
  REFERENCES Teachers(TeacherID)
)

INSERT INTO Teachers VALUES
  (101, 'John', NULL),
  (102, 'Maya', 106),
  (103, 'Silvia', 106),
  (104, 'Ted', 105),
  (105, 'Mark', 101),
  (106, 'Greta', 101)

-- Ex. 05

CREATE DATABASE OnlineStore;
USE OnlineStore;

CREATE TABLE Cities (
  CityID INT PRIMARY KEY IDENTITY NOT NULL,
  Name VARCHAR(50) NOT NULL
)

CREATE TABLE Customers (
  CustomerID INT PRIMARY KEY IDENTITY NOT NULL,
  Name VARCHAR(50) NOT NULL,
  Birthday DATE,
  CityID INT,

  CONSTRAINT FK_Customers_Cities
  FOREIGN KEY(CityID)
  REFERENCES Cities(CityID)
)

CREATE TABLE Orders (
  OrderID INT PRIMARY KEY IDENTITY NOT NULL,
  CustomerID INT NOT NULL,

  CONSTRAINT FK_Orders_Customers
  FOREIGN KEY(CustomerID)
  REFERENCES Customers(CustomerID)
)

CREATE TABLE ItemTypes (
  ItemTypeID INT PRIMARY KEY IDENTITY NOT NULL,
  Name VARCHAR(50) NOT NULL
)

CREATE TABLE Items (
  ItemID INT PRIMARY KEY IDENTITY NOT NULL,
  Name VARCHAR(50) NOT NULL,
  ItemTypeID INT NOT NULL,

  CONSTRAINT FK_Items_ItemTypes
  FOREIGN KEY(ItemTypeID)
  REFERENCES ItemTypes(ItemTypeID)
)

CREATE TABLE OrderItems (
  OrderID INT NOT NULL,
  ItemID INT NOT NULL,

  CONSTRAINT PK_OrderItems PRIMARY KEY (OrderID, ItemID),

  CONSTRAINT FK_OrderItems_Orders
  FOREIGN KEY(OrderID)
  REFERENCES Orders(OrderID),

  CONSTRAINT FK_OrderItems_Items
  FOREIGN KEY(ItemID)
  REFERENCES Items(ItemID)
)

-- Ex. 06

CREATE DATABASE University;
USE University;

CREATE TABLE Majors (
  MajorID INT PRIMARY KEY IDENTITY NOT NULL,
  Name VARCHAR(50) NOT NULL
)

CREATE TABLE Students (
  StudentID INT PRIMARY KEY IDENTITY NOT NULL,
  StudentNumber VARCHAR(50) NOT NULL,
  StudentName VARCHAR(50) NOT NULL,
  MajorID INT,

  CONSTRAINT FK_Students_Majors
  FOREIGN KEY(MajorID)
  REFERENCES Majors(MajorID)
)

CREATE TABLE Payments (
  PaymentID INT PRIMARY KEY IDENTITY NOT NULL,
  PaymentDate DATE NOT NULL,
  PaymentAmount DECIMAL NOT NULL,
  StudentID INT,

  CONSTRAINT FK_Payments_Students
  FOREIGN KEY(StudentID)
  REFERENCES Students(StudentID)
)

CREATE TABLE Subjects (
  SubjectID INT PRIMARY KEY IDENTITY NOT NULL,
  SubjectName VARCHAR(50) NOT NULL
)

CREATE TABLE Agenda (
  StudentID INT NOT NULL,
  SubjectID INT NOT NULL,

  CONSTRAINT PK_Agenda PRIMARY KEY(StudentID, SubjectID),

  CONSTRAINT FK_Agenda_Students
  FOREIGN KEY(StudentID)
  REFERENCES Students(StudentID),

  CONSTRAINT FK_Agenda_Subjects
  FOREIGN KEY(SubjectID)
  REFERENCES Subjects(SubjectID)
)

-- Ex. 09

USE Geography;

SELECT m.MountainRange, p.PeakName, p.Elevation
  FROM Mountains AS m
  JOIN Peaks AS p
  ON m.Id = p.MountainId
  WHERE MountainRange = 'Rila'
  ORDER BY Elevation DESC;





-- Ex. 01 DDL

CREATE TABLE Countries (
  Id INT PRIMARY KEY IDENTITY,
  Name NVARCHAR(50) UNIQUE
)

CREATE TABLE Customers (
  Id INT PRIMARY KEY IDENTITY,
  FirstName NVARCHAR(25),
  LastName NVARCHAR(25),
  Gender CHAR(1) CHECK (Gender IN ('F', 'M')),
  Age INT,
  PhoneNumber CHAR(10),
  CountryId INT FOREIGN KEY REFERENCES Countries(Id)
)

CREATE TABLE Products (
  Id INT PRIMARY KEY IDENTITY,
  Name NVARCHAR(25) UNIQUE,
  Description NVARCHAR(255),
  Recipe NVARCHAR(MAX),
  Price MONEY CHECK (Price >= 0)
)

CREATE TABLE Feedbacks (
  Id INT PRIMARY KEY IDENTITY,
  Description NVARCHAR(255),
  Rate DECIMAL(4,2) CHECK (Rate BETWEEN 0 AND 10),
  ProductId INT FOREIGN KEY REFERENCES Products(Id),
  CustomerId INT FOREIGN KEY REFERENCES Customers(Id)
)


CREATE TABLE Distributors (
  Id INT PRIMARY KEY IDENTITY,
  Name NVARCHAR(25) UNIQUE,
  AddressText NVARCHAR(30),
  Summary NVARCHAR(200),
  CountryId INT FOREIGN KEY REFERENCES Countries(Id)
)

CREATE TABLE Ingredients (
  Id INT PRIMARY KEY IDENTITY,
  Name NVARCHAR(30),
  Description NVARCHAR(200),
  OriginCountryId INT FOREIGN KEY REFERENCES Countries(Id),
  DistributorId INT FOREIGN KEY REFERENCES Distributors(Id)
)

CREATE TABLE ProductsIngredients (
  ProductId INT FOREIGN KEY REFERENCES Products(Id),
  IngredientId INT FOREIGN KEY REFERENCES Ingredients(Id),
  CONSTRAINT PK_ProductsIngredients
  PRIMARY KEY (ProductId, IngredientId)
)

-- Ex. 02 Insert

INSERT INTO Distributors (Name, AddressText, Summary, CountryId) VALUES
  ('Deloitte & Touche', '6 Arch St #9757', 'Customizable neutral traveling', 2),
  ('Congress Title', '58 Hancock St', 'Customer loyalty', 13),
  ('Kitchen People', '3 E 31st St #77', 'Triple-buffered stable delivery', 1),
  ('General Color Co Inc', '6185 Bohn St #72', 'Focus group', 21),
  ('Beck Corporation', '21 E 64th Ave', 'Quality-focused 4th generation hardware', 23)

INSERT INTO Customers (FirstName, LastName, Gender, Age, PhoneNumber, CountryId) VALUES
  ('Francoise', 'Rautenstrauch', 'M', 15,'0195698399', 5),
  ('Kendra', 'Loud', 'F', 22,'0063631526', 11),
  ('Lourdes', 'Bauswell', 'M', 50,'0139037043', 8),
  ('Hannah', 'Edmison','F', 18, '0043343686', 1),
  ('Tom', 'Loeza','M', 31,'0144876096', 23),
  ('Queenie', 'Kramarczyk','F', 30,'0064215793', 29),
  ('Hiu', 'Portaro', 'M',25, '0068277755', 16),
  ('Josefa', 'Opitz','F', 43, '0197887645', 17)

-- Ex. 03 Update

UPDATE Ingredients
  SET DistributorId = 35
 WHERE Name IN ('Bay Leaf', 'Paprika', 'Poppy');

UPDATE Ingredients
   SET OriginCountryId = 14
 WHERE OriginCountryId = 8;

-- Ex. 04 Delete

DELETE FROM Feedbacks WHERE CustomerId = 14 OR ProductId = 5;

-- Ex. 05 Products By Price

SELECT Name, Price, Description
  FROM Products
 ORDER BY Price DESC, Name;

-- Ex. 06 Ingredients

SELECT Name, Description, OriginCountryId
  FROM Ingredients
 WHERE OriginCountryId IN (1, 10, 20)
 ORDER BY Id;

-- Ex. 07 Ingredients from Bulgaria and Greece

SELECT TOP 15
      i.Name,
      i.Description,
      c.Name AS CountryName
  FROM Ingredients AS i
  JOIN Countries AS c
    ON i.OriginCountryId = c.Id
 WHERE c.Name IN ('Bulgaria', 'Greece')
 ORDER BY i.Name, c.Name;

-- Ex. 08 Best Rated Products

SELECT TOP 10
       p.Name,
       p.Description,
       AVG(f.Rate) AS AverageRate,
       COUNT(f.Rate)AS FeedbacksAmount
  FROM Products AS p
  JOIN Feedbacks AS f
    ON p.Id = f.ProductId
 GROUP BY p.Name, p.Description
 ORDER BY AverageRate DESC, FeedbacksAmount DESC;

-- Ex. 09 Negative Feedback

SELECT f.ProductId,
       f.Rate,
       f.Description,
       f.CustomerId,
       c.Age,
       c.Gender
  FROM Feedbacks AS f
  JOIN Customers AS C
    ON f.CustomerId = c.Id
 WHERE f.Rate < 5
 ORDER BY f.ProductId DESC, f.Rate ASC;

-- Ex. 10 Customers without Feedback

SELECT CONCAT(FirstName, ' ',LastName) AS Name,
       PhoneNumber,
       Gender
  FROM Customers
 WHERE Id NOT IN (SELECT CustomerId FROM Feedbacks);

-- Ex. 11 Honorable Mentions

SELECT f.ProductId,
       CONCAT(c.FirstName, ' ' , c.LastName) AS CustomerName,
       f.Description
  FROM Feedbacks AS f
  JOIN Customers AS c
    ON f.CustomerId = c.Id
 WHERE c.Id IN
       (
         SELECT CustomerId
           FROM Feedbacks AS f
           GROUP BY f.CustomerId
           HAVING COUNT(f.Id) >= 3
       )
 ORDER BY f.ProductId, CustomerName, f.Id;

-- Ex. 12 Customers by Criteria

SELECT FirstName,
       Age,
       PhoneNumber
  FROM Customers
 WHERE Age >= 21
   AND FirstName LIKE '%an%'
    OR PhoneNumber LIKE '%38'
   AND CountryId <> 31
 ORDER BY FirstName, Age DESC;

-- Ex 13. Middle Range Distributors

SELECT d.Name AS DistributorName,
       i.Name AS IngredientName,
       p.Name AS ProductName,
       AVG(f.Rate) AS [AverageRate]
  FROM Distributors AS d
  JOIN Ingredients AS i
    ON d.Id = i.DistributorId
  JOIN ProductsIngredients AS pi
    ON i.Id = pi.IngredientId
  JOIN Products AS p
    ON pi.ProductId = p.Id
  JOIN Feedbacks AS f
    ON p.Id = f.ProductId
 GROUP BY d.Name, i.Name, p.Name
 HAVING AVG(f.Rate) BETWEEN 5 AND 8
 ORDER BY DistributorName, IngredientName, ProductName;

-- Ex. 14 The Most Positive Country

SELECT TOP 1 WITH TIES
  c.Name AS CountryName,
  AVG(f.Rate) AS FeedbackRate
  FROM Countries AS c
  JOIN Customers AS cust
    ON c.Id = cust.CountryId
  JOIN Feedbacks AS f
    ON cust.Id = f.CustomerId
 GROUP BY c.Name
 ORDER BY AVG(f.Rate) DESC;

-- Ex. 15 Country Representative

SELECT CountryName,
       DistributorName
  FROM
    (
      SELECT c.Name AS CountryName,
             d.Name AS DistributorName,
             COUNT(i.DistributorId) AS IngredientsByDistributor,
             DENSE_RANK() OVER(PARTITION BY c.Name ORDER BY COUNT(c.Id) DESC) AS Rank
       FROM Countries AS c
       LEFT JOIN Distributors AS d
         ON c.Id = d.CountryId
       LEFT JOIN Ingredients AS i
         ON c.Id = i.OriginCountryId
      GROUP BY c.Name, d.Name
    ) AS Ranked
 WHERE Rank = 1
 ORDER BY CountryName, DistributorName;

-- Ex. 16 Customers with Countries

CREATE VIEW v_UserWithCountries AS
SELECT cust.FirstName + ' ' + cust.LastName AS CustomerName,
       cust.Age,
       cust.Gender,
       c.Name AS CountryName
FROM Customers AS cust
JOIN Countries AS c
  ON cust.CountryId = c.Id;

SELECT TOP 5 *
  FROM v_UserWithCountries
 ORDER BY Age

-- Ex. 17 Feedback by Product Name

CREATE FUNCTION dbo.udf_GetRating(@Name NVARCHAR(25))
  RETURNS NVARCHAR(10)
  AS
  BEGIN
    DECLARE @avgRate DECIMAL(4, 2) =
      (
        SELECT AVG(f.Rate)
          FROM Products AS p
          JOIN Feedbacks AS f
            ON p.Id = f.ProductId
        WHERE p.Name = @Name
      )

    DECLARE @rating VARCHAR(10) =
      CASE
        WHEN @avgRate IS NULL THEN 'No rating'
        WHEN @avgRate < 5 THEN 'Bad'
        WHEN @avgRate <= 8 THEN 'Average'
        ELSE 'Good'
      END
        RETURN @rating
  END

SELECT TOP 5 Id, Name, dbo.udf_GetRating(Name)
  FROM Products
 ORDER BY Id
USE WMS;

SELECT c.FirstName + ' ' + c.LastName AS [Client Full Name],
       DATEDIFF(DAY, IssueDate, '2017/04/24') AS [Days going],
       j.Status
  FROM Clients AS c
  JOIN Jobs AS j
    ON c.ClientId = j.ClientId
 WHERE j.Status IN ('Pending', 'In Progress')
 ORDER BY [Days going] DESC, c.ClientId ASC;



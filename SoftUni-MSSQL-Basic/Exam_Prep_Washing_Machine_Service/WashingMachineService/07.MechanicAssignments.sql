USE WMS;

SELECT m.FirstName + ' ' + m.LastName AS Mechanic,
       Status,
       IssueDAte
  FROM Mechanics AS m
 INNER JOIN Jobs AS j
    ON m.MechanicId = j.MechanicId
 ORDER BY m.MechanicId, j.IssueDate, j.JobId ASC;

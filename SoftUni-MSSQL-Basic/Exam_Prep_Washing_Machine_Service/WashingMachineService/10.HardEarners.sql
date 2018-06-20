USE WMS;

SELECT TOP 3
       m.FirstName + ' ' + m.LastName,
       COUNT(j.Status) AS Jobs
  FROM Mechanics AS m
 INNER JOIN Jobs AS j
    ON m.MechanicId = j.MechanicId
  WHERE j.Status <> 'Finished'
  GROUP BY m.FirstName, m.LastName, m.MechanicId
  HAVING COUNT(j.Status) > 1
 ORDER BY Jobs DESC, m.MechanicId ASC;
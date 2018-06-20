USE WMS;


SELECT FirstName + ' ' + LastName FROM Mechanics AS Avaliable
 WHERE MechanicId NOT IN
  (
    SELECT MechanicId FROM Jobs WHERE MechanicId IS NOT NULL AND Status <> 'Finished'
  )
 ORDER BY MechanicId ASC;

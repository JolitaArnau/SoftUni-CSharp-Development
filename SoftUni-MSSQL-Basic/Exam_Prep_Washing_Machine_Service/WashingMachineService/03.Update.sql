USE WMS;

UPDATE Jobs
   SET MechanicId = 3
 WHERE Status = 'Pending';

UPDATE Jobs
   SET Status = 'In Progress'
 WHERE Status = 'Pending' AND MechanicId = 3;

SELECT * FROM Jobs WHERE MechanicId = 3 And Status = 'In Progress';
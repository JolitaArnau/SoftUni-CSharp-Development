USE WMS;

SELECT j.JobId,
  (
    SELECT ISNULL(SUM(op.Quantity * p.Price), 0)
      FROM Parts as p
     INNER JOIN OrderParts AS op
        ON p.PartId = op.PartId
     INNER JOIN Orders AS o
        ON op.OrderId = o.OrderId
     INNER JOIN Jobs AS jo
        ON o.JobId = jo.JobId
     WHERE jo.JobId = j.JobId
  ) AS Total
  FROM Jobs AS j
  WHERE j.Status = 'Finished'
  ORDER BY Total DESC, j.JobId ASC;

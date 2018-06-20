USE WMS;

SELECT TOP 1 WITH TIES
      m.Name,
      COUNT(*) AS [Times Serviced],
      (
        SELECT ISNULL(SUM(p.Price * op.Quantity),0)
          FROM Jobs AS j
         INNER JOIN Orders AS o
            ON j.JobId = o.JobId
         INNER JOIN OrderParts AS op
            ON o.OrderId = op.OrderId
         INNER JOIN Parts AS p
            ON op.PartId = P.PartId
         WHERE j.ModelId = m.ModelId
      ) AS [Parts Total ]
FROM Models AS m
INNER JOIN Jobs J on m.ModelId = J.ModelId
GROUP BY m.ModelId, m.Name
ORDER BY [Times Serviced] DESC;


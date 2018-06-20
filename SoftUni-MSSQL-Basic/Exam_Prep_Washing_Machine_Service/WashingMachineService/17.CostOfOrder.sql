USE WMS;

CREATE FUNCTION dbo.udf_GetCost(@jobId INT)
  RETURNS DECIMAL(6,2)
  AS
  BEGIN
   DECLARE @TotalSum DECIMAL(6,2) = (SELECT
           ISNULL(SUM(p.Price * op.Quantity),0) AS Result
    FROM Parts AS p
    JOIN OrderParts AS op
      ON p.PartId = op.PartId
    JOIN Orders AS o
      ON op.OrderId = o.OrderId
    JOIN Jobs AS j
      ON o.JobId = j.JobId
    WHERE j.JobId = @jobId)
    RETURN @TotalSum;
  END

EXEC dbo.udf_GetCost 1;



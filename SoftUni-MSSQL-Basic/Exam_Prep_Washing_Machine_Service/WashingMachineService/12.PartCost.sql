USE WMS;

SELECT SUM(Price) FROM Parts;

SELECT ISNULL(SUM (p.Price * op.Quantity), 0) AS [Parts Total]
  FROM Parts AS p
 INNER JOIN OrderParts AS op
    ON p.PartId = op.PartId
 INNER JOIN Orders AS o
    ON op.OrderId = o.OrderId
  WHERE DATEDIFF(WEEK, o.IssueDate, '2017/04/24') <= 3;


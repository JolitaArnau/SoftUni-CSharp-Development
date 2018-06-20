USE WMS;

SELECT m.ModelId,
       m.Name,
       CONCAT(AVG(DATEDIFF(DAY, IssueDate, FinishDate)), ' days') AS [Average Service Time]
  FROM Models AS m
 INNER JOIN Jobs AS j
    ON m.ModelId = J.ModelId
 GROUP BY m.ModelId, m.Name
 ORDER BY AVG(DATEDIFF(DAY, IssueDate, FinishDate)) ASC;

USE WMS;

SELECT Status, IssueDAte
  FROM Jobs
 WHERE Status <> 'Finished'
 ORDER BY IssueDAte, JobId ASC;
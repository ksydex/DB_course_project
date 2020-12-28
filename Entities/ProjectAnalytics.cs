namespace ContractAndProjectManager.Entities
{
    public class ProjectAnalytics
    {
        public int ProjectId { get; set; }
        public double? TotalHours { get; set; }
        public int StagesCount { get; set; }
        public int DoneTasksCount { get; set; }
        public int TasksCount { get; set; }
        public int EmployeesCount { get; set; }
    }

    // SQL query
    // CREATE OR REPLACE VIEW "ProjectAnalytics" AS
    // SELECT a."Id" AS "ProjectId",
    //
    // (SELECT SUM((EXTRACT(EPOCH
    // FROM "DateDeadLine" - "DateStart")/3600))::float
    // FROM "Tasks"
    // WHERE "StageId" IN
    // (SELECT "Id"
    // FROM "ProjectStages"
    // WHERE "ProjectId" = a."Id")) AS "TotalHours",
    //
    // COUNT(*) AS "StagesCount",
    //
    // (SELECT COUNT(*)
    // FROM "Users"
    // WHERE "TeamId" = a."TeamId") AS "EmployeesCount",
    //
    // (SELECT COUNT(*) FROM get_tasks(a."Id")) AS "TasksCount",
    //
    // (SELECT COUNT(*) FROM get_tasks(a."Id") WHERE "statusId" = 6) AS "DoneTasksCount"
    // FROM "Projects" a
    // LEFT JOIN "ProjectStages" b ON a."Id" = b."ProjectId"
    // GROUP BY a."Id";


    // SQL FUNCTION
    // CREATE OR REPLACE FUNCTION get_tasks (
    //   projectId integer
    // ) 
    // 	RETURNS TABLE (
    // 		"id" int,
    // 		"statusId" int
    // 	) 
    // 	LANGUAGE plpgsql
    // AS $$
    // BEGIN
    // 	RETURN QUERY
    // 	   WITH n AS
    // 			(
    // 			   SELECT *,
    // 					 ROW_NUMBER() OVER (PARTITION BY "EntityId" ORDER BY "DateCreated" DESC) AS rn
    // 			   FROM "TaskStatusHistories"
    // 			)
    // 		 SELECT "Id" as "id", "StatusId" as "statusId"
    // 		 FROM n
    // 		 WHERE rn = 1 AND "EntityId" IN (
    // 			SELECT c1."Id" FROM "Projects" a1 
    // 			INNER JOIN "ProjectStages" b1 ON a1."Id" = b1."ProjectId"
    // 			INNER JOIN "Tasks" c1 ON c1."StageId" = b1."Id"
    // 			WHERE a1."Id" = projectId
    // 			GROUP BY c1."Id"
    // 		 );
    // end;
    // $$
    
    // SQL FUNCTION
    // CREATE OR REPLACE FUNCTION get_last_task_status (
    // taskId integer
    // ) 
    // returns integer
    // LANGUAGE plpgsql
    // AS $$
    // BEGIN
    // RETURN (SELECT "StatusId" FROM "TaskStatusHistories" WHERE "EntityId" = taskID ORDER BY "DateCreated" DESC LIMIT 1);
    // END;
    // $$
}
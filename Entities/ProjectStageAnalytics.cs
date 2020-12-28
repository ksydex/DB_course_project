namespace ContractAndProjectManager.Entities
{
    public class ProjectStageAnalytics
    {
        public int ProjectStageId { get; set; }
        public double? TotalHours { get; set; }
        public int DoneTasksCount { get; set; }
        public int TasksCount { get; set; }
        public int EmployeesCount { get; set; }
    }
}
// SQL script
// CREATE OR REPLACE VIEW "ProjectStageAnalytics" AS
// SELECT a."Id" AS "ProjectStageId", 
//
// SUM((EXTRACT(EPOCH
// FROM b."DateDeadLine" - b."DateStart")/3600)::float) AS "TotalHours",
//
// COUNT(DISTINCT b."ExecutorId") AS "EmployeesCount",
//
// COUNT(*) AS "TasksCount",
//
// SUM(CASE get_last_task_status(b."Id") WHEN 6 THEN 1 ELSE 0 END) AS "DoneTasksCount"
//
// FROM "ProjectStages" a
// INNER JOIN "Tasks" b ON b."StageId" = a."Id"
// GROUP BY a."Id";
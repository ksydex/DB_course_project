namespace ContractAndProjectManager.Entities
{
    public class RequestAnalytics
    {
        public int RequestId { get; set; }
        public double? TotalHours { get; set; }
        public int ContractStagesCount { get; set; }
        public int ProjectStagesCount { get; set; }
        public int EmployeesCount { get; set; }
    }
    
    // SQL query
    // CREATE VIEW "RequestAnalytics" AS
    // SELECT a."Id" as "RequestId", aa."Id" as "ProjectId",
    // (SELECT SUM((EXTRACT(EPOCH from "DateDeadLine" - "DateStart")/3600))::float FROM "ContractStages" WHERE "ContractId" = aa."Id") as "TotalHours",
    // (SELECT COUNT(*) FROM "ProjectStages" WHERE "ContractStageId" IN (SELECT "Id" FROM "ContractStages" WHERE "ContractId" = b."ContractId")) as "ProjectStagesCount",
    // (SELECT COUNT(*) FROM "ContractStages" WHERE "ContractId" = aa."Id") as "ContractStagesCount",
    // (SELECT COUNT(*) FROM "Users" WHERE "TeamId" = c."TeamId") as "EmployeesCount"
    // FROM "Requests" a
    // LEFT JOIN "Contracts" aa on aa."RequestId" = a."Id"
    // LEFT JOIN "ContractStages" b ON aa."Id" = b."ContractId"
    // LEFT JOIN "Projects" c ON c."ContractId" = aa."Id"
    // LEFT JOIN "Users" d ON d."TeamId" = c."TeamId"
    // GROUP BY a."Id", aa."Id", b."ContractId", c."Id";
}
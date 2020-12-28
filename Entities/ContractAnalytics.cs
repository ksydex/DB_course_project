namespace ContractAndProjectManager.Entities
{
    public class ContractAnalytics
    {
        public int ContractId { get; set; }
        public double? TotalHours { get; set; }
        public int StagesCount { get; set; }
        public int EmployeesCount { get; set; }
    }
    
    // SQL query
    // CREATE OR REPLACE VIEW "ContractAnalytics" AS
    // SELECT a."Id" as "ContractId",
    // (SELECT SUM((EXTRACT(EPOCH from "DateDeadLine" - "DateStart")/3600))::float FROM "ContractStages" WHERE "ContractId" = a."Id") as "TotalHours",
    // (SELECT COUNT(*) FROM "ContractStages" WHERE "ContractId" = a."Id") as "StagesCount",
    // (SELECT COUNT(*) FROM "Users" WHERE "TeamId" = c."TeamId") as "EmployeesCount"
    // FROM "Contracts" a
    // LEFT JOIN "ContractStages" b ON a."Id" = b."ContractId"
    // LEFT JOIN "Projects" c ON c."ContractId" = a."Id"
    // GROUP BY a."Id", c."Id";
}
using System.Collections.Generic;
using ContractAndProjectManager.Infrastructure.AbstractClasses;
using ContractAndProjectManager.Models;

namespace ContractAndProjectManager.Entities
{
    public class ProjectStatus : Status
    {
        public static ProjectStatus Pending = new ProjectStatus
        {
            Id = 16,
            Name = "Ожидание",
            Color = "#ffc107"
        };
        public static ProjectStatus Active = new ProjectStatus
        {
            Id = 2,
            Name = "Активный проект",
            Color = "#0052cc"
        };

        public static ProjectStatus Completed = new ProjectStatus
        {
            Id = 13,
            Name = "Завершен",
            Color = "#36b37e"
        };

        public static ProjectStatus Denied = new ProjectStatus
        {
            Id = 14,
            Name = "Отклонен",
            Color = "#d04437"
        };
        
        public static List<ProjectStatus> All = new List<ProjectStatus>
        {
            Pending,
            Active,
            Completed,
            Denied
        };
    }
    
    public class ProjectStatusHistory: StatusHistory<ProjectStatus, Project> {}
    
    // DROP TRIGGER IF EXISTS Project_INSERT
    // ON "Projects";
    // CREATE TRIGGER Project_INSERT
    // AFTER INSERT ON "Projects"
    // FOR EACH ROW EXECUTE PROCEDURE add_status_to_entity_trigger("Project", 16);
}
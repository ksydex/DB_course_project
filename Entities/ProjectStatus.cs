using ContractAndProjectManager.Models;

namespace ContractAndProjectManager.Entities
{
    public class ProjectStatus : Status
    {
        public static ProjectStatus Active = new ProjectStatus
        {
            Id = 12,
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
    }
}
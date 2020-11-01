using ContractAndProjectManager.Models;

namespace ContractAndProjectManager.Entities
{
    public class TaskStatus : Status
    {
        public static TaskStatus Pending = new TaskStatus
        {
            Id = 4,
            Name = "К выполнению"
        };
        
        public static TaskStatus Developing = new TaskStatus
        {
            Id = 5,
            Name = "В работе"
        };

        public static TaskStatus Completed = new TaskStatus
        {
            Id = 6,
            Name = "Готово"
        };
    }
}
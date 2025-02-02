﻿using System.Collections.Generic;
using ContractAndProjectManager.Infrastructure.AbstractClasses;
using ContractAndProjectManager.Models;

namespace ContractAndProjectManager.Entities
{
    public class TaskStatus : Status
    {
        public static TaskStatus Pending = new TaskStatus
        {
            Id = 4,
            Name = "К выполнению",
            Color = "#dfe1e6"
        };
        
        public static TaskStatus InWork = new TaskStatus
        {
            Id = 5,
            Name = "В работе",
            Color = "#0052cc"
        };

        public static TaskStatus Completed = new TaskStatus
        {
            Id = 6,
            Name = "Готово",
            Color = "#36b37e"
        };
        
        public static List<TaskStatus> All = new List<TaskStatus>
        {
            Pending,
            InWork,
            Completed
        };
    }
    
    public class TaskStatusHistory : StatusHistory<TaskStatus, Task> {}
    
    // DROP TRIGGER IF EXISTS Task_INSERT
    // ON "Tasks";
    // CREATE TRIGGER Task_INSERT
    // AFTER INSERT ON "Tasks"
    // FOR EACH ROW EXECUTE PROCEDURE add_status_to_entity_trigger("Task", 4);
}
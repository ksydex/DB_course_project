using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ContractAndProjectManager.Infrastructure.Interfaces;

namespace ContractAndProjectManager.Entities
{
    public class Task : IWithDateCreated, IWithRecordDates, IWithTitleDescription
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
        public int? TaskId { get; set; }
        public virtual Task ParentTask { get; set; }

        public virtual List<Task> Tasks { get; set; }
        
        public DateTime DateCreated { get; set; }
        
        public DateTime DateStart { get; set; }
        public DateTime DateDeadLine { get; set; }
        public DateTime DateEnd { get; set; }

        public int? ExecutorId { get; set; }
        public virtual Employee Executor { get; set; } 
        
        public int StatusId { get; set; }
        public virtual TaskStatus Status { get; set; }

        public Task()
        {
            StatusId = TaskStatus.Pending.Id;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ContractAndProjectManager.Infrastructure.Interfaces;

namespace ContractAndProjectManager.Entities
{
    public class Task : IWithDateCreated, IWithRecordDates, IWithTitleDescription,
        IWithStatusHistory<TaskStatusHistory, TaskStatus, Task>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        
        public DateTime? DateStart { get; set; }
        public DateTime? DateDeadLine { get; set; }
        public DateTime? DateEnd { get; set; }

        public int? ExecutorId { get; set; }
        public virtual Employee Executor { get; set; }
        
        public int StageId { get; set; }
        public virtual ProjectStage Stage { get; set; }

        [NotMapped]
        public TaskStatusHistory Status => StatusHistory.OrderByDescending(x => x.Id).FirstOrDefault();
        public virtual List<TaskStatusHistory> StatusHistory { get; set; }

        public Task()
        {
            DateCreated = DateTime.UtcNow;
        }
    }
}
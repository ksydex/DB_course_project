using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ContractAndProjectManager.Infrastructure.Interfaces;

namespace ContractAndProjectManager.Entities
{
    public class ProjectStage : IWithDateCreated, IWithRecordDates
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateStart { get; set; }
        public DateTime? DateDeadLine { get; set; }
        public DateTime? DateEnd => ContractStage?.DateEnd;

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public int? ExecutorId { get; set; }
        public virtual Employee Executor { get; set; } 
        
        public virtual List<Task> Tasks { get; set; }
        
        public int ContractStageId { get; set; }
        public virtual ContractStage ContractStage { get; set; }
        
        [NotMapped] public string Key => ContractStageId +  Id.ToString(); 

        public ProjectStage()
        {
            DateCreated = DateTime.UtcNow;
        }
    }
}
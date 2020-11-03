using System;
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
        
        public DateTime DateStart { get; set; }
        public DateTime DateDeadLine { get; set; }
        public DateTime DateEnd { get; set; }

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }
        
        public int TeamLeadId { get; set; }
        public virtual TeamLead TeamLead { get; set; }
        
        public int? ExecutorId { get; set; }
        public virtual Employee Executor { get; set; } 

        public ProjectStage()
        {
            DateCreated = DateTime.UtcNow;
        }
    }
}
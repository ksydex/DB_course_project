using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ContractAndProjectManager.Infrastructure.Interfaces;

namespace ContractAndProjectManager.Entities
{
    public class ContractStage : IWithDateCreated, IWithTitleDescription, IWithRecordDates
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateDeadLine { get; set; }
        public DateTime? DateEnd { get; set;  }

        public int ContractId { get; set; }
        public virtual Contract Contract { get; set; }
        
        public virtual List<ProjectStage> ProjectStages { get; set; }

        public ContractStage()
        {
            DateCreated = DateTime.UtcNow;
        }
    }
}
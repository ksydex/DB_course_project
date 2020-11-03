using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ContractAndProjectManager.Infrastructure.Interfaces;

namespace ContractAndProjectManager.Entities
{
    public class Contract : IWithDateCreated, IWithRecordDates, IWithTitleDescription
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime DateCreated { get; set; }
        
        public DateTime DateStart { get; set; }
        public DateTime DateDeadLine { get; set; }
        public DateTime DateEnd { get; set; }

        public int RequestId { get; set; }
        public virtual Request Request { get; set; }
        
        public int StatusId { get; set; }
        public virtual ContractStatus ContractStatus { get; set; }

        public int PlannerId { get; set; }
        public virtual Planner Planner { get; set; }
        
        public virtual List<ContractStage> Stages { get; set; }

        public Contract()
        {
            DateCreated = DateTime.UtcNow;
        }
    }
}
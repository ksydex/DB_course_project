using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ContractAndProjectManager.Infrastructure.Interfaces;

namespace ContractAndProjectManager.Entities
{
    public class Contract : IWithDateCreated, IWithRecordDates, IWithTitleDescription,
        IWithStatusHistory<ContractStatusHistory, ContractStatus, Contract>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateStart { get; set; }
        public DateTime? DateDeadLine { get; set; }
        public DateTime? DateEnd { get; set; }

        public int RequestId { get; set; }
        public virtual Request Request { get; set; }

        [NotMapped]
        public ContractStatusHistory Status => StatusHistory.OrderByDescending(x => x.Id)
            .FirstOrDefault();

        public virtual List<ContractStatusHistory> StatusHistory { get; set; }

        public int PlannerId { get; set; }
        public virtual Planner Planner { get; set; }

        public virtual List<ContractStage> Stages { get; set; }

        public Contract()
        {
            DateCreated = DateTime.UtcNow;
        }
    }
}
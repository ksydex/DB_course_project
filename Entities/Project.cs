using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ContractAndProjectManager.Infrastructure.Interfaces;

namespace ContractAndProjectManager.Entities
{
    public class Project : IWithDateCreated, IWithRecordDates,
        IWithStatusHistory<ProjectStatusHistory, ProjectStatus, Project>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateDeadLine => Contract.DateDeadLine;
        public DateTime? DateEnd => Contract.DateEnd;

        public int ContractId { get; set; }
        public virtual Contract Contract { get; set; }

        public virtual List<ProjectStage> Stages { get; set; }

        [NotMapped]
        public ProjectStatusHistory Status => StatusHistory?.OrderByDescending(x => x.Id)
            .FirstOrDefault();
        public virtual List<ProjectStatusHistory> StatusHistory { get; set; }

        public int TeamId { get; set; }
        public virtual Team Team { get; set; }

        public Project()
        {
            DateCreated = DateTime.UtcNow;
        }
    }
}
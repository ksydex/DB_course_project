using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ContractAndProjectManager.Infrastructure.Interfaces;

namespace ContractAndProjectManager.Entities
{
    public class Project : IWithDateCreated, IWithRecordDates
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateDeadLine => Contract.DateDeadLine;
        public DateTime DateEnd { get; set; }
        
        public int ContractId { get; set; }
        public virtual Contract Contract { get; set; }
        
        public virtual List<ProjectStage> Stages { get; set; }
        
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
    }
}
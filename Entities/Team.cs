using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractAndProjectManager.Entities
{
    public class Team
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        
        public int LeadId { get; set; }
        public virtual TeamLead Lead { get; set; }
        
        public virtual List<EmployeeTeam> EmployeeTeams { get; set; }
    }
}
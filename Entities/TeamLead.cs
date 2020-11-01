using System.Collections.Generic;
using ContractAndProjectManager.Models;

namespace ContractAndProjectManager.Entities
{
    public class TeamLead : User
    {
        public virtual List<Team> Teams { get; set; }
        
        public TeamLead()
        {
            RoleId = Role.TeamLead.Id;
        }
    }
}
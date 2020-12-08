using System.Collections.Generic;
using ContractAndProjectManager.Models;

namespace ContractAndProjectManager.Entities
{
    public class TeamLead : User
    {
        public TeamLead()
        {
            RoleId = Role.TeamLead.Id;
        }
    }
}
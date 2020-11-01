using System.Collections.Generic;
using ContractAndProjectManager.Models;

namespace ContractAndProjectManager.Entities
{
    public class Planner : User
    {
        public virtual List<Contract> Contracts { get; set; }

        public Planner()
        {
            RoleId = Role.Planner.Id;
        }
    }
}
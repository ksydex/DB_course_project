using System.Collections.Generic;
using System.Linq;
using ContractAndProjectManager.Models;

namespace ContractAndProjectManager.Entities
{
    public class Employee : User
    {
        public virtual List<EmployeeTeam> EmployeeTeams { get; set; }
        public List<Team> Teams => EmployeeTeams.Select(x => x.Team).ToList();
        
        public Employee()
        {
            RoleId = Role.Employee.Id;
        }
    }
}
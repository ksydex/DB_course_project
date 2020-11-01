using System.Collections.Generic;
using ContractAndProjectManager.Models;

namespace ContractAndProjectManager.Entities
{
    public class Employee : User
    {
        public virtual List<EmployeeTeam> EmployeeTeams { get; set; }
        
        public Employee()
        {
            RoleId = Role.Employee.Id;
        }
    }
}
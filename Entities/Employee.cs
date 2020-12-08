using System.Collections.Generic;
using System.Linq;
using ContractAndProjectManager.Models;

namespace ContractAndProjectManager.Entities
{
    public class Employee : User
    {
        public Employee()
        {
            RoleId = Role.Employee.Id;
        }
    }
}
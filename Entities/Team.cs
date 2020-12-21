using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ContractAndProjectManager.Models;

namespace ContractAndProjectManager.Entities
{
    public class Team
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }

        [NotMapped] public TeamLead Lead => Members?.FirstOrDefault(x => x.RoleId == Role.TeamLead.Id) as TeamLead;

        [NotMapped]
        public List<Employee> Employees =>
            Members?.Where(x => x.RoleId == Role.Employee.Id).Select(x => x as Employee).ToList();
        
        public virtual List<User> Members { get; set; }

    }
}
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractAndProjectManager.Entities
{
    public class EmployeeTeam
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
    }
}
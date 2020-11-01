using System.ComponentModel.DataAnnotations.Schema;

namespace ContractAndProjectManager.Entities
{
    public class Role
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Key { get; set; }


        public static Role Customer = new Role { Id = 1, Key = Keys.Customer };
        public static Role Employee = new Role { Id = 2, Key = Keys.Employee };
        public static Role Planner = new Role { Id = 3, Key = Keys.Planner };
        public static Role TeamLead = new Role { Id = 4, Key = Keys.TeamLead };

        public static class Keys
        {
            public const string Customer = "cust";
            public const string Planner = "pl";
            public const string TeamLead = "tl";
            public const string Employee = "empl";
        }
    }
}
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractAndProjectManager.Entities
{
    public class Role
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }


        public static readonly Role Customer = new Role { Id = 1, Key = Keys.Customer, Name = "Заказчик"};
        public static readonly Role Employee = new Role { Id = 2, Key = Keys.Employee, Name = "Сотрудник" };
        public static readonly Role Planner = new Role { Id = 3, Key = Keys.Planner , Name ="Сотрудник отдела планирования"};
        public static readonly Role TeamLead = new Role { Id = 4, Key = Keys.TeamLead, Name = "Тимлид"};

        public static class Keys
        {
            public const string Customer = "cust";
            public const string Planner = "pl";
            public const string TeamLead = "tl";
            public const string Employee = "empl";
        }
    }
}
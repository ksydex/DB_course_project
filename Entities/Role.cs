using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ContractAndProjectManager.Models;

namespace ContractAndProjectManager.Entities
{
  public class Role
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; }
    public string Key { get; set; }

    [NotMapped] public RouteValue LogInRouteValues { get; set; }


    public static readonly Role Customer = new Role
    {
      Id = 1, Key = Keys.Customer, Name = "Заказчик", LogInRouteValues = new RouteValue { Area = "Customer", Controller = "Home" }
    };

    public static readonly Role Employee = new Role
    {
      Id = 2, Key = Keys.Employee, Name = "Сотрудник", LogInRouteValues = new RouteValue { Area = "Employee", Controller = "Home" }
    };

    public static readonly Role Planner = new Role
    {
      Id = 3, Key = Keys.Planner, Name = "Сотрудник отдела планирования",
      LogInRouteValues = new RouteValue { Area = "Planner", Controller = "Home" }
    };

    public static readonly Role TeamLead = new Role
    {
      Id = 4, Key = Keys.TeamLead, Name = "Тимлид", LogInRouteValues = new RouteValue { Area = "TeamLead", Controller = "Home" }
    };
    
    public static List<Role> All = new List<Role>
    {
        Customer,
        Employee,
        Planner,
        TeamLead
    };

    public static class Keys
    {
      public const string Customer = "cust";
      public const string Planner = "pl";
      public const string TeamLead = "tl";
      public const string Employee = "empl";
    }
  }
}
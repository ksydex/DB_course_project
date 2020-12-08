using ContractAndProjectManager.Entities;
using ContractAndProjectManager.Extensions;
using ContractAndProjectManager.Infrastructure.EntityConfigurations;
using ContractAndProjectManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractAndProjectManager.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<TeamLead> TeamLeads { get; set; }
        public DbSet<Planner> Planners { get; set; }
        public DbSet<Customer> Customers { get; set; }
        
        public DbSet<Role> Roles { get; set; }
        
        public DbSet<Team> Teams { get; set; }

        public DbSet<Request> Requests { get; set; }
        
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractStage> ContractStages { get; set; }
        
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectStage> ProjectStages { get; set; }
        
        public DbSet<Task> Tasks { get; set; }

        public DbSet<Status> Statuses { get; set; }
        public DbSet<TaskStatus> TaskStatuses { get; set; }
        public DbSet<ProjectStatus> ProjectStatuses { get; set; }
        public DbSet<RequestStatus> RequestStatuses { get; set; }
        public DbSet<ContractStatus> ContractStatuses { get; set; }
        public DbSet<RequestStatusHistory> RequestStatusHistories { set; get; }
        public DbSet<ContractStatusHistory> ContractStatusHistories { get; set; }
        public DbSet<ProjectStatusHistory> ProjectStatusHistories { get; set; }
        public DbSet<TaskStatusHistory> TaskStatusHistories { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ConfigureStatusEntities();
            modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
        }
    }
}
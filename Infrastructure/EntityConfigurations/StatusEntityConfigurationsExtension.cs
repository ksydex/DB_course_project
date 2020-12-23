using ContractAndProjectManager.Entities;
using ContractAndProjectManager.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace ContractAndProjectManager.Infrastructure.EntityConfigurations
{
    public static class StatusEntityConfigurationsExtension
    {
        public static void ConfigureStatusEntities(this ModelBuilder builder)
        {
            builder.ApplyConfiguration(new RequestStatusEntityConfiguration());
            builder.ApplyConfiguration(new TaskStatusEntityConfiguration());
            builder.ApplyConfiguration(new ContractStatusEntityConfiguration());
            builder.ApplyConfiguration(new ProjectStatusEntityConfiguration());
        }
    }
    
}
using ContractAndProjectManager.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContractAndProjectManager.Infrastructure.EntityConfigurations
{
    public static class AnalyticsEntitiesConfiguration
    {
        public static void ConfigureViews(this ModelBuilder builder)
        {
            builder.Entity<ContractAnalytics>().HasNoKey()
                .ToView(nameof(ContractAnalytics));
            
            builder.Entity<RequestAnalytics>().HasNoKey()
                .ToView(nameof(RequestAnalytics));
            
            builder.Entity<ProjectAnalytics>().HasNoKey()
                .ToView(nameof(ProjectAnalytics));

            builder.Entity<ProjectStageAnalytics>().HasNoKey()
                .ToView(nameof(ProjectStageAnalytics));
        }
    }
}
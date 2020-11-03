using System.Collections.Generic;
using ContractAndProjectManager.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContractAndProjectManager.Infrastructure.EntityConfigurations
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
    {
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(new List<Role>
        {
            Role.Customer, 
            Role.Employee,
            Role.Planner,
            Role.TeamLead
        });
    }  
    }
}
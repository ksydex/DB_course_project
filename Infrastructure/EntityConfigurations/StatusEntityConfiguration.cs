using System.Collections.Generic;
using ContractAndProjectManager.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContractAndProjectManager.Infrastructure.EntityConfigurations
{
    public class RequestStatusEntityConfiguration : IEntityTypeConfiguration<RequestStatus>
    {
        public void Configure(EntityTypeBuilder<RequestStatus> builder)
        {
            builder.HasData(new List<RequestStatus>
            {
                RequestStatus.Completed,
                RequestStatus.Denied,
                RequestStatus.Pending,
            });
        }
    }
    
    public class TaskStatusEntityConfiguration : IEntityTypeConfiguration<TaskStatus>
    {
        public void Configure(EntityTypeBuilder<TaskStatus> builder)
        {
            builder.HasData(new List<TaskStatus>
            {
                TaskStatus.Completed,
                TaskStatus.Developing,
                TaskStatus.Pending
            });
        }
    }
    
    public class ProjectStatusEntityConfiguration : IEntityTypeConfiguration<ProjectStatus>
    {
        public void Configure(EntityTypeBuilder<ProjectStatus> builder)
        {
            builder.HasData(new List<ProjectStatus>
            {
                ProjectStatus.Completed,
                ProjectStatus.Active,
                ProjectStatus.Denied
            });
        }
    }
    
    public class ContractStatusEntityConfiguration : IEntityTypeConfiguration<ContractStatus>
    {
        public void Configure(EntityTypeBuilder<ContractStatus> builder)
        {
            builder.HasData(new List<ContractStatus>
            {
                ContractStatus.Completed,
                ContractStatus.Developing,
                ContractStatus.Denied,
                ContractStatus.Pending
            });
        }
    }
}
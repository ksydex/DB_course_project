using System.Collections.Generic;
using ContractAndProjectManager.Entities;
using ContractAndProjectManager.Models;
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
                RequestStatus.Developing,
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
}
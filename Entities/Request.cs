using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ContractAndProjectManager.Infrastructure.Interfaces;
using ContractAndProjectManager.Models;

namespace ContractAndProjectManager.Entities
{
    public class Request : IWithDateCreated, IWithTitleDescription
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
        public double Price { get; set; }
        public DateTime DateDeadLine { get; set; }

        public DateTime DateCreated { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

        public Request()
        {
            DateCreated = DateTime.UtcNow;
            StatusId = RequestStatus.Pending.Id;
        }
    }
}
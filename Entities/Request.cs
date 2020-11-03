using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ContractAndProjectManager.Infrastructure.Interfaces;
using ContractAndProjectManager.Models;

namespace ContractAndProjectManager.Entities
{
    public class Request : IWithDateCreated
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

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
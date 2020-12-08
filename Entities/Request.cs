using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ContractAndProjectManager.Infrastructure.Interfaces;
using ContractAndProjectManager.Models;

namespace ContractAndProjectManager.Entities
{
    public class Request : IWithDateCreated, IWithTitleDescription,
        IWithStatusHistory<RequestStatusHistory, RequestStatus, Request>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public DateTime DateDeadLine { get; set; }

        public DateTime DateCreated { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        
        public virtual Contract Contract { get; set; }

        [NotMapped]
        public RequestStatusHistory Status => StatusHistory?.OrderByDescending(x => x.Id)
            .FirstOrDefault();

        public virtual List<RequestStatusHistory> StatusHistory { get; set; }

        public Request()
        {
            DateCreated = DateTime.UtcNow;
        }
    }
}
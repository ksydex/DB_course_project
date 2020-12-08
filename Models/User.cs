using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ContractAndProjectManager.Entities;
using ContractAndProjectManager.Infrastructure.Interfaces;

namespace ContractAndProjectManager.Models
{
    public class User : IWithDateCreated
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }

        public DateTime DateCreated { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        
        public int? TeamId { get; set; }
        public virtual Team Team { get; set; }

        public User()
        {
            DateCreated = DateTime.UtcNow;
        }
    }
}
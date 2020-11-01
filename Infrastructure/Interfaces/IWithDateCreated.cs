using System;
using ContractAndProjectManager.Entities;

namespace ContractAndProjectManager.Infrastructure.Interfaces
{
    public interface IWithDateCreated
    {
        public DateTime DateCreated { get; set; }
    }
}
using System;

namespace ContractAndProjectManager.Infrastructure.Interfaces
{
    public interface IWithDateDeadLineAndEnd
    {
        public DateTime DateDeadLine { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
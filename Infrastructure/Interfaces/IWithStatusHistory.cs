using System.Collections.Generic;
using System.Linq;
using ContractAndProjectManager.Entities;
using ContractAndProjectManager.Infrastructure.AbstractClasses;
using ContractAndProjectManager.Models;

namespace ContractAndProjectManager.Infrastructure.Interfaces
{
    public interface IWithStatusHistory<T, T2, T3>
    where T: StatusHistory<T2, T3>
    where T2: Status
    where T3: class
    {
        T Status { get; }
        List<T> StatusHistory { get; set; }
    }
}
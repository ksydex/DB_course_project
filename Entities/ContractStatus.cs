using ContractAndProjectManager.Infrastructure.AbstractClasses;
using ContractAndProjectManager.Models;

namespace ContractAndProjectManager.Entities
{
    public class ContractStatus : Status
    {
        public static ContractStatus Pending = new ContractStatus
        {
            Id = 8,
            Name = "Ожидание работ"
        };

        public static ContractStatus InWork = new ContractStatus
        {
            Id = 9,
            Name = "В работе"
        };

        public static ContractStatus Completed = new ContractStatus
        {
            Id = 10,
            Name = "Завершен"
        };

        public static ContractStatus Denied = new ContractStatus
        {
            Id = 11,
            Name = "Отклонён"
        };
    }
    
    public class ContractStatusHistory : StatusHistory<ContractStatus, Contract> {}
}
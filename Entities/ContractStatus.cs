using ContractAndProjectManager.Infrastructure.AbstractClasses;
using ContractAndProjectManager.Models;

namespace ContractAndProjectManager.Entities
{
    public class ContractStatus : Status
    {
        public static ContractStatus Pending = new ContractStatus
        {
            Id = 8,
            Name = "Ожидание работ",
            Color = "#ffc107"
        };

        public static ContractStatus InWork = new ContractStatus
        {
            Id = 9,
            Name = "В работе",
            Color = "#0052cc"
        };

        public static ContractStatus Completed = new ContractStatus
        {
            Id = 10,
            Name = "Завершен",
            Color = "#36b37e"
        };

        public static ContractStatus Denied = new ContractStatus
        {
            Id = 11,
            Name = "Отклонён",
            Color = "#dc3545"
        };
    }
    
    public class ContractStatusHistory : StatusHistory<ContractStatus, Contract> {}
    
    // DROP TRIGGER IF EXISTS Contract_INSERT
    // ON "Contracts";
    // CREATE TRIGGER Contract_INSERT
    // AFTER INSERT ON "Contracts"
    // FOR EACH ROW EXECUTE PROCEDURE add_status_to_entity_trigger("Contract", 8);
}
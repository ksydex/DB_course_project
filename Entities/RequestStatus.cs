using ContractAndProjectManager.Infrastructure.AbstractClasses;
using ContractAndProjectManager.Models;

namespace ContractAndProjectManager.Entities
{
    public class RequestStatus : Status
    
    {
        public static RequestStatus Pending = new RequestStatus
        {
            Id = 1,
            Name = "Ожидание обработки",
            Color = "#ffc107"
        };
        
        public static RequestStatus InWork = new RequestStatus
        {
            Id = 15,
            Name = "В работе",
            Color = "#0052cc"
        };

        public static RequestStatus Denied = new RequestStatus
        {
            Id = 3,
            Name = "Отказано",
            Color = "#dc3545"
        };

        public static RequestStatus Completed = new RequestStatus
        {
            Id = 7,
            Name = "Завершен",
            Color = "#36b37e"
        };
    }
    
    public class RequestStatusHistory: StatusHistory<RequestStatus, Request> {}
}
using ContractAndProjectManager.Infrastructure.AbstractClasses;
using ContractAndProjectManager.Models;

namespace ContractAndProjectManager.Entities
{
    public class RequestStatus : Status
    
    {
        public static RequestStatus Pending = new RequestStatus
        {
            Id = 1,
            Name = "В обработке"
        };
        
        public static RequestStatus InWork = new RequestStatus
        {
            Id = 15,
            Name = "В работе"
        };
        

        public static RequestStatus Denied = new RequestStatus
        {
            Id = 3,
            Name = "Отказано"
        };

        public static RequestStatus Completed = new RequestStatus
        {
            Id = 7,
            Name = "Завершен"
        };
    }
    
    public class RequestStatusHistory: StatusHistory<RequestStatus, Request> {}
}
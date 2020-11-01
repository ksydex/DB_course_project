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

        public static RequestStatus Developing = new RequestStatus
        {
            Id = 2,
            Name = "В разработке"
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
}
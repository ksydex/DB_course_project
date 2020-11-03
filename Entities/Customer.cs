using System.Collections.Generic;
using ContractAndProjectManager.Models;

namespace ContractAndProjectManager.Entities
{
    public class Customer : User
    {
        
        public virtual List<Request> Requests { get; set; }
        public Customer()
        {
            RoleId = Role.Customer.Id;
        }
    }
}
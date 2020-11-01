using ContractAndProjectManager.Models;

namespace ContractAndProjectManager.Entities
{
    public class Customer : User
    {
        public Customer()
        {
            RoleId = Role.Customer.Id;
        }
    }
}
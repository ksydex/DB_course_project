using System.ComponentModel.DataAnnotations;

namespace ContractAndProjectManager.Areas.Auth.Models
{
    public class LogInModel
    {
        private string _email;
        
        [EmailAddress]
        public string Email
        {
            get => _email;
            set => _email = value.ToLower();
        }
        public string Password { get; set; }
    }
}
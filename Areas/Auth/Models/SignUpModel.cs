using System.ComponentModel.DataAnnotations;

namespace ContractAndProjectManager.Areas.Auth.Models
{
    public class SignUpModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        public int RoleId { get; set; }
    }
}
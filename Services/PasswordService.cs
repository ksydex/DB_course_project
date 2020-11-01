using ContractAndProjectManager.Entities;
using ContractAndProjectManager.Helpers;
using ContractAndProjectManager.Models;
using Microsoft.AspNetCore.Identity;

namespace ContractAndProjectManager.Services
{
    public sealed class PasswordService : IPasswordHasher<User>
    {
        public string HashPassword(User user, string password)
        {
            return Hasher.Hash(password);
        }

        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword,
            string providedPassword)
        {
            return Hasher.Verify(hashedPassword, providedPassword)
                ? PasswordVerificationResult.Success
                : PasswordVerificationResult.Failed;
        }
    }
}
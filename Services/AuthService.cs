using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ContractAndProjectManager.Data;
using ContractAndProjectManager.Entities;
using ContractAndProjectManager.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ContractAndProjectManager.Services
{
    public class AuthService
    {
        private readonly ApplicationContext _db;
        private readonly PasswordService _passwordService;
        private readonly HttpContext _httpContext;

        public AuthService(ApplicationContext db, PasswordService passwordService, HttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _passwordService = passwordService;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<User> AuthenticateCookies(string email, string password)
        {
            var user = await VerifyUser(email, password);
            if (user == null) return null;
      
            var claimsIdentity = GenerateClaimsIdentity(user);
            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.Now.AddDays(7),
            };
            
            // TODO : implement
            await _httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return user;
        }
        
        private async Task<User> VerifyUser(string email, string password)
        {
            email = email.ToLower();

            var user = await _db.Users.SingleOrDefaultAsync(u => u.Email == email);
            if (user == null) return null;

            
            var passwordVerify = _passwordService.VerifyHashedPassword(user, user.Password, password);
            return passwordVerify == PasswordVerificationResult.Failed ? null : user;
        }

        private ClaimsIdentity GenerateClaimsIdentity(User user)
        {
            var claims = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.Key),
            }, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return claims;
        }
    }
}
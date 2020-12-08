using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContractAndProjectManager.Areas.Auth.Models;
using ContractAndProjectManager.Data;
using ContractAndProjectManager.Entities;
using ContractAndProjectManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TaskStatus = System.Threading.Tasks.TaskStatus;

namespace ContractAndProjectManager.Services
{
    public class UserService
    {
        private readonly ApplicationContext _db;
        private readonly PasswordService _passwordService;
        private readonly HttpContext _httpContext;

        public UserService(ApplicationContext db, PasswordService passwordService,
            IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _passwordService = passwordService;
            _httpContext = httpContextAccessor.HttpContext;
        }

        #region [ User context implementation ]

        private User _user;

        public User User => _user ??= _httpContext.User.Identity.IsAuthenticated
            ? _db.Users.FirstOrDefault(u => u.Id == int.Parse(_httpContext.User.Identity.Name))
            : null;

        public int UserId => _httpContext.User.Identity.Name != null
            ? int.Parse(_httpContext.User.Identity.Name)
            : default;

        #endregion


        public async Task<User> Create(SignUpModel model)
        {
            if (await _db.Users.AnyAsync(x => x.Email == model.Email))
                return null;

            try
            {
                var role = await _db.Roles.FirstOrDefaultAsync(x => x.Id == model.RoleId);
                if (role == null)
                    return null;

                User user;

                if (role.Id == Role.Customer.Id)
                    user = new Customer();
                else if (role.Id == Role.Employee.Id)
                    user = new Employee();
                else if (role.Id == Role.TeamLead.Id)
                    user = new TeamLead();
                else if (role.Id == Role.Planner.Id)
                    user = new Planner();
                else
                    return null;

                user.Email = model.Email;
                user.Name = model.Name;
                user.RoleId = role.Id;
                user.Password = _passwordService.HashPassword(user, model.Password);

                await _db.AddAsync(user);

                await _db.SaveChangesAsync();
                
                
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
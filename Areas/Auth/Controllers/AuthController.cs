using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ContractAndProjectManager.Areas.Auth.Models;
using ContractAndProjectManager.Data;
using ContractAndProjectManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;

namespace ContractAndProjectManager.Areas.Auth.Controllers
{
    [AllowAnonymous]
    [Area("Auth")]
    public class AuthController : Controller
    {
        private readonly AuthService _authService;
        private readonly ApplicationContext _applicationContext;
        private readonly UserService _userService;

        public AuthController(AuthService authService, ApplicationContext applicationContext, UserService userService)
        {
            _authService = authService;
            _applicationContext = applicationContext;
            _userService = userService;
        }

        [HttpGet]
        [Route("/")]
        [Route("[area]/[controller]/[action]")]
        public IActionResult LogIn()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LogInModel model)
        {
            var user = await _authService.Authenticate(model);
            if (user != null)
                return RedirectToAction("Index", new { Area="Project", Controller = "Home" });

            ModelState.AddModelError("", "Неправильный Email или пароль");
            return View(model);
        }

        [HttpGet]
        public IActionResult SignUp(SignUpModel model = null)
        {
            ViewData["Roles"] = new SelectList(_applicationContext.Roles, "Id", "Name");
            if(model?.Email == null)
                ModelState.Clear();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel model, bool tmp=false)
        {
            if (!ModelState.IsValid)
                return SignUp(model);
            

            try
            {
                var user = await _userService.Create(model);
                if (user != null)
                    return RedirectToAction("LogIn");

                ModelState.AddModelError("", "Этот Email уже занят");
                return SignUp(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Произошла непредвиденная ошибка");
                return  SignUp(model);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _authService.LogOut();
            return RedirectToAction("LogIn");
        }
    }
}
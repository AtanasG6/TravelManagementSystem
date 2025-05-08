using Microsoft.AspNetCore.Mvc;
using TravelManagementSystem.MVC.Models.User.ViewModels;
using TravelManagementSystem.MVC.Services;

namespace TravelManagementSystem.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApiService _apiService;

        public AccountController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _apiService.LoginAsync(model);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message ?? "Грешка при вход.");
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error);
                }
                return View(model);
            }

            return RedirectToAction("Index", "Trips");
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterViewModel
            {
                DateOfBirth = new DateTime(2000, 1, 1)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _apiService.RegisterAsync(model);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message ?? "Грешка при регистрация.");
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error);
                }
                return View(model);
            }

            return RedirectToAction("Index", "Trips");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            _apiService.Logout();
            return RedirectToAction("Login");
        }
    }
}

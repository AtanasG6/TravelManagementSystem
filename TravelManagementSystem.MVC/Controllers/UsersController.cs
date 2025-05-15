using Microsoft.AspNetCore.Mvc;
using TravelManagementSystem.MVC.Filters;
using TravelManagementSystem.MVC.Models.User.Parameters;
using TravelManagementSystem.MVC.Models.User.ViewModels;
using TravelManagementSystem.MVC.Services.Interfaces;
using X.PagedList;

namespace TravelManagementSystem.MVC.Controllers
{
    [RequireJwt]
    public class UsersController : Controller
    {
        private readonly IApiService _apiService;

        public UsersController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] UserFilterParameters filters)
        {
            var queryBuilder = new List<string>();

            if (!string.IsNullOrWhiteSpace(filters.Username))
                queryBuilder.Add($"username={Uri.EscapeDataString(filters.Username)}");

            if (!string.IsNullOrWhiteSpace(filters.Email))
                queryBuilder.Add($"email={Uri.EscapeDataString(filters.Email)}");

            if (!string.IsNullOrWhiteSpace(filters.OrderBy))
                queryBuilder.Add($"orderBy={filters.OrderBy}&isDescending={filters.IsDescending}");

            queryBuilder.Add($"page={filters.Page}");
            queryBuilder.Add($"pageSize={filters.PageSize}");

            var query = "users?" + string.Join("&", queryBuilder);

            var result = await _apiService.GetPagedAsync<UserViewModel>(query);

            StaticPagedList<UserViewModel> pagedList;

            if (!result.Success || result.Data == null)
            {
                pagedList = new StaticPagedList<UserViewModel>(new List<UserViewModel>(), filters.Page, filters.PageSize, 0);
                ViewBag.Error = result.Message ?? "Грешка при зареждане на потребителите.";
            }
            else
            {
                pagedList = new StaticPagedList<UserViewModel>(
                    result.Data,
                    result.PageNumber,
                    result.PageSize,
                    result.TotalItemCount
                );
            }

            ViewBag.Filters = filters;
            return View(pagedList);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _apiService.GetAsync<UserViewModel>($"users/{id}");

            if (!result.Success || result.Data == null)
            {
                TempData["Error"] = result.Message ?? "Потребителят не беше намерен.";
                return RedirectToAction("Index");
            }

            return View(result.Data);
        }

        [HttpGet]
        public IActionResult Create() => View(new CreateUserViewModel
        {
            DateOfBirth = new DateTime(2000, 1, 1)
        });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (model.DateOfBirth >= DateTime.Today)
                ModelState.AddModelError(nameof(model.DateOfBirth), "Датата на раждане трябва да е в миналото.");

            if (!ModelState.IsValid)
                return View(model);

            var response = await _apiService.PostAsync<object>("users", model);

            if (!response.Success)
            {
                if (response.Errors != null)
                {
                    foreach (var kvp in response.Errors)
                    {
                        foreach (var msg in kvp.Value)
                        {
                            ModelState.AddModelError(kvp.Key, msg);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, response.Message ?? "Възникна грешка.");
                }

                return View(model);
            }

            TempData["Success"] = "Потребителят беше добавен успешно.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _apiService.GetAsync<EditUserViewModel>($"users/{id}");

            if (!result.Success || result.Data == null)
            {
                TempData["Error"] = result.Message ?? "Потребителят не може да бъде зареден.";
                return RedirectToAction("Index");
            }

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (model.DateOfBirth >= DateTime.Today)
                ModelState.AddModelError(nameof(model.DateOfBirth), "Датата на раждане трябва да е в миналото.");

            if (!ModelState.IsValid)
                return View(model);

            var result = await _apiService.PutAsync<object>($"users/{model.Id}", model);

            if (!result.Success)
            {
                if (result.Errors != null)
                {
                    foreach (var kvp in result.Errors)
                    {
                        foreach (var msg in kvp.Value)
                        {
                            ModelState.AddModelError(kvp.Key, msg);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Message ?? "Възникна грешка при обновяване.");
                }

                return View(model);
            }

            TempData["Success"] = "Потребителят беше успешно обновен.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _apiService.DeleteAsync<object>($"users/{id}");

            if (!result.Success)
            {
                TempData["Error"] = result.Message ?? "Възникна грешка при изтриването на потребителя.";
            }
            else
            {
                TempData["Success"] = "Потребителят беше изтрит успешно.";
            }

            return RedirectToAction("Index");
        }
    }
}

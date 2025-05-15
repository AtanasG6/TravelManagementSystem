using Microsoft.AspNetCore.Mvc;
using TravelManagementSystem.MVC.Filters;
using TravelManagementSystem.MVC.Models.Destination.External;
using TravelManagementSystem.MVC.Models.Destination.Parameters;
using TravelManagementSystem.MVC.Models.Destination.ViewModels;
using TravelManagementSystem.MVC.Models.Shared.ExternalData;
using TravelManagementSystem.MVC.Services;
using X.PagedList;

namespace TravelManagementSystem.MVC.Controllers
{
    [RequireJwt]
    public class DestinationsController : Controller
    {
        private readonly ApiService _apiService;

        public DestinationsController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] DestinationFilterParameters filters)
        {
            var queryBuilder = new List<string>();

            if (!string.IsNullOrWhiteSpace(filters.Country))
                queryBuilder.Add($"country={Uri.EscapeDataString(filters.Country)}");

            if (!string.IsNullOrWhiteSpace(filters.City))
                queryBuilder.Add($"city={Uri.EscapeDataString(filters.City)}");

            if (!string.IsNullOrWhiteSpace(filters.OrderBy))
                queryBuilder.Add($"orderBy={filters.OrderBy}&isDescending={filters.IsDescending}");

            queryBuilder.Add($"page={filters.Page}");
            queryBuilder.Add($"pageSize={filters.PageSize}");

            var query = "destinations?" + string.Join("&", queryBuilder);

            var result = await _apiService.GetPagedAsync<DestinationViewModel>(query);

            StaticPagedList<DestinationViewModel> pagedList;

            if (!result.Success || result.Data == null)
            {
                pagedList = new StaticPagedList<DestinationViewModel>(new List<DestinationViewModel>(), filters.Page, filters.PageSize, 0);
                ViewBag.Error = result.Message ?? "Грешка при зареждане на дестинациите.";
            }
            else
            {
                pagedList = new StaticPagedList<DestinationViewModel>(
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
            var response = await _apiService.GetAsync<DestinationViewModel>($"destinations/{id}");

            if (!response.Success || response.Data == null)
            {
                TempData["Error"] = response.Message ?? "Дестинацията не беше намерена.";
                return RedirectToAction("Index");
            }

            return View(response.Data);
        }

        [HttpGet]
        public IActionResult Create() => View(new CreateDestinationViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDestinationViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _apiService.PostAsync<object>("destinations", model);

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
                    ModelState.AddModelError(string.Empty, response.Message ?? "Грешка при създаване.");
                }

                return View(model);
            }

            TempData["Success"] = "Дестинацията беше добавена успешно!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _apiService.GetAsync<EditDestinationViewModel>($"destinations/{id}");

            if (!result.Success || result.Data == null)
            {
                TempData["Error"] = result.Message ?? "Дестинацията не беше намерена.";
                return RedirectToAction("Index");
            }

            return View(result.Data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditDestinationViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _apiService.PutAsync<object>($"destinations/{model.Id}", model);

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
                    ModelState.AddModelError(string.Empty, result.Message ?? "Възникна грешка при редакцията.");
                }

                return View(model);
            }

            TempData["Success"] = "Дестинацията беше успешно обновена.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _apiService.DeleteAsync<object>($"destinations/{id}");

            if (!response.Success)
            {
                TempData["Error"] = response.Message ?? "Грешка при изтриване на дестинацията.";
            }
            else
            {
                TempData["Success"] = "Дестинацията беше изтрита успешно.";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Info(string country, string city)
        {
            var weather = await _apiService.GetAsync<WeatherInfoViewModel>($"external/weather/{city}");
            var countryInfo = await _apiService.GetAsync<CountryInfoViewModel>($"external/country/{country}");

            if (!weather.Success || !countryInfo.Success || weather.Data == null || countryInfo.Data == null)
            {
                ViewBag.Error = "Неуспешно зареждане на външна информация.";
                return View();
            }

            var model = new DestinationExternalViewModel
            {
                WeatherInfo = weather.Data,
                CountryInfo = countryInfo.Data
            };

            return View(model);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using TravelManagementSystem.MVC.Filters;
using TravelManagementSystem.MVC.Models.Destination.ViewModels;
using TravelManagementSystem.MVC.Models.Trip.Parameters;
using TravelManagementSystem.MVC.Models.Trip.ViewModels;
using TravelManagementSystem.MVC.Services.Interfaces;
using X.PagedList;

namespace TravelManagementSystem.MVC.Controllers
{
    [RequireJwt]
    public class TripsController : Controller
    {
        private readonly IApiService _apiService;

        public TripsController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] TripFilterParameters filters)
        {
            var queryBuilder = new List<string>();

            if (!string.IsNullOrWhiteSpace(filters.Title))
                queryBuilder.Add($"title={Uri.EscapeDataString(filters.Title)}");

            if (filters.PriceMin.HasValue)
                queryBuilder.Add($"priceMin={filters.PriceMin.Value}");

            if (filters.PriceMax.HasValue)
                queryBuilder.Add($"priceMax={filters.PriceMax.Value}");

            if (!string.IsNullOrWhiteSpace(filters.OrderBy))
                queryBuilder.Add($"orderBy={filters.OrderBy}&isDescending={filters.IsDescending}");

            queryBuilder.Add($"page={filters.Page}");
            queryBuilder.Add($"pageSize={filters.PageSize}");

            var query = "trips?" + string.Join("&", queryBuilder);

            var result = await _apiService.GetPagedAsync<TripViewModel>(query);

            StaticPagedList<TripViewModel> pagedList;

            if (!result.Success || result.Data == null)
            {
                pagedList = new StaticPagedList<TripViewModel>(new List<TripViewModel>(), filters.Page, filters.PageSize, 0);
                ViewBag.Error = result.Message ?? "Грешка при зареждане на пътуванията.";
            }
            else
            {
                pagedList = new StaticPagedList<TripViewModel>(
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
            var result = await _apiService.GetAsync<TripViewModel>($"trips/{id}");

            if (!result.Success || result.Data == null)
            {
                TempData["Error"] = result.Message ?? "Пътуването не беше намерено.";
                return RedirectToAction("Index");
            }

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var response = await _apiService.GetAsync<List<DestinationViewModel>>("destinations");

            if (!response.Success || response.Data == null)
            {
                ViewBag.Error = "Неуспешно зареждане на дестинации.";
                return View(new CreateTripViewModel());
            }

            var model = new CreateTripViewModel
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
                Destinations = response.Data
                    .Select(d => new DestinationDropdownItem
                    {
                        Id = d.Id,
                        DisplayName = $"{d.City}, {d.Country}"
                    })
                    .ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTripViewModel model)
        {
            if (model.EndDate <= model.StartDate)
                ModelState.AddModelError("EndDate", "Крайната дата трябва да е след началната дата.");

            if (!ModelState.IsValid)
            {
                var destinations = await _apiService.GetAsync<List<DestinationViewModel>>("destinations");
                model.Destinations = destinations.Data?
                    .Select(d => new DestinationDropdownItem
                    {
                        Id = d.Id,
                        DisplayName = $"{d.City}, {d.Country}"
                    }).ToList() ?? new();

                return View(model);
            }

            var createDto = new
            {
                model.Title,
                model.StartDate,
                model.EndDate,
                model.Price,
                model.DestinationId
            };

            var result = await _apiService.PostAsync<object>("trips", createDto);

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
                    ModelState.AddModelError(string.Empty, result.Message ?? "Грешка при създаване на пътуване.");
                }

                return View(model);
            }

            TempData["Success"] = "Пътуването беше създадено успешно.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var tripResponse = await _apiService.GetAsync<TripViewModel>($"trips/{id}");
            var destinationsResponse = await _apiService.GetAsync<List<DestinationViewModel>>("destinations");

            if (!tripResponse.Success || tripResponse.Data == null)
            {
                TempData["Error"] = tripResponse.Message ?? "Пътуването не беше намерено.";
                return RedirectToAction("Index");
            }

            var model = new EditTripViewModel
            {
                Id = tripResponse.Data.Id,
                Title = tripResponse.Data.Title,
                StartDate = tripResponse.Data.StartDate,
                EndDate = tripResponse.Data.EndDate,
                Price = tripResponse.Data.Price,
                DestinationId = tripResponse.Data.DestinationId,
                Destinations = destinationsResponse.Data?.Select(d => new DestinationDropdownItem
                {
                    Id = d.Id,
                    DisplayName = $"{d.City}, {d.Country}"
                }).ToList() ?? new()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditTripViewModel model)
        {
            if (model.EndDate <= model.StartDate)
                ModelState.AddModelError("EndDate", "Крайната дата трябва да е след началната.");

            if (!ModelState.IsValid)
            {
                var dests = await _apiService.GetAsync<List<DestinationViewModel>>("destinations");
                model.Destinations = dests.Data?
                    .Select(d => new DestinationDropdownItem
                    {
                        Id = d.Id,
                        DisplayName = $"{d.City}, {d.Country}"
                    }).ToList() ?? new();

                return View(model);
            }

            var updateDto = new
            {
                model.Title,
                model.StartDate,
                model.EndDate,
                model.Price,
                model.DestinationId
            };

            var result = await _apiService.PutAsync<object>($"trips/{model.Id}", updateDto);

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
                    ModelState.AddModelError(string.Empty, result.Message ?? "Грешка при редактиране.");
                }

                return View(model);
            }

            TempData["Success"] = "Пътуването беше редактирано успешно.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _apiService.DeleteAsync<object>($"trips/{id}");

            if (!result.Success)
            {
                TempData["Error"] = result.Message ?? "Неуспешно изтриване.";
            }
            else
            {
                TempData["Success"] = "Пътуването беше изтрито успешно.";
            }

            return RedirectToAction("Index");
        }
    }
}

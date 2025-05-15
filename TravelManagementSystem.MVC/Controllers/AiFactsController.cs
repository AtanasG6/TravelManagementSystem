using Microsoft.AspNetCore.Mvc;
using TravelManagementSystem.MVC.Services;

namespace TravelManagementSystem.MVC.Controllers
{
    public class AiFactsController : Controller
    {
        private readonly AiService _aiService;

        public AiFactsController(AiService aiService)
        {
            _aiService = aiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? destinationName)
        {
            if (!string.IsNullOrWhiteSpace(destinationName))
            {
                var facts = await _aiService.GetInterestingFactsAsync(destinationName);
                ViewBag.Facts = facts;
                ViewBag.DestinationName = destinationName;
            }

            return View();
        }
    }
}

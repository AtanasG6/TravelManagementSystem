namespace TravelManagementSystem.MVC.Models.Trip.Parameters
{
    public class TripFilterParameters
    {
        public string? Title { get; set; }

        public decimal? PriceMin { get; set; }

        public decimal? PriceMax { get; set; }

        public string? OrderBy { get; set; }

        public bool IsDescending { get; set; } = false;

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }

}

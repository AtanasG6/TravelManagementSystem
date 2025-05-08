namespace TravelManagementSystem.MVC.Models.Destination.Parameters
{
    public class DestinationFilterParameters
    {
        public string? Country { get; set; }
        public string? City { get; set; }

        public string? OrderBy { get; set; }
        public bool IsDescending { get; set; } = false;

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

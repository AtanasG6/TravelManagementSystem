namespace TravelManagementSystem.MVC.Models.User.Parameters
{
    public class UserFilterParameters
    {
        public string? Username { get; set; }
        public string? Email { get; set; }

        public string? OrderBy { get; set; }
        public bool IsDescending { get; set; } = false;

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

namespace TravelManagementSystem.MVC.Services.Interfaces
{
    public interface IAiService
    {
        Task<string> GetInterestingFactsAsync(string destinationName);
    }
}

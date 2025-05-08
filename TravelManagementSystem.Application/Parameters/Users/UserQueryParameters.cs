using TravelManagementSystem.Application.Parameters.Base;

namespace TravelManagementSystem.Application.Parameters.Users
{
    public class UserQueryParameters : QueryParameters
    {
        public string? Username { get; set; }
        public string? Email { get; set; }

        public Dictionary<string, object?> ToFilterDictionary()
        {
            var filters = new Dictionary<string, object?>();

            if (!string.IsNullOrWhiteSpace(Username))
                filters[nameof(Username)] = Username;

            if (!string.IsNullOrWhiteSpace(Email))
                filters[nameof(Email)] = Email;

            return filters;
        }
    }
}

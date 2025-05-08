using TravelManagementSystem.Application.Parameters.Base;

namespace TravelManagementSystem.Application.Parameters.Destinations
{
    public class DestinationQueryParameters : QueryParameters
    {
        public string? Country { get; set; }
        public string? City { get; set; }

        public Dictionary<string, object?> ToFilterDictionary()
        {
            var filters = new Dictionary<string, object?>();

            if (!string.IsNullOrWhiteSpace(Country))
                filters[nameof(Country)] = Country;

            if (!string.IsNullOrWhiteSpace(City))
                filters[nameof(City)] = City;

            return filters;
        }
    }

}

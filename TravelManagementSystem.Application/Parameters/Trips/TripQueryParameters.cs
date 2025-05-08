using TravelManagementSystem.Application.Parameters.Base;

namespace TravelManagementSystem.Application.Parameters.Trips
{
    public class TripQueryParameters : QueryParameters
    {
        public string? Title { get; set; }
        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }

        public Dictionary<string, object?> ToFilterDictionary()
        {
            var filters = new Dictionary<string, object?>();

            if (!string.IsNullOrWhiteSpace(Title))
                filters[nameof(Title)] = Title;

            if (PriceMin.HasValue)
                filters["Price >= "] = PriceMin.Value;

            if (PriceMax.HasValue)
                filters["Price <= "] = PriceMax.Value;

            return filters;
        }
    }

}

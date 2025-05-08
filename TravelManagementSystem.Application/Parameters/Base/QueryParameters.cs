using TravelManagementSystem.Shared.Constants;

namespace TravelManagementSystem.Application.Parameters.Base
{
    public class QueryParameters
    {
        public int Page { get; set; } = ApplicationConstants.DefaultPage;

        private int _pageSize = ApplicationConstants.DefaultPageSize;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > ApplicationConstants.MaxPageSize ? ApplicationConstants.MaxPageSize : value;
        }

        public string? OrderBy { get; set; }

        public bool IsDescending { get; set; } = false;
    }
}

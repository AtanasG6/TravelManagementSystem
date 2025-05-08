using X.PagedList;

namespace TravelManagementSystem.Application.Wrappers
{
    public class PagedResponse<T> : ApiResponse<List<T>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItemCount { get; set; }
        public int PageCount { get; set; }

        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public bool IsFirstPage { get; set; }
        public bool IsLastPage { get; set; }

        public PagedResponse()
        {
        }

        public PagedResponse(IPagedList<T> pagedList, string? message = null)
            : base(true, message, pagedList.ToList())
        {
            PageNumber = pagedList.PageNumber;
            PageSize = pagedList.PageSize;
            TotalItemCount = pagedList.TotalItemCount;
            PageCount = pagedList.PageCount;

            HasPreviousPage = pagedList.HasPreviousPage;
            HasNextPage = pagedList.HasNextPage;
            IsFirstPage = pagedList.IsFirstPage;
            IsLastPage = pagedList.IsLastPage;
        }
    }
}

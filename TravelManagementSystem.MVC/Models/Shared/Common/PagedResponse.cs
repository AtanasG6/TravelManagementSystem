using X.PagedList;

namespace TravelManagementSystem.MVC.Models.Shared.Common
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

        public PaginationInfo ToPaginationInfo()
        {
            return new PaginationInfo
            {
                CurrentPage = PageNumber,
                PageSize = PageSize,
                TotalCount = TotalItemCount,
                PageCount = PageCount,
                HasPreviousPage = HasPreviousPage,
                HasNextPage = HasNextPage,
                IsFirstPage = IsFirstPage,
                IsLastPage = IsLastPage
            };
        }
    }
}

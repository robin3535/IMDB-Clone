using System.Collections.Generic;
namespace ImdbApi.Common
{
    public class PagedResponse<T>
    {
        public IReadOnlyList<T> Data { get; init; }
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public int TotalRecords { get; init; }
        public int TotalPages { get; init; }
        public bool HasNextPage => PageNumber < TotalPages;
        public bool HasPrevious => PageNumber > 1;
    }
}
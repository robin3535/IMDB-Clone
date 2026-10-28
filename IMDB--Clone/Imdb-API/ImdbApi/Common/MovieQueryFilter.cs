namespace ImdbApi.Common
{
    public class MovieQueryFilter
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 2;
        public string? SortBy { get; set; }
        public string? Search { get; set; }
    }
}
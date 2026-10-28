using System;
using Dapper;

namespace ImdbApi.Common
{
    public static class SQLQueryExtensions
    {
        public static string ApplyOffsetPagination(this string query, int pageNumber, int pageSize, DynamicParameters parameters)
        {
            var validPageNumber = Math.Max(1, pageNumber);
            var validPageSize = Math.Clamp(pageSize, 1, 50);
            parameters.Add("Offset", (validPageNumber - 1) * validPageSize);
            parameters.Add("PageSize", validPageSize);
            return $"{query} OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
        }

        // Try KeySet pagination
        // Cursor based pagination
        // for better optimization for large datasets and infinite scrolling
    }
}
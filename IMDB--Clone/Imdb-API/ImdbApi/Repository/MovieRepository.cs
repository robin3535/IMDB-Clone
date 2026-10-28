using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using Dapper;
using ImdbApi.Models.DB;
using ImdbApi.Models.RequestModel;
using ImdbApi.Models.ResponseModel;
using ImdbApi.Repository.Interfaces;
using Microsoft.Extensions.Options;
using ImdbApi.Common;
using static System.Net.Mime.MediaTypeNames;

namespace ImdbApi.Repository
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {

        public MovieRepository(IOptions<ConnectionString> connectionString) : base(connectionString.Value.IMDBDB)
        {

        }
        public IEnumerable<Movie> Get(MovieQueryFilter filter)
        {
            var parameters = new DynamicParameters();
            var query = @"
SELECT
       [Id],
       [NAME],
       [year_of_release] AS [Yor],
       [Plot],
       [Poster] AS [CoverImage],
       [language],       
       [profit],
       [Producer_Id] AS [ProducerId],
       [UpdatedAt]      
FROM   [foundation].[movies](NOLOCK)
ORDER BY [Id] ASC".ApplyOffsetPagination(filter.PageNumber, filter.PageSize, parameters);
            return Get(query, parameters);
        }
        public Movie Get(int id)
        {
            var query = @"
SELECT
       [Id],
       [NAME],
       [year_of_release] AS [Yor],
       [Plot],
       [Poster] AS [CoverImage],
       [language],       
       [profit],
       [Producer_Id] AS [ProducerId],
       [UpdatedAt]      
FROM   [Foundation].[Movies](NOLOCK)
WHERE Id = @Id";
            return Get(query, new { Id = id });
        }
        public int Create(MovieRequest movieRq)
        {
            return Create("usp_insert_movie", new
            {
                chvName = movieRq.Name,
                intYearOfRelease = movieRq.Yor,
                txtPlot = movieRq.Plot,
                chvPoster = movieRq.CoverImage,
                chrMovieLanguage = movieRq.Language,
                intProfit = movieRq.profit,
                intProducerId = movieRq.ProducerId,
                chvActorIds = movieRq.Actors,
                chvGenreIds = movieRq.Genres
            });
        }
        public void Update(MovieRequest movieRq, int id)
        {

            Update("usp_update_movie", new
            {
                intId = id,
                chvName = movieRq.Name,
                intYearOfRelease = movieRq.Yor,
                txtPlot = movieRq.Plot,
                chvPoster = movieRq.CoverImage,
                chrMovieLanguage = movieRq.Language,
                intProfit = movieRq.profit,
                intProducerId = movieRq.ProducerId,
                chvActorIds = movieRq.Actors,
                chvGenreIds = movieRq.Genres,
                dtmUpdatedAt = DateTime.Now
            });


        }
        public void Delete(int id)
        {
            Delete("usp_delete_movie", new
            {
                intId = id
            });
        }

        public int GetRecordCount()
        {
            const string query = "SELECT COUNT(*) FROM [Foundation].[Movies]";
            return GetRecordCount(query);
        }
    }
}

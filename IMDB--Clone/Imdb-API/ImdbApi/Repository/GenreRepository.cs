using System;
using System.Collections.Generic;
using System.Linq;
using ImdbApi.Models.DB;
using ImdbApi.Models.RequestModel;
using ImdbApi.Models.ResponseModel;
using ImdbApi.Repository.Interfaces;
using Microsoft.Extensions.Options;

namespace ImdbApi.Repository
{
    public class GenreRepository : BaseRepository<Genre>,IGenreRepository
    {
        public GenreRepository(IOptions<ConnectionString> connectionString) : base(connectionString.Value.IMDBDB)
        {
            
        }
        public IEnumerable<Genre> Get()
        {
            string query = @"
SELECT [Id],
       [Name]
FROM   [Foundation].[Genres] (NOLOCK)";

            return Get(query);
        }
        public Genre Get(int id)
        {
            string query = @"
SELECT [Id],
       [Name]
FROM   [Foundation].[Genres] (NOLOCK)
WHERE Id=@Id";
            return Get(query,new {Id = id});
        }
        public IEnumerable<Genre> GetGenreByMovieId(int movieId)
        {
            var query = @"
SELECT
  [G].[Id],
  [G].[Name]
FROM
  [Foundation].[Movies] M
  join [Foundation].[Genres_Movies] GM on [M].[Id] = [GM].[Movie_Id]
  join [Foundation].[Genres] G on [GM].[Genre_Id] = [G].[Id]
WHERE
  [M].[Id] = @Id;";
            return Gets(query, new { Id = movieId });
        }
        public int Create(GenreRequest genreRq)
        {
            
            return Create("usp_insert_genre",
                new
                {
                    chvName = genreRq.Name
                });

        }
        public void Update(GenreRequest genreRq,int id)
        {
            Update("usp_update_genre",
                new
                {
                    intId = id,
                    chvName = genreRq.Name,
                    dtmUpdatedAt = DateTime.Now
                });

        }
        public void Delete(int id)
        {
            Delete("usp_delete_genre", new
            {
                intId = id
            });
        }
    }
}

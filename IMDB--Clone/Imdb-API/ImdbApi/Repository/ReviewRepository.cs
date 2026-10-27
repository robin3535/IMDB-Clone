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
    public class ReviewRepository :BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(IOptions<ConnectionString> connectionString) : base(connectionString.Value.IMDBDB)
        {

        }
        public IEnumerable<Review> GetReviewsByMovieId(int movieId) {
            var query = @"
SELECT [Id]
	,[Message]
	,[Movie_Id] as [MovieId]
FROM [Foundation].[Reviews](NOLOCK)
WHERE [Movie_Id] = @Id
";
            return Gets(query,new { Id = movieId});
        }
        public Review Get(int id) {
            var query = @"
SELECT [Id]
	,[Message]
	,[Movie_Id] as [MovieId]
FROM [Foundation].[Reviews](NOLOCK)
WHERE [Id] = @Id
";
            return Get(query, new {Id = id});
        }
        public int Create(ReviewRequest reviewRq, int movieId) {
            return Create("usp_insert_review",
              new
              {
                  txtMessage = reviewRq.Message,
                  intMovieId = movieId
              });
        }
        public void Update(ReviewRequest reviewRq,int id)
        {
            Update("usp_update_review",
              new
              {
                  intId = id,
                  txtMessage = reviewRq.Message,
                  dtmUpdatedAt = DateTime.Now
              });

        }
        public void Delete(int id)
        {
            Delete("usp_delete_review", new
            {
                intId = id
            });
        }

    }
}

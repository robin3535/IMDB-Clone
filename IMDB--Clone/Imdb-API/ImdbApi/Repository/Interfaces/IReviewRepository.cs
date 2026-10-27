using System.Collections.Generic;
using ImdbApi.Models.DB;
using ImdbApi.Models.RequestModel;
using ImdbApi.Models.ResponseModel;

namespace ImdbApi.Repository.Interfaces
{
    public interface IReviewRepository
    {
        int Create(ReviewRequest review,int movieId);
        void Delete(int id);
        IEnumerable<Review> GetReviewsByMovieId(int movieId);
        Review Get(int id);
        void Update(ReviewRequest review,int id);
    }
}
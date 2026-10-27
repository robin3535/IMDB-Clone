using System.Collections.Generic;
using ImdbApi.Models.DB;
using ImdbApi.Models.RequestModel;
using ImdbApi.Models.ResponseModel;

namespace ImdbApi.Services.Interfaces
{
    public interface IReviewService
    {
        IEnumerable<ReviewResponse> GetReviewsByMovieId(int movieId);
        ReviewResponse Get(int id);
        int Create(ReviewRequest review,int movieId);
        void Update(ReviewRequest review,int id);
        void Delete(int id);
    }
}

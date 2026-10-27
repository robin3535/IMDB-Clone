using ImdbApi.Models.DB;
using ImdbApi.Models.RequestModel;
using ImdbApi.Models.ResponseModel;
using System.Collections.Generic;

namespace ImdbApi.Services.Interfaces
{
    public interface IMovieService
    {
        IEnumerable<MovieResponse> Get();
        MovieResponse Get(int id);
        int Create(MovieRequest movie);
        void Update(MovieRequest movie,int id);
        void Delete(int id);
    }
}

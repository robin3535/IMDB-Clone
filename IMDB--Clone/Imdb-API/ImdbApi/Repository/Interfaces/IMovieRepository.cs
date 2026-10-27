using System.Collections.Generic;
using ImdbApi.Models.DB;
using ImdbApi.Models.RequestModel;
using ImdbApi.Models.ResponseModel;

namespace ImdbApi.Repository.Interfaces
{
    public interface IMovieRepository
    {
        int Create(MovieRequest movie);
        void Delete(int id);
        Movie Get(int id);
        IEnumerable<Movie> Get();
        void Update(MovieRequest movie, int id);
    }
}
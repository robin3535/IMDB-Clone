using System.Collections.Generic;
using ImdbApi.Models.DB;
using ImdbApi.Models.RequestModel;
using ImdbApi.Models.ResponseModel;

namespace ImdbApi.Repository.Interfaces
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> Get();
        Genre Get(int id);
        IEnumerable<Genre> GetGenreByMovieId(int movieId);
        int Create(GenreRequest genre);
        void Delete(int id);
        void Update(GenreRequest genre,int id);
    }
}
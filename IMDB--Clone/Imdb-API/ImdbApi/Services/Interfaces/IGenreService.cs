
using ImdbApi.Models.DB;
using ImdbApi.Models.RequestModel;
using ImdbApi.Models.ResponseModel;
using System.Collections.Generic;

namespace ImdbApi.Services.Interfaces
{
    public interface IGenreService
    {
        IEnumerable<GenreResponse> Get();
        GenreResponse Get(int id);
        int Create(GenreRequest genre);
        IEnumerable<Genre> GetGenreByMovieId(int movieId);
        void Update(GenreRequest genre,int id);
        void Delete(int id);
    }
}

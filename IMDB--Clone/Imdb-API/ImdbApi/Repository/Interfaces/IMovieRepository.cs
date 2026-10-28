using System.Collections.Generic;
using ImdbApi.Common;
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
        IEnumerable<Movie> Get(MovieQueryFilter filter);
        int GetRecordCount();
        void Update(MovieRequest movie, int id);
    }
}
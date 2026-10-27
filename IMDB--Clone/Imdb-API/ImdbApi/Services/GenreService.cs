using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ImdbApi.Exceptions;
using ImdbApi.Models.DB;
using ImdbApi.Models.RequestModel;
using ImdbApi.Models.ResponseModel;
using ImdbApi.Repository;
using ImdbApi.Repository.Interfaces;
using ImdbApi.Services.Interfaces;

namespace ImdbApi.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository genreRepository,IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }
        public IEnumerable<GenreResponse> Get()
        {
            return _mapper.Map<List<GenreResponse>>(_genreRepository.Get());
        }
        public GenreResponse Get(int id)
        {
            if (_genreRepository.Get(id) == null)
            {
                throw new RecordNotFoundException("No Genre found with id= " + id);
            }
            return _mapper.Map<GenreResponse>(_genreRepository.Get(id));
        }
        public IEnumerable<Genre> GetGenreByMovieId(int movieId)
        {
            if(_genreRepository.GetGenreByMovieId(movieId) == null)
            {
                throw new RecordNotFoundException("No Genres found.");
            }
            return _genreRepository.GetGenreByMovieId(movieId);
        }
        public int Create(GenreRequest genre)
        {
            if (Validate(genre))
            {
                return _genreRepository.Create(genre);
            }
            return 0;
        }
        public void Delete(int id)
        {
            if (_genreRepository.Get(id) == null)
            {
                throw new RecordNotFoundException("No Genre found with id= " + id);
            }
            _genreRepository.Delete(id);
        }
        public void Update(GenreRequest genre,int id)
        {
            if (_genreRepository.Get(id) == null)
            {
                throw new RecordNotFoundException("No Genre found with id= " + id);
            }
            if (Validate(genre))
            {
                _genreRepository.Update(genre,id);
            }
        }
        public bool Validate(GenreRequest genre)
        {
            if (string.IsNullOrEmpty(genre.Name))
            {
                throw new FieldValueNullException("Name cannot be empty.");
            }
            if (int.TryParse(genre.Name, out int rName))
            {
                throw new InvalidFieldValueException("Name cannot be integer.");
            }
            return true;
        }
    }
}

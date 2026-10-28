using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using AutoMapper;
using ImdbApi.Common;
using ImdbApi.Exceptions;
using ImdbApi.Models;
using ImdbApi.Models.DB;
using ImdbApi.Models.RequestModel;
using ImdbApi.Models.ResponseModel;
using ImdbApi.Repository;
using ImdbApi.Repository.Interfaces;
using ImdbApi.Services.Interfaces;

namespace ImdbApi.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IActorService _actorService;
        private readonly IProducerService _producerService;
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public MovieService(IMovieRepository movieRepository,
            IActorService actorService, IProducerService producerService, IGenreService genreService, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _actorService = actorService;
            _producerService = producerService;
            _genreService = genreService;
            _mapper = mapper;
        }
        public PagedResponse<MovieResponse> Get(MovieQueryFilter filter)
        {

            var recordCount = _movieRepository.GetRecordCount();
            IEnumerable<Movie> movies = _movieRepository.Get(filter);
            IEnumerable<MovieResponse> movieResponses = movies.Select(movie => new MovieResponse()
            {

                Id = movie.Id,
                Name = movie.Name,
                Yor = movie.Yor,
                Plot = movie.Plot,
                Language = movie.Language,
                Profit = movie.Profit,
                Producer = _mapper.Map<ProducerResponse>(_producerService.Get(movie.ProducerId)),
                Actors = _mapper.Map<List<ActorResponse>>(_actorService.GetActorByMovieId(movie.Id)),
                Genres = _mapper.Map<List<GenreResponse>>(_genreService.GetGenreByMovieId(movie.Id)),
                CoverImage = movie.CoverImage

            });
            var response = new PagedResponse<MovieResponse>()
            {
                Data = movieResponses.ToList(),
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalRecords = recordCount,
                TotalPages = (int)Math.Ceiling(recordCount / (double)filter.PageSize)
            };
            return response;
        }

        public MovieResponse Get(int id)
        {
            if (_movieRepository.Get(id) == null)
            {
                throw new RecordNotFoundException("No Movie found with id= " + id);
            }
            var movie = _movieRepository.Get(id);
            var movieResponse = new MovieResponse()
            {
                Id = movie.Id,
                Name = movie.Name,
                Yor = movie.Yor,
                Plot = movie.Plot,
                Language = movie.Language,
                Profit = movie.Profit,
                Producer = _mapper.Map<ProducerResponse>(_producerService.Get(movie.ProducerId)),
                Actors = _mapper.Map<List<ActorResponse>>(_actorService.GetActorByMovieId(movie.Id)),
                Genres = _mapper.Map<List<GenreResponse>>(_genreService.GetGenreByMovieId(movie.Id)),
                CoverImage = movie.CoverImage
            };
            return movieResponse;
        }
        public int Create(MovieRequest movie)
        {
            if (Validate(movie))
            {
                return _movieRepository.Create(movie);
            }
            return 0;
        }
        public void Delete(int id)
        {
            if (_movieRepository.Get(id) == null)
            {
                throw new RecordNotFoundException("No Movie found with id= " + id);
            }
            _movieRepository.Delete(id);
        }
        public void Update(MovieRequest movie, int id)
        {
            if (_movieRepository.Get(id) == null)
            {
                throw new RecordNotFoundException("No Movie found with id= " + id);
            }
            if (Validate(movie))
            {
                _movieRepository.Update(movie, id);
            }
        }
        public bool Validate(MovieRequest movie)
        {
            var actorIds = movie.Actors.Split(',');
            foreach (var actorId in actorIds)
            {
                if (_actorService.Get(int.Parse(actorId)) == null)
                {
                    throw new RecordNotFoundException("No Actor found with id= " + actorId);
                }
            }
            var genreIds = movie.Genres.Split(',');
            foreach (var genreId in genreIds)
            {
                if (_genreService.Get(int.Parse(genreId)) == null)
                {
                    throw new RecordNotFoundException("No Genre found with id= " + genreId);
                }
            }
            if (_producerService.Get(movie.ProducerId) == null)
            {
                throw new RecordNotFoundException("No Producer found with id= " + movie.ProducerId);
            }
            if (string.IsNullOrEmpty(movie.Name))
            {
                throw new FieldValueNullException("Name cannot be empty.");
            }
            if (string.IsNullOrEmpty(movie.Plot))
            {
                throw new FieldValueNullException("Plot cannot be empty.");
            }
            if (string.IsNullOrEmpty(movie.CoverImage))
            {
                throw new FieldValueNullException("Cover Image cannot be empty.");
            }
            if (string.IsNullOrEmpty(movie.Language))
            {
                throw new FieldValueNullException("Language cannot be empty.");
            }
            if (movie.Yor < 0 || movie.Yor > DateTime.Now.Year)
            {
                throw new InvalidFieldValueException("Invalid Year of release.");
            }
            return true;
        }
    }
}

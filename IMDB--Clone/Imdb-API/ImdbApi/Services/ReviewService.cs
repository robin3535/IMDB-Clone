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
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepository,IMapper mapper, IMovieService movieService)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _movieService = movieService;
        }
        public IEnumerable<ReviewResponse> GetReviewsByMovieId(int movieId)
        {
            return _mapper.Map<List<ReviewResponse>>(_reviewRepository.GetReviewsByMovieId(movieId));
        }

        public ReviewResponse Get(int id)
        {
            if (_reviewRepository.Get(id) == null)
            {
                throw new RecordNotFoundException("No Review found with id= " + id);
            }
            return _mapper.Map<ReviewResponse>(_reviewRepository.Get(id));
        }
        public int Create(ReviewRequest reviewRq,int movieId)
        {
            try
            {
                var Movie = _movieService.Get(movieId);
                if (Validate(reviewRq))
                {
                    return _reviewRepository.Create(reviewRq, movieId);
                }
                return 0;
            }
            catch (RecordNotFoundException)
            {
                throw new RecordNotFoundException("No Movie found with Id=" + movieId);
            }
           
        }
        public void Delete(int id)
        {
            if (_reviewRepository.Get(id) == null)
            {
                throw new RecordNotFoundException("No Review found with id= " + id);
            }
            _reviewRepository.Delete(id);
        }
        public void Update(ReviewRequest reviewRq, int id)
        {
            if (_reviewRepository.Get(id) == null)
            {
                throw new RecordNotFoundException("No Review found with id= " + id);
            }
            if (Validate(reviewRq)) { _reviewRepository.Update(reviewRq,id);}
            
        }
        public bool Validate(ReviewRequest review) {
            if (string.IsNullOrEmpty(review.Message))
            {
                throw new FieldValueNullException("Message cannot be empty.");
            }
            return true;
        }
    }
}

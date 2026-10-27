using AutoMapper;
using ImdbApi.Models.DB;
using ImdbApi.Models.RequestModel;
using ImdbApi.Models.ResponseModel;

namespace ImdbApi.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Actor, ActorResponse>().ReverseMap();
            CreateMap<Actor, ActorRequest>().ReverseMap();
            CreateMap<Genre, GenreResponse>().ReverseMap();
            CreateMap<Producer, ProducerResponse>().ReverseMap();
            CreateMap<Review, ReviewResponse>().ReverseMap();
        }
    }
}

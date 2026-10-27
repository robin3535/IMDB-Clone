using System.Collections.Generic;
using ImdbApi.Models.DB;

namespace ImdbApi.Models.ResponseModel
{
    public class MovieResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Yor { get; set; }
        public string Plot { get; set; }
        public string Language { get; set; }
        public double Profit { get; set; }
        public List<ActorResponse> Actors { get; set; }
        public List<GenreResponse> Genres { get; set; }
        public  ProducerResponse Producer{ get; set; }
        public string CoverImage { get; set; }
       
    }
}

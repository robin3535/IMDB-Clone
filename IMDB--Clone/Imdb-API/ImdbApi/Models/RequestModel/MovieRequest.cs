
namespace ImdbApi.Models.RequestModel
{
    public class MovieRequest
    {
        public string Name { get; set; }
        public int Yor { get; set; }
        public string Plot { get; set; }
        public string Actors { get; set; }
        public string Language { get; set; }
        public double profit { get; set; }
        public string Genres { get; set; }
        public int ProducerId { get; set; }
        public string CoverImage { get; set; }
        public string UpdatedAt { get; set; }
    }
}

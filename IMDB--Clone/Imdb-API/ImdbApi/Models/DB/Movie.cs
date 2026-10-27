
namespace ImdbApi.Models.DB
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Yor { get; set; }
        public string Plot { get; set; }
        public string Language { get; set; }
        public double Profit { get; set; }
        public string UpdatedAt { get; set; }
        public int ProducerId { get; set; }
        public string CoverImage { get; set; }
    }
}

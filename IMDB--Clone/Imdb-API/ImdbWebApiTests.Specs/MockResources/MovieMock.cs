using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImdbApi.Models.DB;
using ImdbApi.Models.RequestModel;
using ImdbApi.Models.ResponseModel;
using ImdbApi.Repository.Interfaces;
using Moq;

namespace ImdbWebApiTests.Specs.MockResources
{
    public class MovieMock
    {
        public static readonly Mock<IMovieRepository> MovieRepoMock = new Mock<IMovieRepository>();
        private static readonly List<Movie> Movies = new List<Movie>()
        {
            new Movie
            {
               Id=1,
               Name="Pathaan",
               Yor=2022,
               Plot="It is...",
               Language="Hindi",
               Profit=100000,
               ProducerId=1,              
               CoverImage="https//www.img.png"
            },  new Movie
            {
               Id=2,
               Name="Shaurya",
               Yor=2008,
               Plot="It is...",
               Language="Hindi",
               Profit=100000,
               ProducerId=1,
               CoverImage="https//www.img.png"
            }
        };

        public static void MockGet()
        {
           MovieRepoMock.Setup(x => x.Get()).Returns(Movies);
        }
        public static void MockGetById()
        {
            MovieRepoMock.Setup(x => x.Get(It.IsAny<int>())).Returns((int id) =>
            {
                return Movies.FirstOrDefault(x => x.Id == id);
            });

        }
        public static void MockCreate()
        {
            MovieRepoMock.Setup(x => x.Create(It.IsAny<MovieRequest>())).Returns(1);
        }

    }
}

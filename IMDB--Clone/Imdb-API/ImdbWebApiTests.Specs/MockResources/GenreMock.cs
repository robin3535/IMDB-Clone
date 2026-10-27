using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImdbApi.Models.DB;
using ImdbApi.Models.RequestModel;
using ImdbApi.Repository.Interfaces;
using Moq;

namespace ImdbWebApiTests.Specs.MockResources
{
    public class GenreMock
    {
        public static readonly Mock<IGenreRepository> GenreRepoMock = new Mock<IGenreRepository>();
        private static readonly List<Genre> Genres = new List<Genre>()
        {
            new Genre()
            {
                Id = 1,
                Name = "Drama"
            },
            new Genre()
            {
                Id = 2,
                Name = "Action"
            }
        };
        public static void MockGet()
        {
            GenreRepoMock.Setup(x => x.Get()).Returns(Genres);
        }
        public static void MockGetById()
        {
            GenreRepoMock.Setup(x => x.Get(It.IsAny<int>())).Returns((int id) =>
            {
                return Genres.FirstOrDefault(x => x.Id == id);
            });

        }
        public static void MockGetGenreByMovieId()
        {
            GenreRepoMock.Setup(x => x.GetGenreByMovieId(It.IsAny<int>())).Returns(new List<Genre>()
        {
            new Genre()
            {
                Id = 1,
                Name = "Drama"
            }
        });
        }
        public static void MockCreate()
        {
            GenreRepoMock.Setup(x => x.Create(It.IsAny<GenreRequest>())).Returns(1);
        }
    }
}

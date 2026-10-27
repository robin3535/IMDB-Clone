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
    public class ActorMock
    {
        public static readonly Mock<IActorRepository> ActorRepoMock = new Mock<IActorRepository>();

        private static readonly List<Actor> Actors = new List<Actor>
        {
            new Actor
            {
               Id = 1,
               Name = "Shah Rukh Khan",
               Bio = "He is a Superstar..",
               DOB = new DateTime(1965,11,04),
               Gender = "Male"
            },new Actor
            {
               Id = 2,
               Name = "Ranbir Kapoor",
               Bio = "He is a star..",
               DOB = new DateTime(1985,10,04),
               Gender = "Male"
            }
        };

        public static void MockGet()
        {
            ActorRepoMock.Setup(x => x.Get()).Returns(Actors);
        }
       
        public static void MockGetById()
        {
            ActorRepoMock.Setup(x => x.Get(It.IsAny<int>())).Returns((int id) =>
            {
                return Actors.FirstOrDefault(x => x.Id == id);
            });
           
        }
        public static void MockGetActorByMovieId()
        {
            ActorRepoMock.Setup(x => x.GetActorByMovieId(It.IsAny<int>())).Returns(new List<Actor>(){ new Actor
            {
                Id = 1,
                Name = "Shah Rukh Khan",
                Bio = "He is a Superstar..",
                DOB = new DateTime(1965, 11, 04),
                Gender = "Male"
            } 
            });
        }
        public static void MockCreate()
        {
            ActorRepoMock.Setup(x => x.Create(It.IsAny<ActorRequest>())).Returns(1);
        }
       
    }
}

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
    public class ProducerMock
    {
        public static readonly Mock<IProducerRepository> ProducerRepoMock = new Mock<IProducerRepository>();
        private static readonly List<Producer> Producers = new List<Producer>()
        {
            new Producer()
            {
                Id = 1,
                Name="Karan",
                Bio = "He is ..",
                DOB = new DateTime(1965,10,04),
                Gender = "Male"
            },
             new Producer()
            {
                Id = 2,
                Name="Deepika",
                Bio = "She is ..",
                DOB = new DateTime(1985,10,04),
                Gender = "Female"
            }
        };
        public static void MockGet()
        {
            ProducerRepoMock.Setup(x => x.Get()).Returns(Producers);    
        }
        public static void MockGetById()
        {
            ProducerRepoMock.Setup(x => x.Get(It.IsAny<int>())).Returns((int id) =>
            {
                return Producers.FirstOrDefault(x => x.Id == id);
            });

        }
        public static void MockCreate()
        {
            ProducerRepoMock.Setup(x => x.Create(It.IsAny<ProducerRequest>())).Returns(1);
        }

    }
}

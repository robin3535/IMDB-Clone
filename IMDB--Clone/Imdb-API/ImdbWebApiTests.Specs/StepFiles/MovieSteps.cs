using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;
using ImdbWebApiTests.Specs.MockResources;

namespace ImdbWebApiTests.Specs.StepFiles
{
    [Scope(Feature = "Movie Resource")]
    [Binding]
    public class MovieSteps : BaseSteps
    {
        public MovieSteps(CustomWebApplicationFactory factory)
         : base(factory.WithWebHostBuilder(builder =>
         {
             builder.ConfigureServices(services =>
             {
                 // Mock Repo
                 services.AddScoped(_ => MovieMock.MovieRepoMock.Object);
                 services.AddScoped(_ => ActorMock.ActorRepoMock.Object);
                 services.AddScoped(_ => ProducerMock.ProducerRepoMock.Object);
                 services.AddScoped(_ => GenreMock.GenreRepoMock.Object);
             });
         }))
        {
        }
        [BeforeScenario]
        public static void Mocks()
        {
            MovieMock.MockGet();
            MovieMock.MockGetById();
            MovieMock.MockCreate(); 
            ActorMock.MockGetActorByMovieId();
            ActorMock.MockGetById();
            ProducerMock.MockGetById();
            GenreMock.MockGetById();
            GenreMock.MockGetGenreByMovieId();
        }
    }
}

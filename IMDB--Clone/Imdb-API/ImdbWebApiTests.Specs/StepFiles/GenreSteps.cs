using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImdbWebApiTests.Specs.MockResources;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;

namespace ImdbWebApiTests.Specs.StepFiles
{
    [Scope(Feature = "Genre Resource")]
    [Binding]
    public class GenreSteps : BaseSteps
    {
        public GenreSteps(CustomWebApplicationFactory factory) 
            : base(factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Mock Repo
                services.AddScoped(_ => GenreMock.GenreRepoMock.Object);
            });
            }))
        {
            
        }
        [BeforeScenario]
        public static void Mocks()
        {
            GenreMock.MockGet();
            GenreMock.MockGetById();
            GenreMock.MockCreate();
        }
    }
}

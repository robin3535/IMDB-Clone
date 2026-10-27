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
    [Scope(Feature = "Actor Resource")]
    [Binding]
    public class ActorSteps : BaseSteps
    {
        public ActorSteps(CustomWebApplicationFactory factory)
           : base(factory.WithWebHostBuilder(builder =>
           {
               builder.ConfigureServices(services =>
               {
                   // Mock Repo
                   services.AddScoped(_ => ActorMock.ActorRepoMock.Object);
               });
           }))
        {
        }

        [BeforeScenario]
        public static void Mocks()
        {
            ActorMock.MockGet();
            ActorMock.MockGetById();
            ActorMock.MockCreate();
        }
    }
}

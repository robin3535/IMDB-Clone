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
    [Scope(Feature = "Producer Resource")]
    [Binding]
    public class ProducerSteps : BaseSteps
    {
        public ProducerSteps(CustomWebApplicationFactory factory)
          : base(factory.WithWebHostBuilder(builder =>
          {
              builder.ConfigureServices(services =>
              {
                  // Mock Repo
                  services.AddScoped(_ => ProducerMock.ProducerRepoMock.Object);
              });
          }))
        {
        }

        [BeforeScenario]
        public static void Mocks()
        {
            ProducerMock.MockGet();
            ProducerMock.MockGetById();
            ProducerMock.MockCreate();
        }
    }
}

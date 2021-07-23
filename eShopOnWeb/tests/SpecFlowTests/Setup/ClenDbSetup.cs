using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SpecFlowTests.Setup
{
    [Binding]
    public class CleanDbSetup
    {
        private readonly UserCommandsHandler _handler;

        public CleanDbSetup(UserCommandsHandler handler)
        {
            _handler = handler;
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            await _handler.CleanDb();
        }
    }
}
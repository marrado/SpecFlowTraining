using TechTalk.SpecFlow;

namespace SpecFlowTests.CurrentUser
{
    [Binding]
    public class CurrentUserSteps
    {
        private readonly CurrentUserDriver _driver;

        public CurrentUserSteps(CurrentUserDriver driver)
        {
            _driver = driver;
        }

        [Given(@"I am logged in as '(.*)'")]
        public void GivenIAmLoggedInAs(string username)
        {
            _driver.GivenIAmLoggedInAs(username);
        }
    }
}
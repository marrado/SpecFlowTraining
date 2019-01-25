using System.Net;
using Microsoft.eShopWeb.Web;
using TechTalk.SpecFlow;

namespace SpecFlowTests.Infrastructure
{
    [Binding]
    public static class TestInfrastructure
    {
        [BeforeTestRun]
        public static void InitializeTestRun()
        {
            var container = new CookieContainer();
            container.Add(new Cookie(Constants.BASKET_COOKIENAME, TestConstants.TestUserId)
            {
                Domain = "localhost"
            });
        }
    }
}